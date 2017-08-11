using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Events;

namespace MazesAndMinotaurs.Ui.Controls.Containers
{
	public class Canvas<TGlyph, TColor, TKey> : Container<TGlyph, TColor, TKey>
	{
		protected override void Drawing(ITerminal<TGlyph, TColor> terminal)
		{
			foreach (var control in Controls)
			{
				control.Draw(terminal);
			}
		}

		protected override void KeyPressed(KeyPressedEventArgs<TKey> args)
		{
			if (KeyboardAdapter.IsTab(args.Key))
			{
				var index = Controls.IndexOf(Focused) + 1;
				Focused = Controls[index % Controls.Count];
				Focused.IsFocused = true;
			}
			else
			{
				Focused.NotifyKeyPressed(args.Key);
			}
		}
	}
}
