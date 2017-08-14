using System;
using System.Collections.Specialized;
using System.Linq;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Adapters;
using MazesAndMinotaurs.Ui.Events;

namespace MazesAndMinotaurs.Ui.Controls
{
	public abstract class Container<TGlyph, TColor, TInput> : Control<TGlyph, TColor, TInput>
	{
		protected Control<TGlyph, TColor, TInput> Focused;

		protected Container()
		{
			Controls = new ControlsCollection<TGlyph, TColor, TInput>(this);
			Controls.CollectionChanged += ControlsOnCollectionChanged;
		}

		private void ControlsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (var control in e.NewItems.Cast<Control<TGlyph, TColor, TInput>>())
					{
						control.OnFocusChanged += OnControlFocusChanged;
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (var control in e.OldItems.Cast<Control<TGlyph, TColor, TInput>>())
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

		private void OnControlFocusChanged(Control<TGlyph, TColor, TInput> control, PropertyChangedExtendedEventArgs<bool> e)
		{
			if (e.NewValue)
			{
				Focused = control;
			}
		}

		public ControlsCollection<TGlyph, TColor, TInput> Controls { get; }

		protected override void FocusChanged(PropertyChangedExtendedEventArgs<bool> args)
		{
			base.FocusChanged(args);
			if (args.NewValue && Controls.Any())
			{
				Controls.First().IsFocused = true;
			}
			else if (!args.NewValue)
			{
				foreach (var control in Controls)
				{
					control.IsFocused = false;
				}
			}
		}

		protected override void KeyboardInput(InputEventArgs<TInput> args)
		{
			Focused?.NotifyKeyboardInput(args.Input);
		}

		protected override void MouseInput(InputEventArgs<TInput> args)
		{
			foreach (var control in Controls)
			{
				control.NotifyMouseInput(args.Input);
			}
		}
	}
}