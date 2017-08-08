using System.Linq;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Adapters;
using MazesAndMinotaurs.Ui.Events;

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
			base.FocusChanged(args);
			if (args.NewValue && Controls.Any())
			{
				Focused = Controls.First();
				Focused.IsFocused = true;
			}
		}

		protected override void KeyPressed(KeyPressedEventArgs<TKey> args)
		{
			Focused?.NotifyKeyPressed(args.Key);
		}
	}
}