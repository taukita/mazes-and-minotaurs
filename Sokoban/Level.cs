using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
	internal class Level
	{
		private HashSet<Tuple<int, int>> _targets = new HashSet<Tuple<int, int>>();

		public HashSet<Tuple<int, int>> Targets
		{
			get
			{
				return _targets;
			}
		}

		private HashSet<Tuple<int, int>> _walls = new HashSet<Tuple<int, int>>();

		public HashSet<Tuple<int, int>> Walls
		{
			get
			{
				return _walls;
			}
		}

		private List<Crate> _crates = new List<Crate>();

		public List<Crate> Crates
		{
			get
			{
				return _crates;
			}
		}

		private int _playerX;

		public int PlayerX
		{
			get
			{
				return _playerX;
			}
		}

		private int _playerY;

		public int PlayerY
		{
			get
			{
				return _playerY;
			}
		}

		private int _width;
		private int _height;

		public static Level FromString(string @string)
		{
			var level = new Level();
			int? width = null;
			bool playerSet = false;
			var y = 0;
			var lines = @string.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var line in lines)
			{
				var x = 0;
				foreach(var @char in line)
				{
					switch (@char)
					{
						case '#':
							level._walls.Add(Tuple.Create(x, y));
							break;
						case 't':
						case 'T':
							level._targets.Add(Tuple.Create(x, y));
							break;
						case 'c':
						case 'C':
							level._crates.Add(new Crate { X = x, Y = y });
							break;
						case '@':
							if (playerSet)
							{
								throw new Exception("Only one player allowed.");
							}
							level._playerX = x;
							level._playerY = y;
							playerSet = true;
							break;
					}
					x++;
				}
				if (width.HasValue && width.Value != x)
				{
					throw new Exception("All lines must be of same width.");
				}
				else
				{
					width = x;
				}
				y++;
			}
			level._width = width.Value;
			level._height = y;
			return level;
		}

		public class Crate
		{
			public int X { get; set; }
			public int Y { get; set; }
		}
	}
}
