using MazesAndMinotaurs.Ui;
using MazesAndMinotaurs.Ui.Controls.Containers;
using SFML.Graphics;
using SFML.Window;

namespace MazesAndMinotaurs.SfmlTarget.Ui
{
	public sealed class Border : Border<SfmlGlyph, Color, Keyboard.Key>
	{
		private static readonly SfmlGlyph Corner = SfmlGlyph.FromChar('*');
		private static readonly SfmlGlyph Horizontal = SfmlGlyph.FromChar('-');
		private static readonly SfmlGlyph Vertical = SfmlGlyph.FromChar('|');
		private static readonly BorderTheme<SfmlGlyph> DefaultBoderTheme =
			new BorderTheme<SfmlGlyph>(Corner, Horizontal, Corner, Vertical, Corner, Horizontal, Corner, Vertical);

		public Border()
		{
			BorderTheme = DefaultBoderTheme;
			ColorTheme = new ColorTheme<Color>(Color.Black, Color.Transparent);
		}
	}
}
