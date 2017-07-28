using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class BorderTheme
	{
		public static BorderTheme Box = new BorderTheme('┌', '─', '┐', '│', '┘', '─', '└', '│');

		public char TopLeft { get; }
		public char Top { get; }
		public char TopRight { get; }
		public char Right { get; }
		public char BottomRight { get; }
		public char Bottom { get; }
		public char BottomLeft { get; }
		public char Left { get; }

		public BorderTheme(char topLeft, char top, char topRight, char right, char bottomRight, char bottom, char bottomLeft, char left)
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

		public BorderTheme(char c)
			: this(c, c, c, c, c, c, c, c)
		{
		}
	}
}
