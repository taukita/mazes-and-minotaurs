using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core.Pathfinding
{
	public interface IDistanceProvider<in T>
	{
		double Get(T first, T second);
	}
}
