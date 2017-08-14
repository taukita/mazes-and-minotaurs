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
	public class Pages<TGlyph, TColor, TInput> : Container<TGlyph, TColor, TInput>
	{
		public event Action<Pages<TGlyph, TColor, TInput>, PropertyChangedExtendedEventArgs<int>> OnPageChanged;

		public int Page
		{
			get
			{
				return Controls.IndexOf(Focused);
			}
			set
			{
				var old = Page;
				if (old != value)
				{
					PropertyChangedExtendedEventArgs<int> args = null;
					if (OnPageChanged != null)
					{
						args = new PropertyChangedExtendedEventArgs<int>(nameof(Page), old, value);
					}
					Focused.IsFocused = false;
					Focused = Controls[value];
					Focused.IsFocused = true;
					OnPageChanged?.Invoke(this, args);
				}
			}
		}

		protected override void Drawing(ITerminal<TGlyph, TColor> terminal)
		{
			Focused?.Draw(terminal);
		}
	}
}
