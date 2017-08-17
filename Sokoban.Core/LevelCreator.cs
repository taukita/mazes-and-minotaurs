using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
					{
						level.Walls.Add(Tuple.Create(x, height));
					}
					else if (@char == _levelFormat.Target)
					{
						level.Targets.Add(Tuple.Create(x, height));
					}
					else if (@char == _levelFormat.Crate)
					{
						level.Crates.Add(new Level.Crate { X = x, Y = height });
					}
					else if (@char == _levelFormat.Player)
					{
						level.PlayerX = x;
						level.PlayerY = height;
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

		private class LevelFormat : ILevelFormat
		{
			public char Crate { get; } = '$';
			public char Player { get; } = '@';
			public char Target { get; } = '.';
			public char Wall { get; } = '#';
		}
	}
}
