using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Ui;
using MazesAndMinotaurs.Ui.Controls;
using MazesAndMinotaurs.Ui.Controls.Containers;
using MazesAndMinotaurs.SfmlTarget;

namespace Sokoban
{	
	using Border = Border<Glyph, Color, Keyboard.Key>;
	using Control = Control<Glyph, Color, Keyboard.Key>;
	using Menu = Menu<Glyph, Color, Keyboard.Key>;
	using Pages = Pages<Glyph, Color, Keyboard.Key>;
	using ControlUtils = ControlUtils<Glyph, Color, Keyboard.Key>;

	class Program
	{	
		private const uint HeightInGlyphs = 40;
		private const uint WidthInGlyphs = 60;

		private static readonly Glyph EllipsisGlyph = new Glyph(14, 7);
		private static readonly Glyph SelectionGlyph = new Glyph(14, 3);

		static void Main(string[] args)
		{
			var image = new Image("cp437_16x16.png");
			image.CreateMaskFromColor(Color.Black);

			var texture = new Texture(image);
			uint glyphWidth = 16;
			uint glyphHeight = 16;

			var renderWindow = new RenderWindow(
				new VideoMode(WidthInGlyphs * glyphWidth, HeightInGlyphs * glyphHeight), "Sokoban", Styles.Close | Styles.Titlebar);
			renderWindow.Closed += (s, e) => renderWindow.Close();
			//var windowColor = new Color(0, 192, 255);
			var windowColor = Color.Black;

			var terminal = new Terminal((int)glyphHeight, (int)glyphWidth, texture, renderWindow);
			var root = CreateRoot();
			root.IsFocused = true;

			renderWindow.KeyPressed += (s, e) => root.NotifyKeyPressed(e.Code);

			while (renderWindow.IsOpen && root.IsFocused)
			{
				renderWindow.Clear(windowColor);
				renderWindow.DispatchEvents();

				root.Draw(terminal);

				renderWindow.Display();
			}
		}

		private static Control CreateRoot()
		{
			var pages = new Pages();
			pages.Controls.Add(CreateMainMenu());
			pages.Controls.Add(CreateGame());
			pages.KeyboardAdapter = SfmlKeyboardAdapter.Instance;
			return pages;
		}

		private static Control CreateMainMenu()
		{
			var menu = new Menu(EllipsisGlyph, SelectionGlyph);
			var newGameItem = menu.AddItem(FromString("New game"));
			menu.AddItem(FromString("Load game"));
			var exitItem = menu.AddItem(FromString("Exit game"));
			menu.OnSelect += (s, item) =>
			{
				if (item == newGameItem)
				{
					ControlUtils.FindParent<Pages>(s).Page = 1;
				}
				else if (item == exitItem)
				{
					ControlUtils.FindParent<Pages>(s).IsFocused = false;
				}
			};

			var border = new Border();
			border.BorderTheme = new BorderTheme<Glyph>(
				new Glyph(9, 12), new Glyph(13, 12), new Glyph(11, 11), new Glyph(10, 11),
				new Glyph(12, 11), new Glyph(13, 12), new Glyph(8, 12), new Glyph(10, 11));
			border.ColorTheme = new ColorTheme<Color>(Color.White, new Color(128, 102, 64));
			border.Controls.Add(menu);
			border.Left = 1;
			border.Top = 1;
			border.Width = 20;
			border.Height = 10;
			border.Title = FromString("Main menu");

			return border;
		}

		private static Control CreateGame()
		{
			var game = new GameControl();
			game.Left = 1;
			game.Top = 1;
			return game;
		}

		private static IEnumerable<Glyph> FromString(string @string)
		{
			return @string.Select(@char => new Glyph(GetX(@char), GetY(@char)));
		}

		private static int GetX(char @char)
		{
			if (@char == ' ')
			{
				return 0;
			}
			if (@char >= 'A' && @char <= 'O')
			{
				return 1 + @char - 'A';
			}
			if (@char >= 'P' && @char <= 'Z')
			{
				return @char - 'P';
			}
			if (@char >= 'a' && @char <= 'o')
			{
				return 1 + @char - 'a';
			}
			if (@char >= 'p' && @char <= 'z')
			{
				return @char - 'p';
			}
			throw new ArgumentOutOfRangeException(nameof(@char));
		}

		private static int GetY(char @char)
		{
			if (@char == ' ')
			{
				return 0;
			}
			if (@char >= 'A' && @char <= 'O')
			{
				return 4;
			}
			if (@char >= 'P' && @char <= 'Z')
			{
				return 5;
			}
			if (@char >= 'a' && @char <= 'o')
			{
				return 6;
			}
			if (@char >= 'p' && @char <= 'z')
			{
				return 7;
			}
			throw new ArgumentOutOfRangeException(nameof(@char));
		}
	}
}
