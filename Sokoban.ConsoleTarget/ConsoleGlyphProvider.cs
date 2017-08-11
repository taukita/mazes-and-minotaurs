using System;
using System.Collections.Generic;
using System.Linq;
using MazesAndMinotaurs.Ui;
using Sokoban.Core;

namespace Sokoban.ConsoleTarget
{
	internal class ConsoleGlyphProvider : IGlyphProvider<char>
	{
		public ConsoleGlyphProvider()
		{
			CrateGlyph = '¤';
			TargetGlyph = '·';
			WallGlyph = '█';
			PlayerGlyph = '@';
			EllipsisGlyph = '~';
			SelectionGlyph = '>';
			Delimiter = Environment.NewLine.ToArray();
			MainMenuBorderTheme = new BorderTheme<char>('╔', '═', '╗', '║', '╝', '═', '╚', '║');
		}

		public char CrateGlyph { get; }
		public char TargetGlyph { get; }
		public char WallGlyph { get; }
		public char PlayerGlyph { get; }
		public char EllipsisGlyph { get; }
		public char SelectionGlyph { get; }
		public ICollection<char> Delimiter { get; }
		public BorderTheme<char> MainMenuBorderTheme { get; }

		public IEnumerable<char> FromString(string @string)
		{
			return @string;
		}
	}
}