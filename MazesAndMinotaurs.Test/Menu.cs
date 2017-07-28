using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.Test
{
	class Menu<TItem>
	{
		private IEnumerable<TItem> _items;
		private ITerminal<char, ConsoleColor> _terminal;
		private TItem _selectedItem;

		public Menu(IEnumerable<TItem> items, ITerminal<char, ConsoleColor> terminal)
		{
			_items = items;
			_terminal = terminal;
			_selectedItem = _items.First();
		}

		public int Top { get; set; }
		public int Left { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public void Draw()
		{
			int height = 2 + _items.Count();
			int itemWidth = Width - 3;
			int i = 0;
			foreach (var item in _items)
			{
				_terminal.DrawString(Left + 2, Top + 1 + i, item.ToString().Fit(itemWidth, '~'),
					ConsoleColor.White, ConsoleColor.Black);
				if (item.Equals(_selectedItem))
				{
					_terminal.Draw(Left + 1, Top + 1 + i, '>', ConsoleColor.White, ConsoleColor.Black);
				}
				i++;
			}
			_terminal.DrawRectangle(Left, Top, Width, height, '#', ConsoleColor.White, ConsoleColor.Black);
		}
	}
}
