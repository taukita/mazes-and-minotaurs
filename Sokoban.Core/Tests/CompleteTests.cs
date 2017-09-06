using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sokoban.Core.Tests
{
	[TestFixture]
	internal class CompleteTests
	{
		[Test]
		public void LevelShouldBeNotCompleted()
		{
			const string levelData = @"
#####
#.$@#
#.$-#
#####";
			var level = new LevelCreator().Create(levelData);
			Assert.IsFalse(level.IsCompleted);
		}

		[Test]
		public void LevelShouldBeCompleted()
		{
			const string levelData = @"
#####
#.$@#
#.$-#
#####";
			var level = new LevelCreator().Create(levelData);
			level.TryMoveLeft();
			level.TryMoveRight();
			level.TryMoveDown();
			level.TryMoveLeft();
			Assert.IsTrue(level.IsCompleted);
		}
	}
}
