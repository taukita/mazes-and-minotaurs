using System;
using System.Collections.ObjectModel;
using System.Linq;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Adapters;
using MazesAndMinotaurs.Ui.Events;

namespace MazesAndMinotaurs.Ui
{
	public abstract class Control<TGlyph, TColor, TInput> : ICollectionItem<Control<TGlyph, TColor, TInput>>
	{
		private ObservableCollection<Control<TGlyph, TColor, TInput>> _collection;
		private bool _isFocused;
		private IKeyboardAdapter<TInput> _keyboardAdapter;
		private IMouseAdapter<TInput> _mouseAdapter;
		public Action<Control<TGlyph, TColor, TInput>> OnDraw;
		public Action<Control<TGlyph, TColor, TInput>, PropertyChangedExtendedEventArgs<bool>> OnFocusChanged;
		public Action<Control<TGlyph, TColor, TInput>, InputEventArgs<TInput>> OnKeyboardInput;
		public Action<Control<TGlyph, TColor, TInput>, InputEventArgs<TInput>> OnMouseInput;

		public ColorTheme<TColor> ColorTheme { get; set; }

		public IKeyboardAdapter<TInput> KeyboardAdapter
		{
			get { return _keyboardAdapter ?? Parent?.KeyboardAdapter; }
			set { _keyboardAdapter = value; }
		}

		public IMouseAdapter<TInput> MouseAdapter
		{
			get { return _mouseAdapter ?? Parent?.MouseAdapter; }
			set { _mouseAdapter = value; }
		}

		public int Height { get; set; }

		public bool IsFocused
		{
			get { return _isFocused; }
			set
			{
				if (_isFocused != value)
				{
					var args = new PropertyChangedExtendedEventArgs<bool>(nameof(IsFocused), _isFocused, value);
					_isFocused = value;
					OnFocusChanged?.Invoke(this, args);
					if (!args.Handled)
						FocusChanged(args);
				}
			}
		}

		public int Left { get; set; }
		public Control<TGlyph, TColor, TInput> Parent => (Collection as ControlsCollection<TGlyph, TColor, TInput>)?.Owner;
		public int Top { get; set; }
		public int Width { get; set; }

		public ObservableCollection<Control<TGlyph, TColor, TInput>> Collection
		{
			get { return _collection; }

			set
			{
				if (_collection != value)
				{
					var collection = _collection;
					_collection = null;
					if (collection != null && collection.Count > 0)
						collection.Remove(this);
					_collection = value;
					if (_collection != null && !_collection.Contains(this))
						_collection.Add(this);
				}
			}
		}

		public void Draw(ITerminal<TGlyph, TColor> terminal)
		{
			Drawing(terminal);
			OnDraw?.Invoke(this);
		}

		public void NotifyKeyboardInput(TInput input)
		{
			if (!IsFocused)
				throw new InvalidOperationException("Not focused controls should not be notified about keyboard input.");
			var args = new InputEventArgs<TInput>(input);
			OnKeyboardInput?.Invoke(this, args);
			if (!args.Handled)
				KeyboardInput(args);
		}

		public void NotifyMouseInput(TInput input)
		{
			var args = new InputEventArgs<TInput>(input);
			OnMouseInput?.Invoke(this, args);
			if (!args.Handled)
				MouseInput(args);
		}

		protected abstract void Drawing(ITerminal<TGlyph, TColor> terminal);

		protected virtual void FocusChanged(PropertyChangedExtendedEventArgs<bool> args)
		{
			if (args.NewValue && Collection != null)
				foreach (var control in Collection.Where(c => c != this))
					control.IsFocused = false;
		}

		protected virtual void KeyboardInput(InputEventArgs<TInput> args)
		{
		}

		protected virtual void MouseInput(InputEventArgs<TInput> args)
		{
		}
	}
}