using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class CanvasContainerControl : ContainerControl
	{
		private Control _focused;

		public override void Focus()
		{
			base.Focus();
			Controls[0].Focus();
			_focused = Controls[0];
		}

		protected override void Drawing(ITerminal<char, ConsoleColor> terminal)
		{
			foreach (var control in Controls)
			{
				control.Draw(terminal);
			}
		}

		public override KeyPressedResult NotifyKeyPressed(ConsoleKey key)
		{
			if (key == ConsoleKey.Tab)
			{
				var index = Controls.IndexOf(_focused);
				index++;
				if (index >= Controls.Count)
				{
					index = 0;
				}
				_focused = Controls[index];
				_focused.Focus();
				return KeyPressedResult.DoNothing;
			}
			return _focused.NotifyKeyPressed(key);
		}
	}
}
