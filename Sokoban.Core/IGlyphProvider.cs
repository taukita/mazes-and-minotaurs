using System.Collections.Generic;
using MazesAndMinotaurs.Ui;

namespace Sokoban.Core
{
	public interface IGlyphProvider<T>
	{
		T CrateGlyph { get; }
		T TargetGlyph { get; }
		T WallGlyph { get; }
		T PlayerGlyph { get; }
		T EllipsisGlyph { get; }
		T SelectionGlyph { get; }
		ICollection<T> Delimiter { get; }
		BorderTheme<T> MainMenuBorderTheme { get; }
		IEnumerable<T> FromString(string @string);
	}
}
