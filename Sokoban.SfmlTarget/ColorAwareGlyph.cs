using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;
using SFML.Graphics;

namespace Sokoban.SfmlTarget
{
	internal class ColorAwareGlyph : Glyph, IColorAwareGlyph<Color>
	{
		public ColorAwareGlyph(int item1, int item2, Color foreground, Color background) : base(item1, item2)
		{
			Foreground = foreground;
			Background = background;
		}

		public Color Foreground { get; }
		public Color Background { get; }
	}
}
