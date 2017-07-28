using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MazesAndMinotaurs.ConsoleTarget.Ui.Tests
{
	[TestFixture]
	internal class CanvasContainerControlTest
	{
		[Test]
		public void TabShouldFocusNextChild()
		{
			var canvas = new TestCanvas {IsFocused = true};
			Assert.AreEqual(canvas.Border1, canvas.FocusedChild);
			canvas.NotifyKeyPressed(ConsoleKey.Tab);
			Assert.AreEqual(canvas.Border2, canvas.FocusedChild);
			canvas.NotifyKeyPressed(ConsoleKey.Tab);
			Assert.AreEqual(canvas.Border3, canvas.FocusedChild);
			canvas.NotifyKeyPressed(ConsoleKey.Tab);
			Assert.AreEqual(canvas.Border4, canvas.FocusedChild);
			canvas.NotifyKeyPressed(ConsoleKey.Tab);
			Assert.AreEqual(canvas.Menu1, canvas.FocusedChild);
			canvas.NotifyKeyPressed(ConsoleKey.Tab);
			Assert.AreEqual(canvas.Menu2, canvas.FocusedChild);
			canvas.NotifyKeyPressed(ConsoleKey.Tab);
			Assert.AreEqual(canvas.Border1, canvas.FocusedChild);
		}

		[Test]
		public void CanvasContainerShouldDrawAllChildren()
		{
			var canvas = new TestCanvas { IsFocused = true };
			canvas.Draw(DummyTerminal.Instance);
			Assert.AreEqual(6, canvas.PaintedChildrenCount);
		}

		private class TestCanvas : CanvasContainerControl
		{
			public TestCanvas()
			{
				Menu1 = new MenuControl(new List<string> {"item 1", "item 2"})
					{
						Left = 25,
						Top = 1,
						Width = 10,
						Height = 10
					};

				Menu2 = new MenuControl(new List<string> {"item 1", "item 2", "item 3"})
					{
						Left = 36,
						Top = 1,
						Width = 10,
						Height = 10
					};

				Border1 = new BorderControl {Left = 1, Top = 1, Width = 3, Height = 3};
				Border2 = new BorderControl {Left = 1, Top = 5, Width = 3, Height = 3};
				Border3 = new BorderControl {Left = 5, Top = 5, Width = 3, Height = 3};
				Border4 = new BorderControl {Left = 5, Top = 1, Width = 3, Height = 3};

				Controls.Add(Border1);
				Controls.Add(Border2);
				Controls.Add(Border3);
				Controls.Add(Border4);
				Controls.Add(Menu1);
				Controls.Add(Menu2);

				Border1.OnDraw += OnDrawChild;
				Border2.OnDraw += OnDrawChild;
				Border3.OnDraw += OnDrawChild;
				Border4.OnDraw += OnDrawChild;
				Menu1.OnDraw += OnDrawChild;
				Menu2.OnDraw += OnDrawChild;
			}

			public BorderControl Border1 { get; }
			public BorderControl Border2 { get; }
			public BorderControl Border3 { get; }
			public BorderControl Border4 { get; }
			public Control FocusedChild => Controls.First(c => c.IsFocused);
			public MenuControl Menu1 { get; }
			public MenuControl Menu2 { get; }
			public int PaintedChildrenCount { get; set; }

			private void OnDrawChild(Control control)
			{
				PaintedChildrenCount++;
			}
		}
	}
}