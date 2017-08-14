using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Core
{
	internal interface ILevelProvider
	{
		Level GetLevel(int index);
	}
}
