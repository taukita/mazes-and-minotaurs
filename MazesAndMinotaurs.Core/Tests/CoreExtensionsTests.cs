using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MazesAndMinotaurs.Core.Tests
{
	[TestFixture]
	internal class CoreExtensionsTests
	{
		[Test]
		public void SplitTest1()
		{
			var arr = new[] {1, 2, 3, 4, 5, 6, 3, 4};
			var parts = arr.Split(new[] {3, 4}).ToArray();
			Assert.AreEqual(3, parts.Length);
			Assert.IsTrue(parts[0].SequenceEqual(new[] {1, 2}));
			Assert.IsTrue(parts[1].SequenceEqual(new[] {5, 6}));
			Assert.IsTrue(parts[2].SequenceEqual(new int[0]));
		}

		[Test]
		public void SplitTest2()
		{
			var arr = new[] {1, 1, 1, 2, 1, 1, 1, 2, 3, 1};
			var parts = arr.Split(new[] {1, 2, 3}).ToArray();
			Assert.AreEqual(2, parts.Length);
			Assert.IsTrue(parts[0].SequenceEqual(new[] {1, 1, 1, 2, 1, 1}));
			Assert.IsTrue(parts[1].SequenceEqual(new[] {1}));
		}
	}
}
