using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.SfmlTarget;
using MazesAndMinotaurs.Ui;
using MazesAndMinotaurs.Ui.Adapters;
using MazesAndMinotaurs.Ui.Controls;
using MazesAndMinotaurs.Ui.Controls.Containers;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Test
{
	internal class SfmlTargetApp
	{
		private const int CHARACTER_SIZE = 14;
		private const int WIDTH_IN_GLYPHS = 80;
		private const int HEIGHT_IN_GLYPHS = 30;

		public void Run()
		{
			var font = new Font("square.ttf");
			var text = new Text
			{
				CharacterSize = CHARACTER_SIZE,
				DisplayedString = "#",
				Font = font
			};
			var lb = text.GetLocalBounds();
			var glyphWidth = (uint)(2 * lb.Left + lb.Width);
			var glyphHeight = (uint)(2 * lb.Top + lb.Height);
		
			var renderWindow = new RenderWindow(
				new VideoMode(WIDTH_IN_GLYPHS * glyphWidth, HEIGHT_IN_GLYPHS * glyphHeight), "Test", Styles.Close | Styles.Titlebar);
			renderWindow.Closed += (s, e) => renderWindow.Close();
			var windowColor = new Color(0, 192, 255);

			var terminal = new SfmlTerminal(renderWindow, font, CHARACTER_SIZE, glyphWidth, glyphHeight);
			var control = CreateRootControl(renderWindow);
			control.IsFocused = true;

			renderWindow.KeyPressed += (s, e) => control.NotifyKeyPressed(e.Code);

			while (renderWindow.IsOpen)
			{
				renderWindow.DispatchEvents();
				renderWindow.Clear(windowColor);

				control.Draw(terminal);

				renderWindow.Display();
			}
		}

		private Control<char, Color, Keyboard.Key> CreateRootControl(RenderWindow renderWindow)
		{
			var pages = new Pages<char, Color, Keyboard.Key>(KeyboardAdapter.Instance);

			pages.Controls.Add(CreatePage1(renderWindow));
			pages.Controls.Add(CreatePage2(renderWindow));
			pages.Controls.Add(CreatePage3(renderWindow));
			pages.Controls.Add(CreatePage4(renderWindow));

			return pages;
		}

		private Control<char, Color, Keyboard.Key> CreatePage1(RenderWindow renderWindow)
		{
			var menu = new Menu<char, Color, Keyboard.Key>(KeyboardAdapter.Instance, '…', '>');
			var page2Item = menu.AddItem("2nd page");
			var page3Item = menu.AddItem("3rd page");
			var page4Item = menu.AddItem("4th page");
			var exitItem = menu.AddItem("Exit");

			menu.OnSelect += (m, i) =>
			{
				if (i == page2Item)
				{
					((Pages<char, Color, Keyboard.Key>)m.Parent.Parent).Page = 1;
				}
				else if (i == page3Item)
				{
					((Pages<char, Color, Keyboard.Key>)m.Parent.Parent).Page = 2;
				}
				else if (i == page4Item)
				{
					((Pages<char, Color, Keyboard.Key>)m.Parent.Parent).Page = 3;
				}
				else if (i == exitItem)
				{
					renderWindow.Close();
				}
			};

			var border = new Border<char, Color, Keyboard.Key>(menu);

			border.Left = 1;
			border.Top = 1;
			border.Width = 20;
			border.Height = 10;

			border.BorderTheme = new BorderTheme<char>('*', '-', '*', '|', '*', '-', '*', '|');
			border.ColorTheme = new ColorTheme<Color>(Color.Black, Color.White);

			border.Title = "Page 1";
			border.Ellipsis = '…';

			return border;
		}

		private Control<char, Color, Keyboard.Key> CreatePage2(RenderWindow renderWindow)
		{
			var menu = new Menu<char, Color, Keyboard.Key>(KeyboardAdapter.Instance, '…', '>');
			var page1Item = menu.AddItem("1st page");
			var exitItem = menu.AddItem("Exit");

			menu.OnSelect += (m, i) =>
			{
				if (i == page1Item)
				{
					((Pages<char, Color, Keyboard.Key>)m.Parent.Parent).Page = 0;
				}
				else if (i == exitItem)
				{
					renderWindow.Close();
				}
			};

			var border = new Border<char, Color, Keyboard.Key>(menu);

			border.Left = 1;
			border.Top = 1;
			border.Width = 20;
			border.Height = 10;

			border.BorderTheme = new BorderTheme<char>('*', '-', '*', '|', '*', '-', '*', '|');
			border.ColorTheme = new ColorTheme<Color>(Color.Black, Color.White);

			border.Title = "Page 2";
			border.Ellipsis = '…';

			return border;
		}

		private Control<char, Color, Keyboard.Key> CreatePage3(RenderWindow renderWindow)
		{
			var label = new Label<char, Color, Keyboard.Key>(KeyboardAdapter.Instance);
			label.Delimiter = Environment.NewLine;
			label.Text = @"This is just Label.
Press escape to return to page #1.";
			label.OnKeyPressed += (control, args) =>
				{
					if (args.Key == Keyboard.Key.Escape)
					{
						((Pages<char, Color, Keyboard.Key>) control.Parent.Parent).Page = 0;
					}
				};

			var border = new Border<char, Color, Keyboard.Key>(label);

			border.Left = 1;
			border.Top = 1;
			border.Width = 20;
			border.Height = 10;

			border.BorderTheme = new BorderTheme<char>('*', '-', '*', '|', '*', '-', '*', '|');
			border.ColorTheme = new ColorTheme<Color>(Color.Black, Color.White);

			border.Title = "Page 3";
			border.Ellipsis = '…';

			return border;
		}

		private Control<char, Color, Keyboard.Key> CreatePage4(RenderWindow renderWindow)
		{
			var borderTheme = new BorderTheme<char>('*', '-', '*', '|', '*', '-', '*', '|');
			var colorTheme1 = new ColorTheme<Color>(Color.Black, Color.Transparent);
			var colorTheme2 = new ColorTheme<Color>(Color.Black, Color.White);
			var grid = new Grid<char, Color, Keyboard.Key>(KeyboardAdapter.Instance)
				{
					Left = 1,
					Top = 1,
					Width = 30,
					Height = 20
				};
			grid.Columns.Add(1);
			grid.Columns.Add(2);
			grid.Rows.Add(1);
			grid.Rows.Add(1);
			grid.OnKeyPressed += (control, args) =>
			{
				if (args.Key == Keyboard.Key.Escape)
				{
					((Pages<char, Color, Keyboard.Key>)control.Parent).Page = 0;
				}
			};

			var border = new Border<char, Color, Keyboard.Key> {BorderTheme = borderTheme, ColorTheme = colorTheme1};
			border.OnFocusChanged += (s, a) => s.ColorTheme = a.NewValue ? colorTheme2 : colorTheme1;
			grid.Controls.Add(border);

			border = new Border<char, Color, Keyboard.Key> {BorderTheme = borderTheme, ColorTheme = colorTheme1};
			border.OnFocusChanged += (s, a) => s.ColorTheme = a.NewValue ? colorTheme2 : colorTheme1;
			grid.Controls.Add(border);

			border = new Border<char, Color, Keyboard.Key> {BorderTheme = borderTheme, ColorTheme = colorTheme1};
			border.OnFocusChanged += (s, a) => s.ColorTheme = a.NewValue ? colorTheme2 : colorTheme1;
			grid.Controls.Add(border);

			var label = new Label<char, Color, Keyboard.Key>(KeyboardAdapter.Instance)
				{
					Delimiter = Environment.NewLine,
					Text = "Press escape to return to page #1."
				};

			border = new Border<char, Color, Keyboard.Key>(label) {BorderTheme = borderTheme, ColorTheme = colorTheme1};
			border.OnFocusChanged += (s, a) => s.ColorTheme = a.NewValue ? colorTheme2 : colorTheme1;
			grid.Controls.Add(border);

			return grid;
		}

		private class KeyboardAdapter : IKeyboardAdapter<Keyboard.Key>
		{
			public static readonly KeyboardAdapter Instance = new KeyboardAdapter();

			public bool IsDown(Keyboard.Key key)
			{
				return key == Keyboard.Key.Down;
			}

			public bool IsEnter(Keyboard.Key key)
			{
				return key == Keyboard.Key.Return;
			}

			public bool IsTab(Keyboard.Key key)
			{
				return key == Keyboard.Key.Tab;
			}

			public bool IsLeft(Keyboard.Key key)
			{
				return key == Keyboard.Key.Left;
			}

			public bool IsRight(Keyboard.Key key)
			{
				return key == Keyboard.Key.Right;
			}

			public bool IsUp(Keyboard.Key key)
			{
				return key == Keyboard.Key.Up;
			}
		}
	}
}
