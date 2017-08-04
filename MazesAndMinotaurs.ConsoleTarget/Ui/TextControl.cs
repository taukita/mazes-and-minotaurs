using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class TextControl : Control
	{
		public string Text { get; set; }

		protected override void Drawing(ITerminal<char, ConsoleColor> terminal)
		{
			var y = 0;
			var lines = Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
			foreach (var line in lines)
			{
				var parts = line.Split(Width);
				foreach(var part in parts)
				{
					terminal.DrawString(Left, Top + y, part, ColorTheme.Foreground, ColorTheme.Background);
					y++;
					if (y >= Height)
					{
						break;
					}
				}
				if (y >= Height)
				{
					break;
				}
			}
		}
	}
}
