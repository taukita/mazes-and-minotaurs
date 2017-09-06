using System;
using System.Collections.Generic;

namespace Sokoban.Core
{
	internal sealed class LevelCreator
	{
		private readonly ILevelFormat _levelFormat;

		public LevelCreator(ILevelFormat levelFormat = null)
		{
			_levelFormat = levelFormat ?? new LevelFormat();
		}

		public Level Create(string levelData)
		{
			return Create(levelData.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries));
		}

		public Level Create(IEnumerable<string> levelData)
		{
			var level = new Level();

			var width = 0;
			var height = 0;

			foreach (var line in levelData)
			{
				var x = 0;
				foreach (var @char in line)
				{
					if (@char == _levelFormat.Wall)
						level.Walls.Add(Tuple.Create(x, height));
					else if (@char == _levelFormat.Target)
						PlaceTarget(level, x, height);
					else if (@char == _levelFormat.Crate)
						PlaceCrate(level, x, height);
					else if (@char == _levelFormat.Player)
						PlacePlayer(level, x, height);
					var extendedLevelFormat = _levelFormat as IExtendedLevelFormat;
					if (extendedLevelFormat != null)
						if (@char == extendedLevelFormat.CrateOverTarget)
						{
							PlaceCrate(level, x, height);
							PlaceTarget(level, x, height);
						}
						else if (@char == extendedLevelFormat.PlayerOverTarget)
						{
							PlacePlayer(level, x, height);
							PlaceTarget(level, x, height);
						}
					x++;
				}
				height++;
				width = Math.Max(width, x);
			}

			level.Width = width;
			level.Height = height;

			return level;
		}

		private static void PlaceCrate(Level level, int x, int y)
		{
			level.Crates.Add(new Level.Crate {X = x, Y = y});
		}

		private static void PlacePlayer(Level level, int x, int y)
		{
			level.PlayerX = x;
			level.PlayerY = y;
		}

		private static void PlaceTarget(Level level, int x, int y)
		{
			level.Targets.Add(Tuple.Create(x, y));
		}

		internal class LevelFormat : ILevelFormat
		{
			public char Crate { get; } = '$';
			public char Empty { get; } = '-';
			public char Player { get; } = '@';
			public char Target { get; } = '.';
			public char Wall { get; } = '#';
		}
	}
}