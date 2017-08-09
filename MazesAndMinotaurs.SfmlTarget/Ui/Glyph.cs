using MazesAndMinotaurs.SfmlTarget.Ui.Glyphs;
using System.Collections.Generic;
using System.Linq;

namespace MazesAndMinotaurs.SfmlTarget.Ui
{
	public abstract class SfmlGlyph
	{
		public static SfmlGlyph FromChar(char @char)
		{
			return new CharGlyph(@char);
		}

		public static IEnumerable<SfmlGlyph> FromString(string @string)
		{
			return @string.Select(@char => FromChar(@char));
		}
	}
}
