using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.ConsoleTarget.Ui.Events;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class BorderControl : Control
	{
		private Control _content;
		private bool _overrideThemes;

		public BorderControl()
		{
		}

		public BorderControl(Control content, bool overrideThemes = true)
		{
			_content = content;
			_overrideThemes = overrideThemes;
		}

		public BorderTheme BorderTheme { get; set; } = new BorderTheme('#');

		public override bool IsFocused
		{
			get
			{
				return base.IsFocused;
			}

			set
			{
				if (_content != null)
				{
					_content.IsFocused = value;
				}
				base.IsFocused = value;
			}
		}

		protected override void Drawing(ITerminal<char, ConsoleColor> terminal)
		{
			PrepareContent();
			Drawing(terminal, Left, Top, Width, Height, Focused(BorderTheme), Focused(ColorTheme));
			_content?.Draw(terminal);
		}

		protected override void KeyPressed(KeyPressedEventArgs args)
		{
			_content?.NotifyKeyPressed(args.Key);
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

		private void PrepareContent()
		{
			if (_content != null)
			{
				_content.Left = Left + 1;
				_content.Top = Top + 1;
				_content.Width = Width - 2;
				_content.Height = Height - 2;
				if (_overrideThemes)
				{
					_content.ColorTheme = ColorTheme;
				}
			}			
		}
	}
}
