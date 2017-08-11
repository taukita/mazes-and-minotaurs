using System;
using System.Collections.Specialized;
using System.Linq;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Adapters;
using MazesAndMinotaurs.Ui.Events;

namespace MazesAndMinotaurs.Ui.Controls
{
	public abstract class Container<TGlyph, TColor, TKey> : Control<TGlyph, TColor, TKey>
	{
		protected Control<TGlyph, TColor, TKey> Focused;

		protected Container()
		{
			Controls = new ControlsCollection<TGlyph, TColor, TKey>(this);
			Controls.CollectionChanged += ControlsOnCollectionChanged;
		}

		private void ControlsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (var control in e.NewItems.Cast<Control<TGlyph, TColor, TKey>>())
					{
						control.OnFocusChanged += OnControlFocusChanged;
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (var control in e.OldItems.Cast<Control<TGlyph, TColor, TKey>>())
					{
						// ReSharper disable once DelegateSubtraction
						control.OnFocusChanged -= OnControlFocusChanged;
					}
					break;
				case NotifyCollectionChangedAction.Replace:
					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Reset:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void OnControlFocusChanged(Control<TGlyph, TColor, TKey> control, PropertyChangedExtendedEventArgs<bool> e)
		{
			if (e.NewValue)
			{
				Focused = control;
			}
		}

		public ControlsCollection<TGlyph, TColor, TKey> Controls { get; }

		protected override void FocusChanged(PropertyChangedExtendedEventArgs<bool> args)
		{
			base.FocusChanged(args);
			if (args.NewValue && Controls.Any())
			{
				Controls.First().IsFocused = true;
			}
		}

		protected override void KeyPressed(KeyPressedEventArgs<TKey> args)
		{
			Focused?.NotifyKeyPressed(args.Key);
		}
	}
}