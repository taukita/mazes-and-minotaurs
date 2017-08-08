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
	public class Menu<TGlyph, TColor, TKey> : Control<TGlyph, TColor, TKey>
	{
		private readonly List<MenuItem> _items = new List<MenuItem>();
		private int _selectedItemIndex;
		private int? _unselectedItemIndex;

		private TGlyph _ellipsisGlyph;
		private TGlyph _selectionGlyph;

		public Menu(IKeyboardAdapter<TKey> keyboardAdapter, TGlyph ellipsisGlyph, TGlyph selectionGlyph)
			: base(keyboardAdapter)
		{
			_ellipsisGlyph = ellipsisGlyph;
			_selectionGlyph = selectionGlyph;
		}

		public event Action<Menu<TGlyph, TColor, TKey>, MenuItem> OnSelect;
		public event Action<Menu<TGlyph, TColor, TKey>, MenuItem, MenuItem> OnSelectionChanged;

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
				terminal.DrawString(Left + 1, Top + i, item.Glyphs.Fit(itemWidth, _ellipsisGlyph),
					ColorTheme.Foreground, ColorTheme.Background);
				if (_unselectedItemIndex.HasValue && item == _items[_unselectedItemIndex.Value])
				{
					terminal.Clear(Left, Top + i);
					_unselectedItemIndex = null;
				}
				if (i == _selectedItemIndex)
				{
					terminal.Draw(Left, Top + i, _selectionGlyph, ColorTheme.Foreground, ColorTheme.Background);
				}
				i++;
			}
		}

		protected override void KeyPressed(KeyPressedEventArgs<TKey> args)
		{
			var key = args.Key;

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
