﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class BorderControl : Control
	{
		protected override void Drawing(ITerminal<char, ConsoleColor> terminal)
		{
			Drawing(terminal, Left, Top, Width, Height, Focused(BorderTheme), Focused(ColorTheme));
		}

		private static void Drawing(ITerminal<char, ConsoleColor> terminal, int left, int top, int width, int height, BorderTheme borderTheme, ColorTheme colorTheme)
		{
			terminal.Draw(left, top, borderTheme.TopLeft, colorTheme.Foreground, colorTheme.Background);
			terminal.DrawLine(left + 1, top, left + width - 2, top, borderTheme.Top, colorTheme.Foreground, colorTheme.Background);

			terminal.Draw(left + width - 1, top, borderTheme.TopRight, colorTheme.Foreground, colorTheme.Background);
			terminal.DrawLine(left + width - 1, top + 1, left + width - 1, top + height - 2, borderTheme.Right, colorTheme.Foreground, colorTheme.Background);

			terminal.Draw(left + width - 1, top + height - 1, borderTheme.BottomRight, colorTheme.Foreground, colorTheme.Background);
			terminal.DrawLine(left + width - 2, top + height - 1, left + 1, top + height - 1, borderTheme.Bottom, colorTheme.Foreground, colorTheme.Background);

			terminal.Draw(left, top + height - 1, borderTheme.BottomLeft, colorTheme.Foreground, colorTheme.Background);
			terminal.DrawLine(left, top + height - 2, left, top + 1, borderTheme.Left, colorTheme.Foreground, colorTheme.Background);
		}
	}
}
