using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Adapters;
using MazesAndMinotaurs.Ui.Events;

namespace MazesAndMinotaurs.Ui.Controls
{
	public class Border<TGlyph, TColor, TKey> : Control<TGlyph, TColor, TKey>
	{
		private Control<TGlyph, TColor, TKey> _content;
		private bool _overrideThemes;

		public BorderTheme<TGlyph> BorderTheme { get; set; }

		public Border(Control<TGlyph, TColor, TKey> content = null, bool overrideThemes = true) : base(null)
		{
			_content = content;
			_overrideThemes = overrideThemes;
		}

		protected override void Drawing(ITerminal<TGlyph, TColor> terminal)
		{
			PrepareContent();
			Drawing(terminal, Left, Top, Width, Height, BorderTheme, ColorTheme);
			_content?.Draw(terminal);
		}

		protected override void FocusChanged(PropertyChangedExtendedEventArgs<bool> args)
		{
			if (args.NewValue && _content != null)
			{
				_content.IsFocused = true;
			}
		}

		protected override void KeyPressed(KeyPressedEventArgs<TKey> args)
		{
			_content?.NotifyKeyPressed(args.Key);
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

		private static void Drawing(ITerminal<TGlyph, TColor> terminal, int left, int top, int width, int height, 
			BorderTheme<TGlyph> borderTheme, ColorTheme<TColor> colorTheme)
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
