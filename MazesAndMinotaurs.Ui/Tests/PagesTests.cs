using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Ui.Controls;
using MazesAndMinotaurs.Ui.Controls.Containers;
using NUnit.Framework;

namespace MazesAndMinotaurs.Ui.Tests
{
	[TestFixture]
	internal class PagesTests
	{
		[Test]
		public void PagesContainerShouldDrawOnlyFocusedChild()
		{
			var menu1Painted = false;
			var menu2Painted = false;

			var pages = new Pages<char, object, TestKey> {KeyboardAdapter = new TestKeyboardAdapter()};

			var menu1 = new Menu<char, object, TestKey>
				{
					EllipsisGlyph = '~',
					SelectionGlyph = '>',
					ColorTheme = new ColorTheme<object>(null, null),
					Width = 10,
					Height = 10
				};
			menu1.OnSelect += (m, s) => pages.Page = 1;
			menu1.OnDraw += control => menu1Painted = true;

			var menu2 = new Menu<char, object, TestKey>
				{
					EllipsisGlyph = '~',
					SelectionGlyph = '>',
					ColorTheme = new ColorTheme<object>(null, null),
					Width = 10,
					Height = 10
				};

			menu2.OnDraw += control => menu2Painted = true;

			pages.Controls.Add(menu1);
			pages.Controls.Add(menu2);
			pages.IsFocused = true;

			var terminal = new TestTerminal(10, 10);

			pages.Draw(terminal);

			Assert.IsTrue(menu1Painted);
			Assert.IsFalse(menu2Painted);

			menu1Painted = false;

			pages.Page = 1;
			pages.Draw(terminal);

			Assert.IsFalse(menu1Painted);
			Assert.IsTrue(menu2Painted);
		}
	}
}
