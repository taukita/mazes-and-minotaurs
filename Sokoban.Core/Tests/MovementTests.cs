using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sokoban.Core.Tests
{
	[TestFixture]
	internal class MovementTests
	{
		[Test]
		public void BasicTest()
		{
			const string step0 = @"####
#-@#
#--#
####";
			const string step1 = @"####
#@-#
#--#
####";
			const string step2 = @"####
#--#
#@-#
####";
			const string step3 = @"####
#--#
#-@#
####";
			var level = new LevelCreator().Create(step0);
			TestStep(level, level.TryMoveLeft, step1);
			TestStep(level, level.TryMoveDown, step2);
			TestStep(level, level.TryMoveRight, step3);
			TestStep(level, level.TryMoveUp, step0);
		}

		[Test]
		public void WallsShouldBeImpassable()
		{
			const string levelData = @"###
#@#
###";
			NoMovement(levelData);
		}

		[Test]
		public void CratesShouldBeImpassable()
		{
			const string levelData = @"#####
#-$-#
#$@$#
#-$-#
#####";
			NoMovement(levelData);
		}

		[Test]
		public void CratesShouldBeMovable()
		{
			const string step0 = @"-###-
-#-#-
##$##
#-$@#
#####";
			const string step1 = @"-###-
-#-#-
##$##
#$@-#
#####";
			const string step2 = @"-###-
-#$#-
##@##
#$--#
#####";
			var level = new LevelCreator().Create(step0);
			TestStep(level, level.TryMoveLeft, step1);
			TestStep(level, level.TryMoveUp, step2);
		}

		[Test]
		public void LevelBordersShouldBeImpassable()
		{
			const string input = "@";
			var level = new LevelCreator().Create(input);
			Assert.AreEqual(1, level.Width);
			Assert.AreEqual(1, level.Height);
			var moved = level.TryMoveLeft() || level.TryMoveDown() || level.TryMoveRight() || level.TryMoveUp();
			Assert.IsFalse(moved);
		}

		private static void NoMovement(string levelData)
		{
			var level = new LevelCreator().Create(levelData);
			TestStep(level, level.TryMoveLeft, levelData, Assert.IsFalse);
			TestStep(level, level.TryMoveDown, levelData, Assert.IsFalse);
			TestStep(level, level.TryMoveRight, levelData, Assert.IsFalse);
			TestStep(level, level.TryMoveUp, levelData, Assert.IsFalse);
		}

		private static void TestStep(Level level, Func<bool> stepFunc, string stepResult, Action<bool> assert = null)
		{
			assert = assert ?? Assert.IsTrue;
			var moved = stepFunc();
			assert(moved);
			Assert.AreEqual(stepResult, level.ToString());
		}
	}
}
