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
		public int Left { get; set; }
		public int Top { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public bool IsFocused { get; protected set; }
		public virtual void Focus()
		{
			IsFocused = true;
		}
		public abstract void Draw(ITerminal<char, ConsoleColor> terminal);
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
