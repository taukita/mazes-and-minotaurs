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
	}
}
