using System;
using MazesAndMinotaurs.ConsoleTarget.Ui.Events;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public abstract class Control
	{
		public Action<Control> OnDraw;
		public Action<Control> OnFocus;
		public Action<Control, KeyPressedEventArgs> OnKeyPressed;

		private bool _isFocused;

		public int Left { get; set; }
		public int Top { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public BorderTheme BorderTheme { get; set; } = new BorderTheme('#');
		public ColorTheme ColorTheme { get; set; } = ColorTheme.Create(ConsoleColor.Black, ConsoleColor.White);

		public virtual bool IsFocused
		{
			get
			{
				return _isFocused;
			}
			set
			{
				_isFocused = value;
				if (_isFocused)
				{
					OnFocus?.Invoke(this);
				}
			}
		}

		public void Draw(ITerminal<char, ConsoleColor> terminal)
		{
			Drawing(terminal);
			OnDraw?.Invoke(this);
		}

		protected abstract void Drawing(ITerminal<char, ConsoleColor> terminal);

		public void NotifyKeyPressed(ConsoleKey key)
		{
			var args = new KeyPressedEventArgs(key);
			OnKeyPressed?.Invoke(this, args);
			if (!args.Handled)
			{
				KeyPressed(args);
			}
		}

		protected T Focused<T> (T @object)
		{
			return @object is IFocusAwareObject<T> ? ((IFocusAwareObject<T>)@object).Focus(IsFocused) : @object;
		}

		protected virtual void KeyPressed(KeyPressedEventArgs args)
		{
			
		}
	}
}
