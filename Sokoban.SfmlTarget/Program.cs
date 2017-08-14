using System;
using MazesAndMinotaurs.SfmlTarget;
using MazesAndMinotaurs.Ui;
using SFML.Graphics;
using SFML.Window;
using Sokoban.Core;

namespace Sokoban.SfmlTarget
{
	using Control = Control<Glyph, Color, SfmlInput>;

	internal class Program
	{
		private const uint HeightInGlyphs = 40;
		private const uint WidthInGlyphs = 60;

		private static readonly IGlyphProvider<Glyph> GlyphProvider = new SfmlGlyphProvider();
		private static readonly IColorProvider<Color> ColorProvider = new SfmlColorProvider();

		// ReSharper disable once UnusedParameter.Local
		private static void Main(string[] args)
		{
			var image = new Image("cp437_16x16.png");
			image.CreateMaskFromColor(Color.Black);

			var texture = new Texture(image);
			uint glyphWidth = 16;
			uint glyphHeight = 16;

			var renderWindow = new RenderWindow(
				new VideoMode(WidthInGlyphs * glyphWidth, HeightInGlyphs * glyphHeight), "Sokoban", Styles.Close | Styles.Titlebar);
			renderWindow.Closed += (s, e) => renderWindow.Close();
			var windowColor = Color.Black;

			var terminal = new Terminal((int) glyphHeight, (int) glyphWidth, texture, renderWindow);
			var root = CreateRoot(glyphWidth, glyphHeight);
			root.IsFocused = true;

			renderWindow.KeyPressed += (s, e) => root.NotifyKeyboardInput(new SfmlInput(e));
			renderWindow.MouseButtonPressed += (s, e) => root.NotifyMouseInput(new SfmlInput(e));

			while (renderWindow.IsOpen && root.IsFocused)
			{
				renderWindow.Clear(windowColor);
				renderWindow.DispatchEvents();

				root.Draw(terminal);

				renderWindow.Display();
			}
		}

		private static Control CreateRoot(uint glyphWidth, uint glyphHeight)
		{
			Control root =
				new GameControl<Glyph, Color, SfmlInput>(GlyphProvider, ColorProvider, (int) HeightInGlyphs,
					(int) WidthInGlyphs);
			root.KeyboardAdapter = SfmlKeyboardAdapter.Instance;
			root.MouseAdapter = new MouseAdapter(glyphWidth, glyphHeight);
			return root;
		}
	}
}