﻿using System;
using System.Collections.Generic;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Events;

namespace MazesAndMinotaurs.Ui.Controls
{
	public class Menu<TGlyph, TColor, TInput> : Control<TGlyph, TColor, TInput>
	{
		private readonly List<MenuItem> _items = new List<MenuItem>();
		private int _selectedItemIndex;
		private int? _unselectedItemIndex;

		public event Action<Menu<TGlyph, TColor, TInput>, MenuItem> OnSelect;
		public event Action<Menu<TGlyph, TColor, TInput>, MenuItem, MenuItem> OnSelectionChanged;

		public TGlyph EllipsisGlyph { get; set; }
		public TGlyph SelectionGlyph { get; set; }

		public MenuItem AddItem(IEnumerable<TGlyph> glyphs)
		{
			var item = new MenuItem(glyphs);
			_items.Add(item);
			return item;
		}

		protected override void Drawing(ITerminal<TGlyph, TColor> terminal)
		{
			int itemWidth = Width - 1;
			int i = 0;
			foreach (var item in _items)
			{
				if (i > Height)
				{
					throw new Exception();
				}
				terminal.DrawString(Left + 1, Top + i, item.Glyphs.Fit(itemWidth, EllipsisGlyph),
					ColorTheme.Foreground, ColorTheme.Background);
				if (_unselectedItemIndex.HasValue && item == _items[_unselectedItemIndex.Value])
				{
					terminal.Clear(Left, Top + i);
					_unselectedItemIndex = null;
				}
				if (i == _selectedItemIndex)
				{
					terminal.Draw(Left, Top + i, SelectionGlyph, ColorTheme.Foreground, ColorTheme.Background);
				}
				i++;
			}
		}

		protected override void FocusChanged(PropertyChangedExtendedEventArgs<bool> args)
		{
			if (args.NewValue)
			{
				_selectedItemIndex = 0;
			}
		}

		protected override void KeyboardInput(InputEventArgs<TInput> args)
		{
			var key = args.Input;

			if (KeyboardAdapter.IsUp(key))
			{
				_unselectedItemIndex = _selectedItemIndex;
				_selectedItemIndex--;
				if (_selectedItemIndex < 0)
				{
					_selectedItemIndex = _items.Count - 1;
				}
				OnSelectionChanged?.Invoke(this, _items[_unselectedItemIndex.Value], _items[_selectedItemIndex]);
			}
			else if (KeyboardAdapter.IsDown(key))
			{
				_unselectedItemIndex = _selectedItemIndex;
				_selectedItemIndex++;
				if (_selectedItemIndex >= _items.Count)
				{
					_selectedItemIndex = 0;
				}
				OnSelectionChanged?.Invoke(this, _items[_unselectedItemIndex.Value], _items[_selectedItemIndex]);
			}
			else if (KeyboardAdapter.IsEnter(key))
			{
				OnSelect?.Invoke(this, _items[_selectedItemIndex]);
			}
		}

		protected override void MouseInput(InputEventArgs<TInput> args)
		{
			if (MouseAdapter != null)
			{
				var x = MouseAdapter.GetX(args.Input);
				var y = MouseAdapter.GetY(args.Input);
				if (x >= Left && x < Left + Width && y >= Top && y < Top + Width)
				{
					if (y - Top != _selectedItemIndex)
					{
						_unselectedItemIndex = _selectedItemIndex;
						_selectedItemIndex = y - Top;
					}
				}
			}
		}

		public class MenuItem
		{
			public MenuItem(IEnumerable<TGlyph> glyphs)
			{
				Glyphs = glyphs;
			}

			public IEnumerable<TGlyph> Glyphs { get; }
		}
	}
}