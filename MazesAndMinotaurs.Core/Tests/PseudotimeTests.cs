using MazesAndMinotaurs.Core.Pseudotime;
using NUnit.Framework;

namespace MazesAndMinotaurs.Core.Tests
{
	[TestFixture]
	internal class PseudotimeTests
	{
		[Test]
		public void CreationTest()
		{
			var speed = Speed.FromApq(0);
			Assert.AreEqual(0, speed.ActionsPerQuantum);
			Assert.IsNull(speed.QuantumsPerAction);

			Assert.Catch(() => Speed.FromQpa(0));

			Assert.Catch(() => Speed.FromApq(-1));
			Assert.Catch(() => Speed.FromQpa(-1));

			speed = Speed.FromQpa(1000);
			Assert.AreEqual(0.001m, speed.ActionsPerQuantum);
		}

		[Test]
		public void TimeAwareObjectTest()
		{
			var tao = new TimeAwareObject(Speed.FromQpa(1000));
			int[] steps = {0};
			tao.Action += () => steps[0]++;
			Assert.AreEqual(0, steps[0]);
			for (var i = 1; i <= 999; i++)
			{
				tao.Quantum();
				Assert.AreEqual(0, steps[0]);
			}
			tao.Quantum();
			Assert.AreEqual(1, steps[0]);

			var speed = Speed.FromApq(0);
			tao = new TimeAwareObject(speed);
			steps[0] = 0;
			tao.Action += () => steps[0]++;
			Assert.AreEqual(0, steps[0]);
			for (var i = 1; i <= 10000; i++)
				tao.Quantum();
			Assert.AreEqual(0, steps[0]);
			speed.QuantumsPerAction = 2;
			tao.Quantum();
			Assert.AreEqual(0, steps[0]);
			tao.Quantum();
			Assert.AreEqual(1, steps[0]);
		}
	}
}
