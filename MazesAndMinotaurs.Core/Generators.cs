using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core
{
	public static class Generators
	{
		public static readonly IGenerator RacketMazeGenerator = new RacketMazeGenerator.Generator();
	}
}
