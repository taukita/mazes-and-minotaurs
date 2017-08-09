using MazesAndMinotaurs.Ui.Controls.Containers;
using SFML.Graphics;
using SFML.Window;

namespace MazesAndMinotaurs.SfmlTarget.Ui
{
	public sealed class Grid : Grid<SfmlGlyph, Color, Keyboard.Key>
	{
		public Grid()
		{
			KeyboardAdapter = SfmlKeyboardAdapter.Instance;
		}
	}
}
