using MazesAndMinotaurs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public abstract class Control
	{
		public Action<Control> OnDraw;
		public Action<Control> OnFocus;
		private bool _isFocused;

		public int Left { get; set; }
		public int Top { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public bool IsFocused
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

		public virtual void Focus()
		{
			IsFocused = true;
		}

		public void Draw(ITerminal<char, ConsoleColor> terminal)
		{
			Drawing(terminal);
			OnDraw?.Invoke(this);
		}

		protected abstract void Drawing(ITerminal<char, ConsoleColor> terminal);

		public virtual KeyPressedResult NotifyKeyPressed(ConsoleKey key)
		{
			if (key == ConsoleKey.Escape)
			{
				IsFocused = false;
				return KeyPressedResult.Unfocus;
			}

			if (key == ConsoleKey.Tab)
			{
				IsFocused = false;
				return KeyPressedResult.Next;
			}

			return KeyPressedResult.DoNothing;
		}
	}
}
