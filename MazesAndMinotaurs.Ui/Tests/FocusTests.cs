using System;
using System.Linq;
using MazesAndMinotaurs.Ui.Controls;
using MazesAndMinotaurs.Ui.Controls.Containers;
using NUnit.Framework;

namespace MazesAndMinotaurs.Ui.Tests
{
	[TestFixture]
	internal class FocusTests
	{
		[Test]
		public void BorderContentShouldBeFocusedAfterBorderFocused()
		{
			var menu = new Menu<char, object, TestKey>();
			var border = new Border<char, object, TestKey>();
			border.Controls.Add(menu);
			Assert.IsFalse(menu.IsFocused);
			border.IsFocused = true;
			Assert.IsTrue(menu.IsFocused);
		}

		[Test]
		public void FirstChildShouldBeFocusedAfterGridFocused()
		{
			var grid = new Grid<char, object, TestKey>();
			grid.Controls.Add(new Border<char, object, TestKey>());
			grid.Controls.Add(new Border<char, object, TestKey>());
			grid.Controls.Add(new Border<char, object, TestKey>());
			grid.Controls.Add(new Border<char, object, TestKey>());

			Assert.IsFalse(grid.IsFocused);
			Assert.IsFalse(grid.Controls[0].IsFocused);
			Assert.IsFalse(grid.Controls[1].IsFocused);
			Assert.IsFalse(grid.Controls[2].IsFocused);
			Assert.IsFalse(grid.Controls[3].IsFocused);

			grid.IsFocused = true;

			Assert.IsTrue(grid.IsFocused);
			Assert.IsTrue(grid.Controls[0].IsFocused);
			Assert.IsFalse(grid.Controls[1].IsFocused);
			Assert.IsFalse(grid.Controls[2].IsFocused);
			Assert.IsFalse(grid.Controls[3].IsFocused);
		}

		[Test]
		public void FirstPageShouldBeFocusedAfterPagesFocused()
		{
			var border1 = new Border<char, object, TestKey>();
			var border2 = new Border<char, object, TestKey>();
			var pages = new Pages<char, object, TestKey>();

			pages.Controls.Add(border1);
			pages.Controls.Add(border2);

			Assert.IsFalse(border1.IsFocused);
			Assert.IsFalse(border2.IsFocused);
			Assert.IsFalse(pages.IsFocused);

			pages.IsFocused = true;

			Assert.IsTrue(pages.IsFocused);
			Assert.IsTrue(border1.IsFocused);
			Assert.IsFalse(border2.IsFocused);
			Assert.AreEqual(0, pages.Page);
		}

		[TestCase(typeof(Canvas<char, object, TestKey>))]
		[TestCase(typeof(Grid<char, object, TestKey>))]
		public void TabInSomeContainersShouldMoveFocusToNextChild(Type containerType)
		{
			var container = (Container<char, object, TestKey>)Activator.CreateInstance(containerType);
			container.KeyboardAdapter = new TestKeyboardAdapter();
			container.Controls.Add(new Border<char, object, TestKey>());
			container.Controls.Add(new Border<char, object, TestKey>());
			container.Controls.Add(new Border<char, object, TestKey>());
			container.Controls.Add(new Border<char, object, TestKey>());
			container.IsFocused = true;

			for (var i = 0; i <= container.Controls.Count; i++)
			{
				Assert.IsTrue(container.IsFocused);
				var focused = container.Controls[i%container.Controls.Count];
				Assert.IsTrue(focused.IsFocused);
				foreach (var control in container.Controls.Where(c => c != focused))
				{
					Assert.IsFalse(control.IsFocused);
				}
				container.NotifyKeyboardInput(TestKey.Tab);
			}
		}

		[TestCase(typeof(Border<char, object, TestKey>))]
		[TestCase(typeof(Canvas<char, object, TestKey>))]
		[TestCase(typeof(Grid<char, object, TestKey>))]
		[TestCase(typeof(Pages<char, object, TestKey>))]
		[TestCase(typeof(Panel<char, object, TestKey>))]
		public void ChildrenShouldBeUnfocusedAfterContainerUnfocused(Type containerType)
		{
			var menu1 = new Menu<char, object, TestKey>();
			var menu2 = new Menu<char, object, TestKey>();
			var container = (Container<char, object, TestKey>)Activator.CreateInstance(containerType);
			container.Controls.Add(menu1);
			container.Controls.Add(menu2);
			container.IsFocused = true;
			menu2.IsFocused = true;
			container.IsFocused = false;
			Assert.IsFalse(menu2.IsFocused);
		}
	}
}