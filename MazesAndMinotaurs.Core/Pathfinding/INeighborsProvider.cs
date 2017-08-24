using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core.Pathfinding
{
	public interface INeighborsProvider<T>
	{
		IEnumerable<T> Get(T current);
	}
}
