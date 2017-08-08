using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Adapters;
using MazesAndMinotaurs.Ui.Events;

namespace MazesAndMinotaurs.Ui.Controls.Containers
{
	public class Pages<TGlyph, TColor, TKey> : Container<TGlyph, TColor, TKey>
	{
		public Pages(IKeyboardAdapter<TKey> keyboardAdapter) : base(keyboardAdapter)
		{
		}

		public int Page
		{
			get
			{
				return Controls.IndexOf(Focused);
			}
			set
			{
				Focused.IsFocused = false;
				Focused = Controls[value];
				Focused.IsFocused = true;
			}
		}

		protected override void Drawing(ITerminal<TGlyph, TColor> terminal)
		{
			Focused?.Draw(terminal);
		}
	}
}
