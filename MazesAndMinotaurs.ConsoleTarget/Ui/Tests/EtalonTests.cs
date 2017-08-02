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

		private void AssertEqual(Control control, string etalon)
		{
			var terminal = new TestTerminal(control.Width, control.Height);
			control.Draw(terminal);
			Assert.AreEqual(etalon, terminal.ToString());
		}
	}
}
