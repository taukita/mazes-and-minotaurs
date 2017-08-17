using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Core
{
	internal interface ILevelFormat
	{
		char Crate { get; }
		char Empty { get; }
		char Player { get; }
		char Target { get; }
		char Wall { get; }
	}
}
