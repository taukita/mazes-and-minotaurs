using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sokoban.Core.Tests
{
	[TestFixture]
	internal class LevelToStringTests
	{
		[Test]
		public void BasicTest()
		{
			const string input = @"
#####
#.$@-#
#####";
			const string output = @"#####-
#.$@-#
#####-";
			var level = new LevelCreator().Create(input);
			Assert.AreEqual(output, level.ToString());
		}

		[Test]
		public void CustomFormatTest()
		{
			const string input = @"
#####
#.$@-#
#####";
			const string output = @"wwwww-
wtcp-w
wwwww-";
			var level = new LevelCreator().Create(input);
			Assert.AreEqual(output, level.ToString(new TestLevelFormat()));
		}

		[Test]
		public void CustomExtendedFormatTest()
		{
			const string input = @"
wwwww
wtCP-w
wwwww";

			const string output = @"wwwww-
wtCP-w
wwwww-";
			var level = new LevelCreator(new TestExtendedLevelFormat()).Create(input);
			Assert.AreEqual(output, level.ToString(new TestExtendedLevelFormat()));
		}
	}
}
