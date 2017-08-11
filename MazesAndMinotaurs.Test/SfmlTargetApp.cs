using System;
using MazesAndMinotaurs.SfmlTarget;
using MazesAndMinotaurs.Ui;
using MazesAndMinotaurs.Ui.Controls;
using MazesAndMinotaurs.Ui.Controls.Containers;
using SFML.Graphics;
using SFML.Window;

namespace MazesAndMinotaurs.Test
{
	internal class SfmlTargetApp
	{
		private const int CharacterSize = 14;
		private const int HeightInGlyphs = 30;
		private const int WidthInGlyphs = 80;

		public void Run()
		{
			var font = new Font("square.ttf");
			var text = new Text
				{
					CharacterSize = CharacterSize,
					DisplayedString = "#",
					Font = font
				};
			var lb = text.GetLocalBounds();
			var glyphWidth = (uint) (2*lb.Left + lb.Width);
			var glyphHeight = (uint) (2*lb.Top + lb.Height);

			var renderWindow = new RenderWindow(
				new VideoMode(WidthInGlyphs*glyphWidth, HeightInGlyphs*glyphHeight), "Test", Styles.Close | Styles.Titlebar);
			renderWindow.Closed += (s, e) => renderWindow.Close();
			var windowColor = new Color(0, 192, 255);

			var terminal = new SfmlTerminal(renderWindow, font, CharacterSize, glyphWidth, glyphHeight);
			var control = CreateRootControl(renderWindow);
			control.IsFocused = true;
			control.KeyboardAdapter = SfmlKeyboardAdapter.Instance;

			renderWindow.KeyPressed += (s, e) => control.NotifyKeyPressed(e.Code);

			while (renderWindow.IsOpen)
			{
				renderWindow.Clear(windowColor);
				renderWindow.DispatchEvents();

				control.Draw(terminal);

				renderWindow.Display();
			}
		}

		private static Control<char, Color, Keyboard.Key> CreatePage1(RenderWindow renderWindow)
		{
			var menu = new Menu<char, Color, Keyboard.Key>('…', '>');
			var page2Item = menu.AddItem("2nd page");
			var page3Item = menu.AddItem("3rd page");
			var page4Item = menu.AddItem("4th page");
			var exitItem = menu.AddItem("Exit");

			menu.OnSelect += (m, i) =>
				{
					if (i == page2Item)
					{
						ControlUtils<char, Color, Keyboard.Key>.FindParent<Pages<char, Color, Keyboard.Key>>(m).Page = 1;
					}
					else if (i == page3Item)
					{
						ControlUtils<char, Color, Keyboard.Key>.FindParent<Pages<char, Color, Keyboard.Key>>(m).Page = 2;
					}
					else if (i == page4Item)
					{
						ControlUtils<char, Color, Keyboard.Key>.FindParent<Pages<char, Color, Keyboard.Key>>(m).Page = 3;
					}
					else if (i == exitItem)
					{
						renderWindow.Close();
					}
				};

			var border = new Border<char, Color, Keyboard.Key>
				{
					Left = 1,
					Top = 1,
					Width = 20,
					Height = 10,
					BorderTheme = new BorderTheme<char>('*', '-', '*', '|', '*', '-', '*', '|'),
					ColorTheme = new ColorTheme<Color>(Color.Black, Color.White),
					Title = "Page 1",
					Ellipsis = '…'
				};
			border.Controls.Add(menu);
			return border;
		}

		private static Control<char, Color, Keyboard.Key> CreatePage2(RenderWindow renderWindow)
		{
			var menu = new Menu<char, Color, Keyboard.Key>('…', '>');
			var page1Item = menu.AddItem("1st page");
			var exitItem = menu.AddItem("Exit");

			menu.OnSelect += (m, i) =>
				{
					if (i == page1Item)
					{
						ControlUtils<char, Color, Keyboard.Key>.FindParent<Pages<char, Color, Keyboard.Key>>(m).Page = 0;
					}
					else if (i == exitItem)
					{
						renderWindow.Close();
					}
				};

			var border = new Border<char, Color, Keyboard.Key>
				{
					Left = 1,
					Top = 1,
					Width = 20,
					Height = 10,
					BorderTheme = new BorderTheme<char>('*', '-', '*', '|', '*', '-', '*', '|'),
					ColorTheme = new ColorTheme<Color>(Color.Black, Color.White),
					Title = "Page 2",
					Ellipsis = '…'
				};
			border.Controls.Add(menu);
			return border;
		}

		// ReSharper disable once UnusedParameter.Local
		private static Control<char, Color, Keyboard.Key> CreatePage3(RenderWindow renderWindow)
		{
			var label = new Label<char, Color, Keyboard.Key>
				{
					Delimiter = Environment.NewLine,
					Text = @"This is just Label.
Press escape to return to page #1."
				};
			label.OnKeyPressed += (control, args) =>
				{
					if (args.Key == Keyboard.Key.Escape)
					{
						ControlUtils<char, Color, Keyboard.Key>.FindParent<Pages<char, Color, Keyboard.Key>>(control).Page = 0;
					}
				};

			var border = new Border<char, Color, Keyboard.Key>
				{
					Left = 1,
					Top = 1,
					Width = 20,
					Height = 10,
					BorderTheme = new BorderTheme<char>('*', '-', '*', '|', '*', '-', '*', '|'),
					ColorTheme = new ColorTheme<Color>(Color.Black, Color.White),
					Title = "Page 3",
					Ellipsis = '…'
				};
			border.Controls.Add(label);
			return border;
		}

		// ReSharper disable once UnusedParameter.Local
		private static Control<char, Color, Keyboard.Key> CreatePage4(RenderWindow renderWindow)
		{
			var borderTheme = new BorderTheme<char>('*', '-', '*', '|', '*', '-', '*', '|');
			var colorTheme1 = new ColorTheme<Color>(Color.Black, Color.Transparent);
			var colorTheme2 = new ColorTheme<Color>(Color.Black, Color.White);
			var grid = new Grid<char, Color, Keyboard.Key>
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
						ControlUtils<char, Color, Keyboard.Key>.FindParent<Pages<char, Color, Keyboard.Key>>(control).Page = 0;
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

			var label = new Label<char, Color, Keyboard.Key>
				{
					Delimiter = Environment.NewLine,
					Text = "Press escape to return to page #1."
				};

			border = new Border<char, Color, Keyboard.Key> {BorderTheme = borderTheme, ColorTheme = colorTheme1};
			border.Controls.Add(label);
			border.OnFocusChanged += (s, a) => s.ColorTheme = a.NewValue ? colorTheme2 : colorTheme1;
			grid.Controls.Add(border);

			return grid;
		}

		private static Control<char, Color, Keyboard.Key> CreateRootControl(RenderWindow renderWindow)
		{
			var pages = new Pages<char, Color, Keyboard.Key>();

			pages.Controls.Add(CreatePage1(renderWindow));
			pages.Controls.Add(CreatePage2(renderWindow));
			pages.Controls.Add(CreatePage3(renderWindow));
			pages.Controls.Add(CreatePage4(renderWindow));

			return pages;
		}
	}
}