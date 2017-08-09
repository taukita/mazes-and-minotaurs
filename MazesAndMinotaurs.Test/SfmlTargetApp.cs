using System;
using MazesAndMinotaurs.SfmlTarget;
using MazesAndMinotaurs.SfmlTarget.Ui;
using SFML.Graphics;
using SFML.Window;
using Control =
	MazesAndMinotaurs.Ui.Control<MazesAndMinotaurs.SfmlTarget.Ui.SfmlGlyph, SFML.Graphics.Color, SFML.Window.Keyboard.Key>;
using ControlUtils =
	MazesAndMinotaurs.Ui.ControlUtils<MazesAndMinotaurs.SfmlTarget.Ui.SfmlGlyph, SFML.Graphics.Color, SFML.Window.Keyboard.Key>;
using MazesAndMinotaurs.Ui;

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

		private static Control CreatePage1(RenderWindow renderWindow)
		{
			var menu = new Menu();
			var page2Item = menu.AddItem("2nd page");
			var page3Item = menu.AddItem("3rd page");
			var page4Item = menu.AddItem("4th page");
			var exitItem = menu.AddItem("Exit");

			menu.OnSelect += (m, i) =>
				{
					if (i == page2Item)
					{
						ControlUtils.FindParent<Pages>(m).Page = 1;
					}
					else if (i == page3Item)
					{
						ControlUtils.FindParent<Pages>(m).Page = 2;
					}
					else if (i == page4Item)
					{
						ControlUtils.FindParent<Pages>(m).Page = 3;
					}
					else if (i == exitItem)
					{
						renderWindow.Close();
					}
				};

			var border = new Border
				{
					Left = 1,
					Top = 1,
					Width = 20,
					Height = 10,
					Title = SfmlGlyph.FromString("Page 1"),
				};
			border.Controls.Add(menu);
			return border;
		}

		private static Control CreatePage2(RenderWindow renderWindow)
		{
			var menu = new Menu();
			var page1Item = menu.AddItem("1st page");
			var exitItem = menu.AddItem("Exit");

			menu.OnSelect += (m, i) =>
				{
					if (i == page1Item)
					{
						ControlUtils.FindParent<Pages>(m).Page = 0;
					}
					else if (i == exitItem)
					{
						renderWindow.Close();
					}
				};

			var border = new Border
				{
					Left = 1,
					Top = 1,
					Width = 20,
					Height = 10,
					Title = SfmlGlyph.FromString("Page 2"),
				};
			border.Controls.Add(menu);
			return border;
		}

		// ReSharper disable once UnusedParameter.Local
		private static Control CreatePage3(RenderWindow renderWindow)
		{
			var label = new Label
				{
					Text = SfmlGlyph.FromString(@"This is just Label.
Press escape to return to page #1.")
				};
			label.OnKeyPressed += (control, args) =>
				{
					if (args.Key == Keyboard.Key.Escape)
					{
						ControlUtils.FindParent<Pages>(control).Page = 0;
					}
				};

			var border = new Border
				{
					Left = 1,
					Top = 1,
					Width = 20,
					Height = 10,
					Title = SfmlGlyph.FromString("Page 3"),
				};
			border.Controls.Add(label);
			return border;
		}

		// ReSharper disable once UnusedParameter.Local
		private static Control CreatePage4(RenderWindow renderWindow)
		{
			var colorTheme1 = new ColorTheme<Color>(Color.Black, Color.Transparent);
			var colorTheme2 = new ColorTheme<Color>(Color.Black, Color.White);
			var grid = new Grid
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
						ControlUtils.FindParent<Pages>(control).Page = 0;
					}
				};

			var border = new Border { ColorTheme = colorTheme1 };
			border.OnFocusChanged += (s, a) => s.ColorTheme = a.NewValue ? colorTheme2 : colorTheme1;
			grid.Controls.Add(border);

			border = new Border { ColorTheme = colorTheme1 };
			border.OnFocusChanged += (s, a) => s.ColorTheme = a.NewValue ? colorTheme2 : colorTheme1;
			grid.Controls.Add(border);

			border = new Border { ColorTheme = colorTheme1 };
			border.OnFocusChanged += (s, a) => s.ColorTheme = a.NewValue ? colorTheme2 : colorTheme1;
			grid.Controls.Add(border);

			var label = new Label
			{
				Text = SfmlGlyph.FromString("Press escape to return to page #1.")
			};

			border = new Border { ColorTheme = colorTheme1 };
			border.Controls.Add(label);
			border.OnFocusChanged += (s, a) => s.ColorTheme = a.NewValue ? colorTheme2 : colorTheme1;
			grid.Controls.Add(border);

			return grid;
		}

		private static Control CreateRootControl(RenderWindow renderWindow)
		{
			var pages = new Pages();

			pages.Controls.Add(CreatePage1(renderWindow));
			pages.Controls.Add(CreatePage2(renderWindow));
			pages.Controls.Add(CreatePage3(renderWindow));
			pages.Controls.Add(CreatePage4(renderWindow));

			return pages;
		}
	}
}