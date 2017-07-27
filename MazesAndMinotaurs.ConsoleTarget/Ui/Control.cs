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
		protected Control(ITerminal<char, ConsoleColor> terminal)
		{
			Terminal = terminal;
		}

		public ITerminal<char, ConsoleColor> Terminal { get; }

		public int Left { get; set; }
		public int Top { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public abstract bool IsFocused { get; }
		public abstract void Focus();
		public abstract void Draw();
	}
}
