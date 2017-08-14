using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Sokoban.Core.Properties;

namespace Sokoban.Core
{
	internal class LevelProvider : ILevelProvider
	{
		private readonly XDocument _data;

		public static Level TestLevel
		{
			get
			{
				const string data = @"
#######
#.-$--#
#-#-#-#
#--@--#
#-#-#-#
#.-$--#
#######";
				var level = GetLevel(data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries));
				level.Index = -1;
				return level;
			}
		}

		public LevelProvider()
		{
			_data = XDocument.Parse(Encoding.UTF8.GetString(Resources.Original));
			Count = _data.Root?.Element("LevelCollection")?.Elements("Level").Count() ?? 0;
		}

		public int Count { get; }

		public Level GetLevel(int index)
		{
			if (index == -1)
				return TestLevel;
			var levelData = _data.Root?.Element("LevelCollection")?.Elements("Level").ElementAt(index);
			if (levelData == null)
				throw new ArgumentNullException(nameof(levelData));
			var level = GetLevel(levelData.Elements("L").Select(e => e.Value));
			level.Index = index;
			return level;
		}

		internal static Level GetLevel(IEnumerable<string> lines)
		{
			var level = new Level {ProviderType = typeof(LevelProvider)};

			var width = 0;
			var height = 0;

			foreach (var line in lines)
			{
				var x = 0;
				foreach (var @char in line)
				{
					switch (@char)
					{
						case '#':
							level.Walls.Add(Tuple.Create(x, height));
							break;
						case '.':
							level.Targets.Add(Tuple.Create(x, height));
							break;
						case '$':
							level.Crates.Add(new Level.Crate { X = x, Y = height });
							break;
						case '@':
							level.PlayerX = x;
							level.PlayerY = height;
							break;
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
	}
}