using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Events;

namespace MazesAndMinotaurs.Ui.Controls.Containers
{
	public class Canvas<TGlyph, TColor, TInput> : Container<TGlyph, TColor, TInput>
	{
		protected override void Drawing(ITerminal<TGlyph, TColor> terminal)
		{
			foreach (var control in Controls)
			{
				control.Draw(terminal);
			}
		}

		protected override void KeyboardInput(InputEventArgs<TInput> args)
		{
			if (KeyboardAdapter.IsTab(args.Input))
			{
				var index = Controls.IndexOf(Focused) + 1;
				Focused = Controls[index % Controls.Count];
				Focused.IsFocused = true;
			}
			else
			{
				Focused.NotifyKeyboardInput(args.Input);
			}
		}
	}
}
