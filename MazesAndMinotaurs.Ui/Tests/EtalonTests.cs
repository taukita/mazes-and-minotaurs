using MazesAndMinotaurs.Ui.Controls;
using MazesAndMinotaurs.Ui.Controls.Containers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Ui.Tests
{
	[TestFixture]
	internal class EtalonTests
	{
		private const string BorderlessMenuEtalon = @">item1
 item2
 item3";

		private const string BorderedMenuEtalon = @"########
#>item1#
# item2#
# item3#
########";

		private const string Text1 = @"abcde";

		private const string LabelEtalon1 = @"abc
de.";

		private const string Text2 = @"abcde
abcde";

		private const string LabelEtalon2 = @"abc
de.
abc";

		[Test]
		public void BorderlessMenuDrawingShouldBeEqualToItsEtalon()
		{
			var menu = new Menu<char, object, TestKey>('~', '>');
			menu.ColorTheme = new ColorTheme<object>(null, null);
			menu.Width = 6;
			menu.Height = 3;
			menu.AddItem("item1");
			menu.AddItem("item2");
			menu.AddItem("item3");
			AssertEqual(menu, BorderlessMenuEtalon);
		}

		[Test]
		public void BorderedMenuDrawingShouldBeEqualToItsEtalon()
		{
			var menu = new Menu<char, object, TestKey>('~', '>');
			menu.AddItem("item1");
			menu.AddItem("item2");
			menu.AddItem("item3");

			var border = new Border<char, object, TestKey>(menu);
			border.BorderTheme = new BorderTheme<char>('#');
			border.ColorTheme = new ColorTheme<object>(null, null);
			border.Width = 8;
			border.Height = 5;
			AssertEqual(border, BorderedMenuEtalon);
		}

		[TestCase(3, 2, Text1, LabelEtalon1)]
		[TestCase(3, 3, Text2, LabelEtalon2)]
		public void LabelDrawingShouldBeEqualToItsEtalon(int width, int height, string text, string etalon)
		{
			var label = new Label<char, object, TestKey>();
			label.ColorTheme = new ColorTheme<object>(null, null);
			label.Width = width;
			label.Height = height;
			label.Delimiter = Environment.NewLine;
			label.Text = text;

			AssertEqual(label, etalon, '.');
		}

		private void AssertEqual(Control<char, object, TestKey> control, string etalon, char empty = ' ')
		{
			var terminal = new TestTerminal(control.Width, control.Height, empty);
			control.Draw(terminal);
			Assert.AreEqual(etalon, terminal.ToString());
		}
	}
}
