using MazesAndMinotaurs.SfmlTarget.Ui.Glyphs;
using MazesAndMinotaurs.Ui;
using MazesAndMinotaurs.Ui.Controls;
using SFML.Graphics;
using SFML.Window;

namespace MazesAndMinotaurs.SfmlTarget.Ui
{
	public sealed class Menu : Menu<SfmlGlyph, Color, Keyboard.Key>
	{
		public Menu()
		{
			ColorTheme = new ColorTheme<Color>(Color.Black, Color.Transparent);
			BackgroundGlyph = new CharGlyph(' ');
			EllipsisGlyph = new CharGlyph('~');
			SelectionGlyph = new CharGlyph('>');
			KeyboardAdapter = SfmlKeyboardAdapter.Instance;
		}

		public MenuItem AddItem(string @string)
		{
			return AddItem(SfmlGlyph.FromString(@string));
		}
	}
}
