using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sokoban.Core.Tests
{
	[TestFixture]
	internal class LurdTests
	{
		[Test]
		public void BasicTest()
		{
			const string input = @"#####
#---#
#---#
#@--#
#####";
			const string output = @"#####
#---#
#@--#
#---#
#####";
			var level = new LevelCreator().Create(input);
			var lurded = new Lurd("rruulld").TryOn(level);
			Assert.IsTrue(lurded);
			Assert.AreEqual(output, level.ToString());
		}

		[Test]
		public void LevelShouldBeInOriginalStateIfLurdFailed()
		{
			const string input = @"#####
#---#
#---#
#@--#
#####";
			var level = new LevelCreator().Create(input);
			var lurded = new Lurd("lurd").TryOn(level);
			Assert.IsFalse(lurded);
			Assert.AreEqual(input, level.ToString());

			lurded = new Lurd("rrdd").TryOn(level);

			Assert.IsFalse(lurded);
			Assert.AreEqual(input, level.ToString());
		}

		[Test]
		public void LurdTest()
		{
			const string input = @"
#####
#---#
#---#
#@--#
#####";
			foreach (var lurd in Lurd.All(8))
			{
				var level = new LevelCreator().Create(input);
				var lurded = lurd.TryOn(level);
				if (lurded)
				{
					Assert.AreEqual(lurd.ToString(), level.GetLurd());
				}
			}
		}

		[Test]
		public void LurdFTest()
		{
			//mx == 0
			Assert.AreEqual('l', Lurd.F(0, 0, 0));
			Assert.AreEqual('u', Lurd.F(0, 1, 0));
			Assert.AreEqual('r', Lurd.F(0, 2, 0));
			Assert.AreEqual('d', Lurd.F(0, 3, 0));
			//mx == 1
			Assert.AreEqual('l', Lurd.F(0, 0, 1));
			Assert.AreEqual('l', Lurd.F(0, 1, 1));
			Assert.AreEqual('l', Lurd.F(0, 2, 1));
			Assert.AreEqual('l', Lurd.F(0, 3, 1));

			Assert.AreEqual('l', Lurd.F(1, 0, 1));
			Assert.AreEqual('u', Lurd.F(1, 1, 1));
			Assert.AreEqual('r', Lurd.F(1, 2, 1));
			Assert.AreEqual('d', Lurd.F(1, 3, 1));
			//mx == 2
			Assert.AreEqual('l', Lurd.F(0, 0, 2));
			Assert.AreEqual('l', Lurd.F(0, 1, 2));
			Assert.AreEqual('l', Lurd.F(0, 2, 2));
			Assert.AreEqual('l', Lurd.F(0, 3, 2));

			Assert.AreEqual('l', Lurd.F(1, 0, 2));
			Assert.AreEqual('l', Lurd.F(1, 1, 2));
			Assert.AreEqual('l', Lurd.F(1, 2, 2));
			Assert.AreEqual('l', Lurd.F(1, 3, 2));

			Assert.AreEqual('l', Lurd.F(2, 0, 2));
			Assert.AreEqual('u', Lurd.F(2, 1, 2));
			Assert.AreEqual('r', Lurd.F(2, 2, 2));
			Assert.AreEqual('d', Lurd.F(2, 3, 2));

			Assert.AreEqual(4, Lurd.All(1).Select(lurd => lurd.ToString()).Distinct().Count());
			Assert.AreEqual(16, Lurd.All(2).Select(lurd => lurd.ToString()).Distinct().Count());
			Assert.AreEqual(64, Lurd.All(3).Select(lurd => lurd.ToString()).Distinct().Count());
		}
	}
}
