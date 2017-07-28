using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MazesAndMinotaurs.ConsoleTarget.Ui.Tests
{
	[TestFixture]
	internal class PagesContainerControlTest
	{
		[Test]
		public void PagesContainerShouldDrawOnlyFocusedChild()
		{
			var menu1Painted = false;
			var menu2Painted = false;

			var menu1 = new MenuControl("item") {Width = 10, Height = 10};
			menu1.OnSelect += (m, s) => KeyPressedResult.Next;
			menu1.OnDraw += control => menu1Painted = true;

			var menu2 = new MenuControl("item") {Width = 10, Height = 10};
			menu2.OnDraw += control => menu2Painted = true;

			var pages = new PagesContainerControl();
			pages.Controls.Add(menu1);
			pages.Controls.Add(menu2);
			pages.Focus();

			pages.Draw(DummyTerminal.Instance);

			Assert.IsTrue(menu1Painted);
			Assert.IsFalse(menu2Painted);

			menu1Painted = false;

			pages.NotifyKeyPressed(ConsoleKey.Enter);
			pages.Draw(DummyTerminal.Instance);

			Assert.IsFalse(menu1Painted);
			Assert.IsTrue(menu2Painted);
		}
	}
}
