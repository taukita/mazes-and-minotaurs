using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Ui.Controls;

namespace MazesAndMinotaurs.Ui.Tests
{
	internal class TestsBase
	{
		protected static Menu<char, object, TestKey> Menu(int width = 0, int height = 0)
		{
			var menu = new Menu<char, object, TestKey>
			{
				ColorTheme = new ColorTheme<object>(null, null),
				EllipsisGlyph = '~',
				SelectionGlyph = '>',
				Width = width,
				Height = height
			};
			return menu;
		}
	}
}
