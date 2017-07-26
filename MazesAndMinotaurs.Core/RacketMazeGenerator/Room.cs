using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core.RacketMazeGenerator
{
	internal class Room : Tuple<int, int>
	{
		public Room(int item1, int item2) : base(item1, item2)
		{
		}

		public Room NorthRoom => new Room(Item1, Item2 - 1);
		public Room WestRoom => new Room(Item1 - 1, Item2);
		public Room SouthRoom => new Room(Item1, Item2 + 1);
		public Room EastRoom => new Room(Item1 + 1, Item2);
	}
}
