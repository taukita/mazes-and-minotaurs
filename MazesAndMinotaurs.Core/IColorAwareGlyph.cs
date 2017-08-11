using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core
{
	public interface IColorAwareGlyph<out TColor>
	{
		TColor Foreground { get; }
		TColor Background { get; }
	}
}
