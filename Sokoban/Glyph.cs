using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
	internal class Glyph : Tuple<int, int>
	{
		public Glyph(int item1, int item2) : base(item1, item2)
		{
		}
	}
}
