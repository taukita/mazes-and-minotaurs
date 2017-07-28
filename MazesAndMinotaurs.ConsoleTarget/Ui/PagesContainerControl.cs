using System;
using System.Collections.Generic;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class PagesContainerControl : ContainerControl
	{
		private Control _focused;

		public int Page
		{
			get
			{
				return Controls.IndexOf(_focused);
			}
			set
			{
				Controls[value].Focus();
				_focused = Controls[value];
			}
		}

		public override void Focus()
		{
			base.Focus();
			Controls[0].Focus();
			_focused = Controls[0];
		}

		public override KeyPressedResult NotifyKeyPressed(ConsoleKey key)
		{
			var kpr = _focused.NotifyKeyPressed(key);
			if (kpr == KeyPressedResult.Next || kpr == KeyPressedResult.Prev)
			{
				var index = Controls.IndexOf(_focused);
				index += kpr == KeyPressedResult.Next ? 1 : -1;
				if (index >= 0 && index < Controls.Count)
				{
					_focused = Controls[index];
					_focused.Focus();
				}
				return KeyPressedResult.DoNothing;
			}

			if (kpr == KeyPressedResult.Unfocus)
			{
				_focused = null;
				IsFocused = false;
				return KeyPressedResult.Unfocus;
			}

			return KeyPressedResult.DoNothing;
		}

		protected override void Drawing(ITerminal<char, ConsoleColor> terminal)
		{
			_focused.Draw(terminal);
		}
	}
}