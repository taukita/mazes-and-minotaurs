using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class Menu : Control
	{
		private BorderControl _focusedBorder;
		private BorderControl _unfocusedBorder;

		private List<string> _items;
		private int _selectedItemIndex;
		private int? _unselectedItemIndex;

		public event Func<Menu, string, KeyPressedResult> OnSelect;
		public event Func<Menu, string, string, KeyPressedResult> OnSelectionChanged;

		public Menu(List<string> items)
		{
			_focusedBorder = new BorderControl { Foreground = ConsoleColor.Black, Background = ConsoleColor.White };
			_unfocusedBorder = new BorderControl { Foreground = ConsoleColor.White, Background = ConsoleColor.Black };

			_items = items;
		}

		public override void Draw(ITerminal<char, ConsoleColor> terminal)
		{
			var border = IsFocused ? _focusedBorder : _unfocusedBorder;
			border.Left = Left;
			border.Top = Top;
			border.Width = Width;
			border.Height = Height;

			border.Draw(terminal);

			int itemWidth = Width - 3;
			int i = 0;
			foreach (var item in _items)
			{
				if (i > Height - 2)
				{
					throw new Exception();
				}
				terminal.DrawString(Left + 2, Top + 1 + i, item.Fit(itemWidth, '~'),
					ConsoleColor.White, ConsoleColor.Black);
				if (_unselectedItemIndex.HasValue && item == _items[_unselectedItemIndex.Value])
				{
					terminal.Draw(Left + 1, Top + 1 + i, ' ', ConsoleColor.White, ConsoleColor.Black);
					_unselectedItemIndex = null;
				}
				if (i == _selectedItemIndex)
				{
					terminal.Draw(Left + 1, Top + 1 + i, '>', ConsoleColor.White, ConsoleColor.Black);
				}
				i++;
			}			
		}

		public override KeyPressedResult NotifyKeyPressed(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.UpArrow:
					_unselectedItemIndex = _selectedItemIndex;
					_selectedItemIndex--;
					if (_selectedItemIndex < 0)
					{
						_selectedItemIndex = _items.Count - 1;
					}
					return OnSelectionChanged?.Invoke(this, _items[_unselectedItemIndex.Value], _items[_selectedItemIndex]) ?? KeyPressedResult.DoNothing;
				case ConsoleKey.DownArrow:
					_unselectedItemIndex = _selectedItemIndex;
					_selectedItemIndex++;
					if (_selectedItemIndex >= _items.Count)
					{
						_selectedItemIndex = 0;
					}
					return OnSelectionChanged?.Invoke(this, _items[_unselectedItemIndex.Value], _items[_selectedItemIndex]) ?? KeyPressedResult.DoNothing;
				case ConsoleKey.Enter:
					return OnSelect?.Invoke(this, _items[_selectedItemIndex]) ?? KeyPressedResult.DoNothing;
				default:
					return base.NotifyKeyPressed(key);
			}
		}
	}
}
