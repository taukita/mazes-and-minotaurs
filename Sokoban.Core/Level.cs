using System;
using System.Collections.Generic;
using System.Linq;

namespace Sokoban.Core
{
	internal class Level
	{
		public int Height { get; set; }
		public int Width { get; set; }
		public HashSet<Tuple<int, int>> Targets { get; } = new HashSet<Tuple<int, int>>();
		public HashSet<Tuple<int, int>> Walls { get; } = new HashSet<Tuple<int, int>>();
		public List<Crate> Crates { get; } = new List<Crate>();
		public int PlayerX { get; set; }
		public int PlayerY { get; set; }
		public int Index { get; set; }

		public bool IsCompleted
		{
			get
			{
				var matrix = new bool[Width, Height];
				foreach (var target in Targets)
					matrix[target.Item1, target.Item2] = true;
				return Crates.All(crate => matrix[crate.X, crate.Y]);
			}
		}

		public static Level FromString(string @string)
		{
			var level = new Level();
			int? width = null;
			var playerSet = false;
			var y = 0;
			var lines = @string.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
			foreach (var line in lines)
			{
				var x = 0;
				foreach (var @char in line)
				{
					switch (@char)
					{
						case '#':
							level.Walls.Add(Tuple.Create(x, y));
							break;
						case 't':
						case 'T':
							level.Targets.Add(Tuple.Create(x, y));
							break;
						case 'c':
						case 'C':
							level.Crates.Add(new Crate {X = x, Y = y});
							break;
						case '@':
							if (playerSet)
								throw new Exception("Only one player allowed.");
							level.PlayerX = x;
							level.PlayerY = y;
							playerSet = true;
							break;
					}
					x++;
				}
				if (width.HasValue && width.Value != x)
					throw new Exception("All lines must be of same width.");
				width = x;
				y++;
			}
			if (!width.HasValue)
				throw new Exception("Source string cannot be empty.");
			level.Width = width.Value;
			level.Height = y;
			level.Validate();
			return level;
		}

		public bool TryMoveUp()
		{
			return TryMove(0, -1);
		}

		public bool TryMoveLeft()
		{
			return TryMove(-1, 0);
		}

		public bool TryMoveDown()
		{
			return TryMove(0, 1);
		}

		public bool TryMoveRight()
		{
			return TryMove(1, 0);
		}

		private bool TryMove(int dx, int dy)
		{
			//preconditions
			if (dx * dy != 0)
				throw new InvalidOperationException("Only horizontal or vertical movement allowed.");
			if (dx < -1 || dx > 1)
				throw new ArgumentException(nameof(dx));
			if (dy < -1 || dy > 1)
				throw new ArgumentException(nameof(dy));

			var x = PlayerX + dx;
			var y = PlayerY + dy;

			//Wall
			if (Walls.Any(wall => wall.Item1 == x && wall.Item2 == y))
				return false;

			//Crate
			var crate = Crates.FirstOrDefault(c => c.X == x && c.Y == y);
			if (crate != null)
			{
				var x0 = x + dx;
				var y0 = y + dy;
				if (Walls.Any(wall => wall.Item1 == x0 && wall.Item2 == y0) || Crates.Any(c => c.X == x0 && c.Y == y0))
					return false;
				crate.X += dx;
				crate.Y += dy;
			}

			PlayerX += dx;
			PlayerY += dy;
			return true;
		}

		private void Validate()
		{
			if (Targets.Count < Crates.Count)
				throw new InvalidOperationException("There must be target for every crate.");
		}

		public class Crate
		{
			public int X { get; set; }
			public int Y { get; set; }
		}
	}
}