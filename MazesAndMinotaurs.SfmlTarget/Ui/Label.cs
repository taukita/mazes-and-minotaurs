using MazesAndMinotaurs.Ui;
using MazesAndMinotaurs.Ui.Controls;
using SFML.Graphics;
using SFML.Window;
using System;

namespace MazesAndMinotaurs.SfmlTarget.Ui
{
	public sealed class Label : Label<SfmlGlyph, Color, Keyboard.Key>
	{
		public Label()
		{
			ColorTheme = new ColorTheme<Color>(Color.Black, Color.Transparent);
			Delimiter = SfmlGlyph.FromString(Environment.NewLine);
		}
	}
}
