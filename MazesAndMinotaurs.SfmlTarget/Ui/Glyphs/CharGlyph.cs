namespace MazesAndMinotaurs.SfmlTarget.Ui.Glyphs
{
	internal class CharGlyph : SfmlGlyph
	{
		public CharGlyph(char @char)
		{
			Char = @char;
		}

		public char Char { get; }

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var glyph = obj as CharGlyph;
			if (glyph == null)
			{
				return false;
			}
			return Char == glyph.Char;
		}

		public override int GetHashCode()
		{
			return Char.GetHashCode();
		}
	}
}
