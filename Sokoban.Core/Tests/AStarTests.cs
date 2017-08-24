using System;
using System.Collections.Generic;
using System.Linq;
using MazesAndMinotaurs.Core.Pathfinding;
using NUnit.Framework;

namespace Sokoban.Core.Tests
{
	[TestFixture]
	internal class AStarTests
	{
		[Test]
		public void BasicTest()
		{
			const string levelData = @"
##########
#-------.#
#-######-#
#------#-#
#------#-#
#---@--#-#
#------#-#
#------#-#
#--------#
##########";

			const string pathData = @"
##########
#-------*#
#-######*#
#------#*#
#------#*#
#---*--#*#
#---*--#*#
#---*--#*#
#---*****#
##########";
			var level = new LevelCreator().Create(levelData);
			var aStar = new AStarBuilder<Tuple<int, int>>().Neighbors(new TestNeighborsProvider(level)).Build();
			var path = aStar.Search(Tuple.Create(level.PlayerX, level.PlayerY), level.Targets.Single());
			Assert.NotNull(path);
			var result = Draw(level, path);
			Assert.AreEqual(pathData.Trim(), result);
		}

		[Test]
		public void ShouldPreferHorizontalMovement()
		{
			const string levelData = @"
##########
#-------.#
#-######-#
#------#-#
#------#-#
#---@--#-#
#------#-#
#------#-#
#--------#
##########";

			const string pathData = @"
##########
#********#
#*######-#
#*-----#-#
#****--#-#
#---*--#-#
#------#-#
#------#-#
#--------#
##########";
			var level = new LevelCreator().Create(levelData);
			var aStar = new AStarBuilder<Tuple<int, int>>()
				.Distance((f, s) => f.Item1 != s.Item1 ? 1 : 1.1)
				.Neighbors(new TestNeighborsProvider(level))
				.Build();
			var path = aStar.Search(Tuple.Create(level.PlayerX, level.PlayerY), level.Targets.Single());
			Assert.NotNull(path);
			var result = Draw(level, path);
			Assert.AreEqual(pathData.Trim(), result);
		}

		private static string Draw(Level level, IEnumerable<Tuple<int, int>> path)
		{
			var terminal = new TestTerminal(level.Width, level.Height, '-');
			foreach (var wall in level.Walls)
				terminal.Draw(wall.Item1, wall.Item2, '#');
			foreach (var target in level.Targets)
				terminal.Draw(target.Item1, target.Item2, '.');
			terminal.Draw(level.PlayerX, level.PlayerY, '@');
			foreach (var step in path)
				terminal.Draw(step.Item1, step.Item2, '*');
			return terminal.ToString();
		}

		private class TestNeighborsProvider : INeighborsProvider<Tuple<int, int>>
		{
			private readonly Level _level;

			public TestNeighborsProvider(Level level)
			{
				if (level.Crates.Any())
					throw new Exception("Level without crates expected");
				_level = level;
			}

			public IEnumerable<Tuple<int, int>> Get(Tuple<int, int> current)
			{
				var neighbor = Tuple.Create(current.Item1 - 1, current.Item2);
				if (Check(neighbor))
					yield return neighbor;
				neighbor = Tuple.Create(current.Item1 + 1, current.Item2);
				if (Check(neighbor))
					yield return neighbor;
				neighbor = Tuple.Create(current.Item1, current.Item2 - 1);
				if (Check(neighbor))
					yield return neighbor;
				neighbor = Tuple.Create(current.Item1, current.Item2 + 1);
				if (Check(neighbor))
					yield return neighbor;
			}

			private bool Check(Tuple<int, int> neighbor)
			{
				return !_level.Walls.Any(w => w.Equals(neighbor));
			}
		}
	}
}