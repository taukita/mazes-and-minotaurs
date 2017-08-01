using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class ColorTheme
	{
		public static ColorTheme Create(ConsoleColor background, ConsoleColor foreground)
		{
			return new ColorTheme(background, foreground);
		}

		public static ColorTheme Create(ConsoleColor background, ConsoleColor foreground, 
			ConsoleColor focusedBackground, ConsoleColor focusedForeground)
		{
			return new FocusedAwareColorTheme(background, foreground,
				new ColorTheme(focusedBackground, focusedForeground));
		}

		protected ColorTheme(ConsoleColor background, ConsoleColor foreground)
		{
			Background = background;
			Foreground = foreground;
		}

		public ConsoleColor Background { get; }
		public ConsoleColor Foreground { get; }

		private class FocusedAwareColorTheme : ColorTheme, IFocusAwareObject<ColorTheme>
		{
			private ColorTheme _focusedTheme;

			public FocusedAwareColorTheme(ConsoleColor background, ConsoleColor foreground, ColorTheme focusedTheme) 
				: base(background, foreground)
			{
				_focusedTheme = focusedTheme;
			}

			public ColorTheme Focus(bool focus)
			{
				return focus ? _focusedTheme : this;
			}
		}
	}
}
