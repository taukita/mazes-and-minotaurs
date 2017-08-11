using System;
using MazesAndMinotaurs.Ui.Controls;
using MazesAndMinotaurs.Ui.Controls.Containers;
using NUnit.Framework;

namespace MazesAndMinotaurs.Ui.Tests
{
	[TestFixture]
	internal class EtalonTests : TestsBase
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

		private const string VerticalPanelEtalon1 = @"..###..
..#.#..
..###..
.*****.
.*...*.
.*...*.
.*****.
..###..
..#.#..
..###..";

		private const string VerticalPanelEtalon2 = @".....
.###.
.#.#.
.#.#.
.###.
.....";

		private const string CanvasEtalon = @"..........
.###......
.#.#......
.###......
..........
.test.....
......###.
......#.#.
.**...###.
.**.......";

		[Test]
		public void BorderedMenuDrawingShouldBeEqualToItsEtalon()
		{
			var menu = Menu();
			menu.AddItem("item1");
			menu.AddItem("item2");
			menu.AddItem("item3");

			var border = Border('#', 8, 5);
			border.Controls.Add(menu);

			AssertEqual(border, BorderedMenuEtalon);
		}

		[Test]
		public void BorderlessMenuDrawingShouldBeEqualToItsEtalon()
		{
			var menu = Menu(6, 3);
			menu.AddItem("item1");
			menu.AddItem("item2");
			menu.AddItem("item3");
			AssertEqual(menu, BorderlessMenuEtalon);
		}

		[TestCase(3, 2, Text1, LabelEtalon1)]
		[TestCase(3, 3, Text2, LabelEtalon2)]
		public void LabelDrawingShouldBeEqualToItsEtalon(int width, int height, string text, string etalon)
		{
			AssertEqual(Label(text, width, height), etalon, '.');
		}

		[Test]
		public void VerticalPanelDrawingShouldBeEqualToItsEtalon()
		{
			var panel = new Panel<char, object, TestKey>
			{
				Height = 10,
				Vertical = true,
				Width = 7
			};
			var border = Border('#', 3, 3);
			panel.Controls.Add(border);
			border = Border('*', 5, 4);
			panel.Controls.Add(border);
			border = Border('#', 3, 3);
			panel.Controls.Add(border);

			AssertEqual(panel, VerticalPanelEtalon1, '.');

			panel.Controls.Clear();
			panel.Height = 6;
			panel.Width = 5;
			border = Border('#', 3, 4);
			panel.Controls.Add(border);

			AssertEqual(panel, VerticalPanelEtalon2, '.');
		}

		[Test]
		public void CanvasDrawingShouldBeEqualToItsEtalon()
		{
			var canvas = new Canvas<char, object, TestKey> {Width = 10, Height = 10};

			canvas.Controls.Add(Border('#', 3, 3, 1, 1));
			canvas.Controls.Add(Border('#', 3, 3, 6, 6));
			canvas.Controls.Add(Border('*', 2, 2, 1, 8));
			canvas.Controls.Add(Label("test", 4, 1, 1, 5));

			AssertEqual(canvas, CanvasEtalon, '.');
		}

		private static Border<char, object, TestKey> Border(char theme, int width, int height, int left = 0, int top = 0)
		{
			var border = new Border<char, object, TestKey>
			{
				BorderTheme = new BorderTheme<char>(theme),
				ColorTheme = new ColorTheme<object>(null, null),
				Height = height,
				Width = width,
				Left = left,
				Top = top
			};
			return border;
		}

		private static Label<char, object, TestKey> Label(string text, int width, int height, int left = 0, int top = 0)
		{
			var label = new Label<char, object, TestKey>
			{
				ColorTheme = new ColorTheme<object>(null, null),
				Width = width,
				Height = height,
				Left = left,
				Top = top,
				Delimiter = Environment.NewLine,
				Text = text
			};
			return label;
		}

		private static void AssertEqual(Control<char, object, TestKey> control, string etalon, char empty = ' ')
		{
			var terminal = new TestTerminal(control.Width, control.Height, empty);
			control.Draw(terminal);
			Assert.AreEqual(etalon, terminal.ToString());
		}
	}
}