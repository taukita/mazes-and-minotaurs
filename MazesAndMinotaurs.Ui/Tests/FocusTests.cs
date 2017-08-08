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
	internal class FocusTests
	{
		[Test]
		public void BorderContentShouldBeFocusedAfterBorderFocused()
		{
			var menu = new Menu<char, object, TestKey>(new TestKeyboardAdapter(), '~', '>');
			var border = new Border<char, object, TestKey>(menu);
			Assert.IsFalse(menu.IsFocused);
			border.IsFocused = true;
			Assert.IsTrue(menu.IsFocused);
		}
	}
}
