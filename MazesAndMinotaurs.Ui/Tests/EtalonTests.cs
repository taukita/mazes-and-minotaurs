using System;
using MazesAndMinotaurs.Ui.Controls;
using MazesAndMinotaurs.Ui.Controls.Containers;
using NUnit.Framework;

namespace MazesAndMinotaurs.Ui.Tests
{
	[TestFixture]
	internal class EtalonTests
	{
		private const string BorderedMenuEtalon = @"########
#>item1#
# item2#
# item3#
########";
		private const string BorderlessMenuEtalon = @">item1
 item2
 item3";
		private const string LabelEtalon1 = @"abc
de.";
		private const string LabelEtalon2 = @"abc
de.
abc";
		private const string Text1 = @"abcde";
		private const string Text2 = @"abcde
abcde";

		[Test]
		public void BorderedMenuDrawingShouldBeEqualToItsEtalon()
		{
			var menu = new Menu<char, object, TestKey>('~', '>');
			menu.AddItem("item1");
			menu.AddItem("item2");
			menu.AddItem("item3");

			var border = new Border<char, object, TestKey>(menu)
				{
					BorderTheme = new BorderTheme<char>('#'),
					ColorTheme = new ColorTheme<object>(null, null),
					Width = 8,
					Height = 5
				};
			AssertEqual(border, BorderedMenuEtalon);
		}

		[Test]
		public void BorderlessMenuDrawingShouldBeEqualToItsEtalon()
		{
			var menu = new Menu<char, object, TestKey>('~', '>')
				{
					ColorTheme = new ColorTheme<object>(null, null),
					Width = 6,
					Height = 3
				};
			menu.AddItem("item1");
			menu.AddItem("item2");
			menu.AddItem("item3");
			AssertEqual(menu, BorderlessMenuEtalon);
		}

		[TestCase(3, 2, Text1, LabelEtalon1)]
		[TestCase(3, 3, Text2, LabelEtalon2)]
		public void LabelDrawingShouldBeEqualToItsEtalon(int width, int height, string text, string etalon)
		{
			var label = new Label<char, object, TestKey>
				{
					ColorTheme = new ColorTheme<object>(null, null),
					Width = width,
					Height = height,
					Delimiter = Environment.NewLine,
					Text = text
				};

			AssertEqual(label, etalon, '.');
		}

		private static void AssertEqual(Control<char, object, TestKey> control, string etalon, char empty = ' ')
		{
			var terminal = new TestTerminal(control.Width, control.Height, empty);
			control.Draw(terminal);
			Assert.AreEqual(etalon, terminal.ToString());
		}
	}
}