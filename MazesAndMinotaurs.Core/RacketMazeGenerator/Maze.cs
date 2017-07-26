using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core.RacketMazeGenerator
{
	internal class Maze
	{
		public Maze()
		{
			Rooms = new HashSet<Room>();
			Corridors = new HashSet<Corridor>();
		}

		public Maze(IEnumerable<Room> rooms, IEnumerable<Corridor> corridors)
		{
			Rooms = new HashSet<Room>(rooms);
			Corridors = new HashSet<Corridor>(corridors);
		}

		public HashSet<Room> Rooms { get; }
		public HashSet<Corridor> Corridors { get; }

		public IEnumerable<Corridor> GetPotentialCorridors(int width, int height)
		{
			var result = new List<Corridor>();
			foreach (var room in Rooms)
			{
				Check(room, room.NorthRoom, result, width, height);
				Check(room, room.WestRoom, result, width, height);
				Check(room, room.SouthRoom, result, width, height);
				Check(room, room.EastRoom, result, width, height);
			}
			return result;
		}

		public Maze Join(Maze otherMaze, Corridor corridor)
		{
			var result = new Maze(Rooms.Union(otherMaze.Rooms), Corridors.Union(otherMaze.Corridors));
			result.Corridors.Add(corridor);
			return result;
		}

		private void Check(Room from, Room to, List<Corridor> result, int width, int height)
		{
			if (to.Item1 < 0 || to.Item1 > width - 1 || to.Item2 < 0 || to.Item2 > height - 1)
			{
				return;
			}

			if (!Rooms.Contains(to))
			{
				result.Add(new Corridor(from.Item1, from.Item2, to.Item1, to.Item2));
			}
		}
	}
}
