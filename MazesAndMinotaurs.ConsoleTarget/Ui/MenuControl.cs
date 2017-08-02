﻿using System;
using System.Collections.Generic;
using System.Linq;
using MazesAndMinotaurs.ConsoleTarget.Ui.Events;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class MenuControl : Control
	{
		private readonly string[] _items;
		private int _selectedItemIndex;
		private int? _unselectedItemIndex;

		public event Action<MenuControl, string> OnSelect;
		public event Action<MenuControl, string, string> OnSelectionChanged;

		public MenuControl(params string[] items)
		{
			_items = items;
		}

		public MenuControl(IEnumerable<string> items)
			: this(items.ToArray())
		{
		}

		protected override void Drawing(ITerminal<char, ConsoleColor> terminal)
		{
			int itemWidth = Width - 1;
			int i = 0;
			foreach (var item in _items)
			{
				if (i > Height)
				{
					throw new Exception();
				}
				terminal.DrawString(Left + 1, Top + i, item.Fit(itemWidth, '~'),
					ConsoleColor.White, ConsoleColor.Black);
				if (_unselectedItemIndex.HasValue && item == _items[_unselectedItemIndex.Value])
				{
					terminal.Draw(Left, Top + i, ' ', ConsoleColor.White, ConsoleColor.Black);
					_unselectedItemIndex = null;
				}
				if (i == _selectedItemIndex)
				{
					terminal.Draw(Left, Top + i, '>', ConsoleColor.White, ConsoleColor.Black);
				}
				i++;
			}			
		}

		protected override void KeyPressed(KeyPressedEventArgs args)
		{
			switch (args.Key)
			{
				case ConsoleKey.UpArrow:
					_unselectedItemIndex = _selectedItemIndex;
					_selectedItemIndex--;
					if (_selectedItemIndex < 0)
					{
						_selectedItemIndex = _items.Length - 1;
					}
					OnSelectionChanged?.Invoke(this, _items[_unselectedItemIndex.Value], _items[_selectedItemIndex]);
					break;
				case ConsoleKey.DownArrow:
					_unselectedItemIndex = _selectedItemIndex;
					_selectedItemIndex++;
					if (_selectedItemIndex >= _items.Length)
					{
						_selectedItemIndex = 0;
					}
					OnSelectionChanged?.Invoke(this, _items[_unselectedItemIndex.Value], _items[_selectedItemIndex]);
					break;
				case ConsoleKey.Enter:
					OnSelect?.Invoke(this, _items[_selectedItemIndex]);
					break;
			}
		}
	}
}
