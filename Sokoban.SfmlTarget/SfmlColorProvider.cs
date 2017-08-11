using MazesAndMinotaurs.Ui;
using SFML.Graphics;
using Sokoban.Core;

namespace Sokoban.SfmlTarget
{
	internal class SfmlColorProvider : IColorProvider<Color>
	{
		public SfmlColorProvider()
		{
			MainMenuColorTheme = new ColorTheme<Color>(Color.White, new Color(128, 102, 64));
			SokobanLabelColorTheme = new ColorTheme<Color>(Color.Red, Color.Transparent);

			CrateForeground = new Color(158, 134, 100);
			CrateBackground = Color.Black;

			TargetForeground = new Color(255, 0, 191);
			TargetBackground = Color.Black;

			WallForeground = new Color(128, 102, 64);
			WallBackground = new Color(191, 191, 191);

			PlayerForeground = new Color(255, 102, 102);
			PlayerBackground = Color.Black;
		}

		public ColorTheme<Color> MainMenuColorTheme { get; }
		public ColorTheme<Color> SokobanLabelColorTheme { get; }
		public Color CrateForeground { get; }
		public Color CrateBackground { get; }
		public Color TargetForeground { get; }
		public Color TargetBackground { get; }
		public Color WallForeground { get; }
		public Color WallBackground { get; }
		public Color PlayerForeground { get; }
		public Color PlayerBackground { get; }
	}
}