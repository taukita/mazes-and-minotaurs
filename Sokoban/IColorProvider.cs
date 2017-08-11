using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Ui;

namespace Sokoban
{
	public interface IColorProvider<T>
	{
		ColorTheme<T> MainMenuColorTheme { get; }
		ColorTheme<T> SokobanLabelColorTheme { get; }

		T CrateForeground { get; }
		T CrateBackground { get; }
		T TargetForeground { get; }
		T TargetBackground { get; }
		T WallForeground { get; }
		T WallBackground { get; }
		T PlayerForeground { get; }
		T PlayerBackground { get; }
	}
}
