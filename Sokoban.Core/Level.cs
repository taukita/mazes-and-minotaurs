using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Sokoban.Core
{
	internal class Level
	{
		private readonly Stack<Step> _steps = new Stack<Step>();
		public List<Crate> Crates { get; } = new List<Crate>();
		public int Height { get; set; }
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

		public int PlayerX { get; set; }
		public int PlayerY { get; set; }
		public Type ProviderType { get; set; }
		public HashSet<Tuple<int, int>> Targets { get; } = new HashSet<Tuple<int, int>>();
		public HashSet<Tuple<int, int>> Walls { get; } = new HashSet<Tuple<int, int>>();
		public int Width { get; set; }

		public static Level Load(string filename)
		{
			using (var stream = File.Open(filename, FileMode.Open))
			{
				var formatter = new BinaryFormatter();
				var data = (LevelData) formatter.Deserialize(stream);
				var levelProvider = (ILevelProvider) Activator.CreateInstance(data.ProviderType);
				return levelProvider.GetLevel(data.Index).Load(data);
			}
		}

		public void Save(string filename)
		{
			using (var stream = File.Open(filename, FileMode.Create))
			{
				var formatter = new BinaryFormatter();
				var data = new LevelData
				{
					Crates = Crates,
					Index = Index,
					PlayerX = PlayerX,
					PlayerY = PlayerY,
					ProviderType = ProviderType
				};
				formatter.Serialize(stream, data);
			}
		}

		public override string ToString()
		{
			return ToString(new LevelCreator.LevelFormat());
		}

		public string ToString(ILevelFormat format)
		{
			var data = new char[Height][];
			for (var y = 0; y < Height; y++)
				data[y] = new char[Width];

			foreach (var target in Targets)
				data[target.Item2][target.Item1] = format.Target;

			foreach (var wall in Walls)
				data[wall.Item2][wall.Item1] = format.Wall;

			foreach (var crate in Crates)
				if (data[crate.Y][crate.X] == format.Target)
				{
					var extendedFormat = format as IExtendedLevelFormat;
					if (extendedFormat == null)
						throw new InvalidOperationException("Cannot transform Level to string without extended format.");
					data[crate.Y][crate.X] = extendedFormat.CrateOverTarget;
				}
				else
				{
					data[crate.Y][crate.X] = format.Crate;
				}

			if (data[PlayerY][PlayerX] == format.Target)
			{
				var extendedFormat = format as IExtendedLevelFormat;
				if (extendedFormat == null)
					throw new InvalidOperationException("Cannot transform Level to string without extended format.");
				data[PlayerY][PlayerX] = extendedFormat.PlayerOverTarget;
			}
			else
			{
				data[PlayerY][PlayerX] = format.Player;
			}

			var sb = new StringBuilder((Width + Environment.NewLine.Length) * Height);
			for (var y = 0; y < Height; y++)
				sb.AppendLine(new string(data[y].Select(@char => @char == '\0' ? format.Empty : @char).ToArray()));
			return sb.ToString().Trim();
		}

		public bool TryMoveDown()
		{
			return TryMove(0, 1);
		}

		public bool TryMoveLeft()
		{
			return TryMove(-1, 0);
		}

		public bool TryMoveRight()
		{
			return TryMove(1, 0);
		}

		public bool TryMoveUp()
		{
			return TryMove(0, -1);
		}

		public bool Undo()
		{
			if (_steps.Any())
			{
				var step = _steps.Pop();
				PlayerX = step.OldPlayerX;
				PlayerY = step.OldPlayerY;
				if (step.Crate != null)
				{
					step.Crate.X = step.OldCrateX;
					step.Crate.Y = step.OldCrateY;
				}
				return true;
			}
			return false;
		}

		private Level Load(LevelData data)
		{
			Crates.Clear();
			Crates.AddRange(data.Crates);
			PlayerX = data.PlayerX;
			PlayerY = data.PlayerY;
			return this;
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

			if (x < 0 || x >= Width || y < 0 || y >= Height)
				return false;

			//Wall
			if (Walls.Any(wall => wall.Item1 == x && wall.Item2 == y))
				return false;

			var step = new Step();

			//Crate
			var crate = Crates.FirstOrDefault(c => c.X == x && c.Y == y);
			if (crate != null)
			{
				var x0 = x + dx;
				var y0 = y + dy;
				if (Walls.Any(wall => wall.Item1 == x0 && wall.Item2 == y0) || Crates.Any(c => c.X == x0 && c.Y == y0))
					return false;

				step.Crate = crate;
				step.OldCrateX = crate.X;
				step.OldCrateY = crate.Y;

				crate.X += dx;
				crate.Y += dy;
			}

			step.OldPlayerX = PlayerX;
			step.OldPlayerY = PlayerY;
			_steps.Push(step);
			PlayerX += dx;
			PlayerY += dy;
			return true;
		}

		[Serializable]
		public class Crate
		{
			public int X { get; set; }
			public int Y { get; set; }
		}

		[Serializable]
		private class LevelData
		{
			public List<Crate> Crates { get; set; }
			public int Index { get; set; }
			public int PlayerX { get; set; }
			public int PlayerY { get; set; }
			public Type ProviderType { get; set; }
		}

		private class Step
		{
			public Crate Crate;
			public int OldCrateX;
			public int OldCrateY;
			public int OldPlayerX;
			public int OldPlayerY;
		}
	}
}