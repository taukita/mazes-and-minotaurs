using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core
{
	public interface IGenerator
	{
		IEnumerable<Tuple<int, int>> Generate(int width, int height);
		IEnumerable<Tuple<int, int>> Generate(int width, int height, int seed);
	}
}
