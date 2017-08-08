using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.SfmlTarget;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Test
{
	internal class SfmlTargetApp
	{
		private const int CHARACTER_SIZE = 12;
		private const int WIDTH_IN_GLYPHS = 80;
		private const int HEIGHT_IN_GLYPHS = 60;

		public void Run()
		{
			var font = new Font("square.ttf");
			var text = new Text
			{
				CharacterSize = CHARACTER_SIZE,
				DisplayedString = "#",
				Font = font
			};
			var lb = text.GetLocalBounds();
			var glyphWidth = (uint)(lb.Left + lb.Width);
			var glyphHeight = (uint)(lb.Top + lb.Height);

			var renderWindow = new RenderWindow(
				new VideoMode(WIDTH_IN_GLYPHS * glyphWidth, HEIGHT_IN_GLYPHS * glyphHeight), "Test", Styles.Close | Styles.Titlebar);
			renderWindow.Closed += (s, e) => renderWindow.Close();
			var windowColor = new Color(0, 192, 255);

			var terminal = new SfmlTerminal(renderWindow, font, CHARACTER_SIZE, glyphWidth, glyphHeight);

			while (renderWindow.IsOpen)
			{
				renderWindow.DispatchEvents();
				renderWindow.Clear(windowColor);

				terminal.DrawRectangle(1, 1, WIDTH_IN_GLYPHS - 2, HEIGHT_IN_GLYPHS - 2, '#', Color.Black, Color.White);
				terminal.DrawRectangle(2, 2, WIDTH_IN_GLYPHS - 4, HEIGHT_IN_GLYPHS - 4, '#', Color.Black, windowColor);

				renderWindow.Display();
			}
		}
	}
}
