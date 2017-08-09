﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using MazesAndMinotaurs.Core;
using SFML.System;
using MazesAndMinotaurs.SfmlTarget.Ui;
using MazesAndMinotaurs.SfmlTarget.Ui.Glyphs;

namespace MazesAndMinotaurs.SfmlTarget
{
	public class SfmlTerminal : AbstractTerminal<SfmlGlyph, Color>
	{
		private RenderWindow _window;

		private Font _font;
		private uint _characterSize;

		private Dictionary<char, Text> _cache = new Dictionary<char, Text>();
		private RectangleShape _shape;

		private uint _glyphWidth;
		private uint _glyphHeight;

		public SfmlTerminal(RenderWindow window, Font font, uint characterSize, uint glyphWidth, uint glyphHeight)
		{
			_window = window;
			_font = font;
			_characterSize = characterSize;
			_glyphWidth = glyphWidth;
			_glyphHeight = glyphHeight;
			_shape = new RectangleShape(new Vector2f(glyphWidth, glyphHeight));
		}

		public override void Clear(int x, int y)
		{
			
		}

		public override void Draw(int x, int y, SfmlGlyph glyph, Color foreground, Color background)
		{
			var position = new Vector2f(x * _glyphWidth, y * _glyphHeight);
			_shape.Position = position;
			_shape.FillColor = background;

			var text = GetCached(glyph);
			text.Position = position;
			text.FillColor = foreground;

			_window.Draw(_shape);
			_window.Draw(text);
		}

		private Text GetCached(SfmlGlyph glyph)
		{
			var ch = (glyph as CharGlyph)?.Char;
			if (!ch.HasValue)
			{
				return null;
			}
			if (!_cache.ContainsKey(ch.Value))
			{
				var text = new Text
				{
					CharacterSize = _characterSize,
					DisplayedString = ch.ToString(),
					Font = _font
				};
				_cache[ch.Value] = text;
			}
			return _cache[ch.Value];
		}
	}
}
