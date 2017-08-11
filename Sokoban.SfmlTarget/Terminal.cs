using MazesAndMinotaurs.Core;
using SFML.Graphics;
using SFML.System;

namespace Sokoban.SfmlTarget
{
	internal class Terminal : AbstractTerminal<Glyph, Color>
	{
		private readonly int _glyphHeight;
		private readonly int _glyphWidth;
		private readonly RectangleShape _shape;
		private readonly Texture _texture;
		private readonly RenderWindow _window;

		public Terminal(int glyphHeight, int glyphWidth, Texture texture, RenderWindow window)
		{
			_glyphHeight = glyphHeight;
			_glyphWidth = glyphWidth;
			_texture = texture;
			_window = window;
			_shape = new RectangleShape(new Vector2f(glyphWidth, glyphHeight));
		}

		public override void Clear(int x, int y)
		{
		}

		protected override void Drawing(int x, int y, Glyph glyph, Color foreground, Color background)
		{
			var sprite = new Sprite(_texture,
				new IntRect(glyph.Item1 * _glyphWidth, glyph.Item2 * _glyphHeight, _glyphWidth, _glyphHeight))
			{
				Color = foreground,
				Position = new Vector2f(x * _glyphWidth, y * _glyphHeight)
			};
			_shape.FillColor = background;
			_shape.Position = sprite.Position;
			_window.Draw(_shape);
			_window.Draw(sprite);
		}
	}
}