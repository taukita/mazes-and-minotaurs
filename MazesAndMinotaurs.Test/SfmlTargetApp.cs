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
			pages.Controls.Add(CreatePage2(renderWindow));

			return pages;
		}

		private Control<char, Color, Keyboard.Key> CreatePage1(RenderWindow renderWindow)
		{
			var menu = new Menu<char, Color, Keyboard.Key>(KeyboardAdapter.Instance, '…', '>');
			var page2Item = menu.AddItem("2nd page");
			var page3Item = menu.AddItem("3rd page");
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

			border.BorderTheme = new BorderTheme<char>('*');
			border.ColorTheme = new ColorTheme<Color>(Color.Black, Color.White);

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

			border.BorderTheme = new BorderTheme<char>('*');
			border.ColorTheme = new ColorTheme<Color>(Color.Black, Color.White);

			return border;
		}

		private class KeyboardAdapter : IKeyboardAdapter<Keyboard.Key>
		{
			public static readonly KeyboardAdapter Instance = new KeyboardAdapter();

			public bool isDown(Keyboard.Key key)
			{
				return key == Keyboard.Key.Down;
			}

			public bool isEnter(Keyboard.Key key)
			{
				return key == Keyboard.Key.Return;
			}

			public bool isLeft(Keyboard.Key key)
			{
				return key == Keyboard.Key.Left;
			}

			public bool isRight(Keyboard.Key key)
			{
				return key == Keyboard.Key.Right;
			}

			public bool isUp(Keyboard.Key key)
			{
				return key == Keyboard.Key.Up;
			}
		}
	}
}
