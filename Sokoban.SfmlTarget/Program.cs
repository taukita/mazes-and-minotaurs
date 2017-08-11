using MazesAndMinotaurs.SfmlTarget;
using MazesAndMinotaurs.Ui;
using SFML.Graphics;
using SFML.Window;
using Sokoban.Core;

namespace Sokoban.SfmlTarget
{
	using Control = Control<Glyph, Color, Keyboard.Key>;

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
			Control root =
				new GameControl<Glyph, Color, Keyboard.Key>(GlyphProvider, ColorProvider, (int) HeightInGlyphs,
					(int) WidthInGlyphs);
			root.KeyboardAdapter = SfmlKeyboardAdapter.Instance;
			return root;
		}
	}
}