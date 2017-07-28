using System;
using MazesAndMinotaurs.ConsoleTarget.Ui.Events;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class CanvasContainerControl : ContainerControl
	{
		protected override void Drawing(ITerminal<char, ConsoleColor> terminal)
		{
			foreach (var control in Controls)
			{
				control.Draw(terminal);
			}
		}

		protected override void KeyPressed(KeyPressedEventArgs args)
		{
			if (args.Key == ConsoleKey.Tab)
			{
				var index = Controls.IndexOf(Focused);
				index++;
				if (index >= Controls.Count)
				{
					index = 0;
				}
				Focused = Controls[index];
				Focused.IsFocused = true;
			}
			else
			{
				Focused.NotifyKeyPressed(args.Key);
			}
		}
	}
}