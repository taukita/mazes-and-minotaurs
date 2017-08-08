using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Adapters;
using MazesAndMinotaurs.Ui.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MazesAndMinotaurs.Ui
{
	public abstract class Control<TGlyph, TColor, TKey> : ICollectionItem<Control<TGlyph, TColor, TKey>>
	{
		private ObservableCollection<Control<TGlyph, TColor, TKey>> _collection;
		private bool _isFocused;
		protected readonly IKeyboardAdapter<TKey> KeyboardAdapter;

		protected Control(IKeyboardAdapter<TKey> keyboardAdapter)
		{
			KeyboardAdapter = keyboardAdapter;
		}

		public Action<Control<TGlyph, TColor, TKey>> OnDraw;
		public Action<Control<TGlyph, TColor, TKey>, KeyPressedEventArgs<TKey>> OnKeyPressed;
		public Action<Control<TGlyph, TColor, TKey>, PropertyChangedExtendedEventArgs<bool>> OnFocusChanged;

		public int Left { get; set; }
		public int Top { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public ColorTheme<TColor> ColorTheme { get; set; }

		public bool IsFocused
		{
			get
			{
				return _isFocused;
			}
			set
			{
				if (_isFocused != value)
				{
					var args = new PropertyChangedExtendedEventArgs<bool>(nameof(IsFocused), _isFocused, value);
					_isFocused = value;
					OnFocusChanged?.Invoke(this, args);
					FocusChanged(args);
				}
			}
		}

		public Control<TGlyph, TColor, TKey> Parent
		{
			get
			{
				return (Collection as ControlsCollection<TGlyph, TColor, TKey>)?.Owner;
			}
		}

		public ObservableCollection<Control<TGlyph, TColor, TKey>> Collection
		{
			get
			{
				return _collection;
			}

			set
			{
				if (_collection != value)
				{
					var collection = _collection;
					_collection = null;
					if (collection != null && collection.Count > 0)
					{
						collection.Remove(this);
					}
					_collection = value;
					if (_collection != null && !_collection.Contains(this))
					{
						_collection.Add(this);
					}
				}
			}
		}

		public void Draw(ITerminal<TGlyph, TColor> terminal)
		{
			Drawing(terminal);
			OnDraw?.Invoke(this);
		}

		public void NotifyKeyPressed(TKey key)
		{
			var args = new KeyPressedEventArgs<TKey>(key);
			OnKeyPressed?.Invoke(this, args);
			if (!args.Handled)
			{
				KeyPressed(args);
			}
		}

		protected abstract void Drawing(ITerminal<TGlyph, TColor> terminal);

		protected virtual void KeyPressed(KeyPressedEventArgs<TKey> args)
		{

		}

		protected virtual void FocusChanged(PropertyChangedExtendedEventArgs<bool> args)
		{

		}
	}
}
