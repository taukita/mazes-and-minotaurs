using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core.RacketMazeGenerator
{
	internal class Generator : IGenerator
	{
		public IEnumerable<Tuple<int, int>> Generate(int width, int height)
		{
			return Generate(width, height, new Random());
		}

		public IEnumerable<Tuple<int, int>> Generate(int width, int height, int seed)
		{
			return Generate(width, height, new Random(seed));
		}

		private IEnumerable<Tuple<int, int>> Generate(int width, int height, Random random)
		{
			var maze = GenerateMaze(width, height, random);
			var result = new List<Tuple<int, int>>();
			for (var x = 0; x < 2 * width + 1; x++)
			{
				for (var y = 0; y < 2 * height + 1; y++)
				{
					if (Check(x, y, maze, width, height))
					{
						result.Add(Tuple.Create(x, y));
					}
				}
			}
			return result;
		}

		private bool Check(int x, int y, Maze maze, int width, int height)
		{
			if (x == 0 || x == 2 * width || y == 0 || y == 2 * height)
			{
				return true;
			}

			if (x % 2 == 0 && y % 2 == 0)
			{
				return true;
			}

			if (x % 2 == 1 && y % 2 == 1)
			{
				var room = new Room((x - 1) / 2, (y - 1) / 2);
				return !maze.Rooms.Any(r => r.Equals(room));
			}

			if (x % 2 == 1 && y % 2 == 0)
			{
				var cx = (x - 1) / 2;
				var cy1 = (y - 2) / 2;
				var cy2 = y / 2;
				var corridor1 = new Corridor(cx, cy1, cx, cy2);
				var corridor2 = new Corridor(cx, cy2, cx, cy1);
				return !maze.Corridors.Any(c => c.Equals(corridor1) || c.Equals(corridor2));
			}

			if (x % 2 == 0 && y % 2 == 1)
			{
				var cx1 = (x - 2) / 2;
				var cx2 = x / 2;
				var cy = (y - 1) / 2;
				var corridor1 = new Corridor(cx1, cy, cx2, cy);
				var corridor2 = new Corridor(cx2, cy, cx1, cy);
				return !maze.Corridors.Any(c => c.Equals(corridor1) || c.Equals(corridor2));
			}

			throw new Exception();
		}

		private Maze GenerateMaze(int width, int height, Random random)
		{
			var mazes = new List<Maze>();

			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					var maze = new Maze();
					maze.Rooms.Add(new Room(x, y));
					mazes.Add(maze);
				}
			}

			while (mazes.Count > 1)
			{
				var maze1 = mazes[random.Next(mazes.Count)];
				mazes.Remove(maze1);
				var corridors = maze1.GetPotentialCorridors(width, height).ToArray();
				var corridor = corridors[random.Next(corridors.Length)];
				var toRoom = new Room(corridor.Item3, corridor.Item4);
				var maze2 = mazes.Single(m => m.Rooms.Any(r => r.Equals(toRoom)));
				mazes.Remove(maze2);
				mazes.Add(maze1.Join(maze2, corridor));
			}

			return mazes[0];
		}
	}
}
