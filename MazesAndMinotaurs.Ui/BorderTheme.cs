using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Ui
{
	public class BorderTheme<TGlyph>
	{
		public TGlyph TopLeft { get; }
		public TGlyph Top { get; }
		public TGlyph TopRight { get; }
		public TGlyph Right { get; }
		public TGlyph BottomRight { get; }
		public TGlyph Bottom { get; }
		public TGlyph BottomLeft { get; }
		public TGlyph Left { get; }

		public BorderTheme(TGlyph topLeft, TGlyph top, TGlyph topRight, TGlyph right, TGlyph bottomRight, TGlyph bottom, TGlyph bottomLeft, TGlyph left)
		{
			TopLeft = topLeft;
			Top = top;
			TopRight = topRight;
			Right = right;
			BottomRight = bottomRight;
			Bottom = bottom;
			BottomLeft = bottomLeft;
			Left = left;
		}

		public BorderTheme(TGlyph c)
			: this(c, c, c, c, c, c, c, c)
		{
		}
	}
}
