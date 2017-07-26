using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core.RacketMazeGenerator
{
	internal class Corridor : Tuple<int, int, int, int>
	{
		public Corridor(int item1, int item2, int item3, int item4) : base(item1, item2, item3, item4)
		{
		}
	}
}
