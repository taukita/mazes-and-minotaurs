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

		[Test]
		public void FirstPageShouldBeFocusedAfterPagesFocused()
		{
			var border1 = new Border<char, object, TestKey>();
			var border2 = new Border<char, object, TestKey>();
			var pages = new Pages<char, object, TestKey>(new TestKeyboardAdapter());

			pages.Controls.Add(border1);
			pages.Controls.Add(border2);

			Assert.IsFalse(border1.IsFocused);
			Assert.IsFalse(border2.IsFocused);
			Assert.IsFalse(pages.IsFocused);

			pages.IsFocused = true;

			Assert.IsTrue(border1.IsFocused);
			Assert.IsFalse(border2.IsFocused);
			Assert.IsTrue(pages.IsFocused);
		}
	}
}
