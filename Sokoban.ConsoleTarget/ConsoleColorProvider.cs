using System;
using MazesAndMinotaurs.Ui;
using Sokoban.Core;

namespace Sokoban.ConsoleTarget
{
	internal class ConsoleColorProvider : IColorProvider<ConsoleColor>
	{
		public ConsoleColorProvider()
		{
			MainMenuColorTheme = new ColorTheme<ConsoleColor>(ConsoleColor.White, ConsoleColor.DarkYellow);
			SokobanLabelColorTheme = new ColorTheme<ConsoleColor>(ConsoleColor.Red, ConsoleColor.Black);

			CrateForeground = ConsoleColor.DarkYellow;
			CrateBackground = ConsoleColor.Black;

			TargetForeground = ConsoleColor.Magenta;
			TargetBackground = ConsoleColor.Black;

			WallForeground = ConsoleColor.DarkYellow;
			WallBackground = ConsoleColor.Gray;

			PlayerForeground = ConsoleColor.Red;
			PlayerBackground = ConsoleColor.Black;
		}

		public ColorTheme<ConsoleColor> MainMenuColorTheme { get; }
		public ColorTheme<ConsoleColor> SokobanLabelColorTheme { get; }
		public ConsoleColor CrateForeground { get; }
		public ConsoleColor CrateBackground { get; }
		public ConsoleColor TargetForeground { get; }
		public ConsoleColor TargetBackground { get; }
		public ConsoleColor WallForeground { get; }
		public ConsoleColor WallBackground { get; }
		public ConsoleColor PlayerForeground { get; }
		public ConsoleColor PlayerBackground { get; }
	}
}