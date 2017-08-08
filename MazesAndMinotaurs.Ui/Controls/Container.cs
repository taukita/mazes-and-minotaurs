using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Adapters;

namespace MazesAndMinotaurs.Ui.Controls
{
	public abstract class Container<TGlyph, TColor, TKey> : Control<TGlyph, TColor, TKey>
	{
		protected Control<TGlyph, TColor, TKey> Focused;

		protected Container(IKeyboardAdapter<TKey> keyboardAdapter) : base(keyboardAdapter)
		{
			Controls = new ControlsCollection<TGlyph, TColor, TKey>(this);
		}

		public ControlsCollection<TGlyph, TColor, TKey> Controls { get; }

		protected override void FocusChanged(PropertyChangedExtendedEventArgs<bool> args)
		{
			if (args.NewValue && Controls.Any())
			{
				Focused = Controls.First();
				Focused.IsFocused = true;
			}
		}
	}
}
