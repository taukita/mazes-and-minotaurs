using System;
using MazesAndMinotaurs.ConsoleTarget.Ui.Events;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class PagesContainerControl : ContainerControl
	{
		public int Page
		{
			get
			{
				return Controls.IndexOf(Focused);
			}
			set
			{
				Controls[value].IsFocused = true;
				Focused = Controls[value];
			}
		}

		protected override void Drawing(ITerminal<char, ConsoleColor> terminal)
		{
			Focused.Draw(terminal);
		}

		protected override void KeyPressed(KeyPressedEventArgs args)
		{
			Focused.NotifyKeyPressed(args.Key);
		}
	}
}