using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Ui
{
	public class ColorTheme<TColor>
	{
		public ColorTheme(TColor foreground, TColor background)
		{
			Foreground = foreground;
			Background = background;
		}

		public TColor Foreground { get; }
		public TColor Background { get; }
	}
}
