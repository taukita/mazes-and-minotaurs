using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;
using NUnit.Framework;

namespace MazesAndMinotaurs.ConsoleTarget.Ui.Tests
{
	[TestFixture]
	internal class BorderControlTests
	{
		[Test]
		public void DrawingTest()
		{
			ITerminal<char, ConsoleColor> terminal = new TestTerminal(3, 3);
			var borderControl = new BorderControl
				{
					Width = 3,
					Height = 3,
					BorderTheme = new BorderTheme('#')
				};
			borderControl.Draw(terminal);
			Assert.AreEqual(@"###
# #
###", terminal.ToString());
		}

		[Test]
		public void MinimalDrawingTest()
		{
			ITerminal<char, ConsoleColor> terminal = new TestTerminal(2, 2);
			var borderControl = new BorderControl
			{
				Width = 2,
				Height = 2,
				BorderTheme = new BorderTheme('#')
			};
			borderControl.Draw(terminal);
			Assert.AreEqual(@"##
##", terminal.ToString());
		}
	}
}
