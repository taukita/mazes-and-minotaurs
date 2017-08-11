using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Sokoban.Core.Properties;

namespace Sokoban.Core
{
	internal class LevelProvider
	{
		private readonly XDocument _data;

		public LevelProvider()
		{
			_data = XDocument.Parse(Encoding.UTF8.GetString(Resources.Zone_26));
		}

		public Level GetLevel(int index)
		{
			var levelData = _data.Root?.Element("LevelCollection")?.Elements("Level").ElementAt(index);
			if (levelData == null)
				throw new ArgumentNullException(nameof(levelData));
			var level = GetLevel(levelData.Elements("L").Select(e => e.Value));
			level.Index = index;
			return level;
		}

		internal Level GetLevel(IEnumerable<string> lines)
		{
			var level = new Level();

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