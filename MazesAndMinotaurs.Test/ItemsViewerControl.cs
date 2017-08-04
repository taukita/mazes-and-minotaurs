using MazesAndMinotaurs.ConsoleTarget.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.ConsoleTarget.Ui.Events;

namespace MazesAndMinotaurs.Test
{
	class ItemsViewerControl : Control
	{
		private WallsControl _walls;
		private MenuControl _menu;
		private TextControl _text;
		private Dictionary<string, string> _items = new Dictionary<string, string>();

		public void AddItem(string header, string body)
		{
			_items.Add(header, body);
		}

		protected override void Drawing(ITerminal<char, ConsoleColor> terminal)
		{
			var width = _items.Keys.Max(k => k.Length) + 2;
			CreateMenu(width);
			CreateText(width);
			CreateWalls(width);

			_menu.Draw(terminal);
			_text.Draw(terminal);
			_walls.Draw(terminal);
		}

		private void CreateMenu(int width)
		{
			if (_menu != null && _menu.Width == width)
			{
				return;
			}
			if (_menu != null)
			{
				_menu.OnSelectionChanged -= MenuOnSelectionChanged;
			}
			_menu = new MenuControl(_items.Keys);
			_menu.Left = Left + 1;
			_menu.Top = Top + 1;
			_menu.Width = width;
			_menu.Height = Height - 2;
			_menu.OnSelectionChanged += MenuOnSelectionChanged;
			_menu.IsFocused = true;
		}

		private void CreateText(int width)
		{
			if (_text == null)
			{
				_text = new TextControl();
				_text.Text = _items.Values.First();
			}
			_text.Left = Left + 1 + width + 1;
			_text.Top = Top + 1;
			_text.Width = Width - 2 - 1 - width;
			_text.Height = Height - 2;			
		}

		private void CreateWalls(int width)
		{
			if (_walls != null)
			{
				return;
			}
			var walls = new HashSet<Tuple<int, int>>();
			for (var i = 0; i < Width; i++)
			{
				walls.Add(Tuple.Create(i, 0));
				walls.Add(Tuple.Create(i, Height - 1));
			}
			for (var i = 0; i < Height; i++)
			{
				walls.Add(Tuple.Create(0, i));
				walls.Add(Tuple.Create(width + 1, i));
				walls.Add(Tuple.Create(Width - 1, i));
			}
			_walls = new WallsControl(walls);
			_walls.Left = Left;
			_walls.Top = Top;
			_walls.Width = Width;
			_walls.Height = Height;
			_walls.WallsTheme = WallsTheme.Box;			
		}

		private void MenuOnSelectionChanged(MenuControl menu, string old, string @new)
		{
			_text.Text = _items[@new];
		}

		protected override void KeyPressed(KeyPressedEventArgs args)
		{
			if (args.Key == ConsoleKey.Escape)
			{
				IsFocused = false;
				return;
			}
			_menu.NotifyKeyPressed(args.Key);
		}
	}
}
