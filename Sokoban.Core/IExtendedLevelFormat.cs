using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Core
{
	internal interface IExtendedLevelFormat : ILevelFormat
	{
		char CrateOverTarget { get; }
		char PlayerOverTarget { get; }
	}
}
