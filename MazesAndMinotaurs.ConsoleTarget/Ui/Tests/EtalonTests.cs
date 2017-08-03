using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.ConsoleTarget.Ui.Tests
{
	[TestFixture]
	internal class EtalonTests
	{
		private const string BorderEtalon1 = @"####
#  #
####";

		private const string BorderEtalon2 = @"┌──┐
│  │
└──┘";

		private const string BorderlessMenuEtalon = @">item1
 item2
 item3";

		private const string BorderedMenuEtalon = @"########
#>item1#
# item2#
# item3#
########";

		private const string GridEtalon = @"┌────────┐┌────────┐##########
│>item 1 ││>item 1 │#        #
│ item 2 ││ item 2 │#        #
│        ││ item 3 │#        #
│        ││        │#        #
│        ││        │#        #
│        ││        │#        #
│        ││        │#        #
│        ││        │#        #
└────────┘└────────┘##########
##############################
#        ##        ##        #
#        ##        ##        #
#        ##        ##        #
#        ##        ##        #
#        ##        ##        #
#        ##        ##        #
#        ##        ##        #
#        ##        ##        #
##############################";

		private const string CanvasEtalon = @"..............................................
.###.###.................┌────────┐.┌────────┐
.#.#.#.#.................│>item 1.│.│>item 1.│
.###.###.................│.item 2.│.│.item 2.│
.........................│........│.│.item 3.│
.###.###.................│........│.│........│
.#.#.#.#.................│........│.│........│
.###.###.................│........│.│........│
.........................│........│.│........│
.........................│........│.│........│
.........................└────────┘.└────────┘";

		private const string Text1 = @"abcde";

		private const string TextEtalon1 = @"abc
de.";

		private const string Text2 = @"abcde
abcde";

		private const string TextEtalon2 = @"abc
de.
abc";

		[Test]
		public void BorderDrawingShouldBeEqualToItsEtalon()
		{
			var control = new BorderControl();
			control.Width = 4;
			control.Height = 3;
			AssertEqual(control, BorderEtalon1);
			control.BorderTheme = BorderTheme.Box;
			AssertEqual(control, BorderEtalon2);
		}

		[Test]
		public void BorderlessMenuDrawingShouldBeEqualToItsEtalon()
		{
			var control = new MenuControl("item1", "item2", "item3");
			control.Width = 6;
			control.Height = 3;
			AssertEqual(control, BorderlessMenuEtalon);
		}

		[Test]
		public void BorderedMenuDrawingShouldBeEqualToItsEtalon()
		{
			var control = new BorderControl(new MenuControl("item1", "item2", "item3"));
			control.Width = 8;
			control.Height = 5;
			AssertEqual(control, BorderedMenuEtalon);
		}

		[Test]
		public void GridDrawingShouldBeEqualToItsEtalon()
		{
			var grid = new GridContainerControl();
			grid.Width = 30;
			grid.Height = 20;

			grid.Columns.Add(10);
			grid.Columns.Add(10);
			grid.Columns.Add(10);

			grid.Rows.Add(10);
			grid.Rows.Add(10);

			foreach (var control in TestControls())
			{
				grid.Controls.Add(control);
			}

			AssertEqual(grid, GridEtalon);
		}

		[Test]
		public void CanvasDrawingShouldBeEqualToItsEtalon()
		{
			var canvas = new CanvasContainerControl();
			canvas.Width = 46;
			canvas.Height = 11;

			foreach (var control in TestControls())
			{
				canvas.Controls.Add(control);
			}

			AssertEqual(canvas, CanvasEtalon, '.');
		}

		[TestCase(3, 2, Text1, TextEtalon1)]
		[TestCase(3, 3, Text2, TextEtalon2)]
		public void TextDrawingShouldBeEqualToItsEtalon(int width, int height, string text, string etalon)
		{
			var textControl = new TextControl();
			textControl.Width = width;
			textControl.Height = height;
			textControl.Text = text;

			AssertEqual(textControl, etalon, '.');
		}

		private void AssertEqual(Control control, string etalon, char empty = ' ')
		{
			var terminal = new TestTerminal(control.Width, control.Height, empty);
			control.Draw(terminal);
			Assert.AreEqual(etalon, terminal.ToString());
		}

		private static IEnumerable<Control> TestControls()
		{
			var colorTheme = ColorTheme.Create(ConsoleColor.Black, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Black);

			var menu1 = new BorderControl(new MenuControl("item 1", "item 2"))
			{
				Left = 25,
				Top = 1,
				Width = 10,
				Height = 10,
				BorderTheme = BorderTheme.Box,
				ColorTheme = colorTheme
			};

			yield return menu1;

			var menu2 = new BorderControl(new MenuControl("item 1", "item 2", "item 3"))
			{
				Left = 36,
				Top = 1,
				Width = 10,
				Height = 10,
				BorderTheme = BorderTheme.Box,
				ColorTheme = colorTheme
			};

			yield return menu2;

			yield return new BorderControl { Left = 1, Top = 1, Width = 3, Height = 3, ColorTheme = colorTheme };
			yield return new BorderControl { Left = 1, Top = 5, Width = 3, Height = 3, ColorTheme = colorTheme };
			yield return new BorderControl { Left = 5, Top = 5, Width = 3, Height = 3, ColorTheme = colorTheme };
			yield return new BorderControl { Left = 5, Top = 1, Width = 3, Height = 3, ColorTheme = colorTheme };
		}
	}
}
