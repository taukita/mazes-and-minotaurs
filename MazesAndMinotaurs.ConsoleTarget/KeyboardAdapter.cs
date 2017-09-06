using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Ui.Adapters;

namespace MazesAndMinotaurs.ConsoleTarget
{
	public class KeyboardAdapter : IKeyboardAdapter<ConsoleKey>
	{
		public bool IsKeyboardInput(ConsoleKey input)
		{
			return true;
		}

		public bool IsUp(ConsoleKey input)
		{
			return input == ConsoleKey.UpArrow;
		}

		public bool IsLeft(ConsoleKey input)
		{
			return input == ConsoleKey.LeftArrow;
		}

		public bool IsDown(ConsoleKey input)
		{
			return input == ConsoleKey.DownArrow;
		}

		public bool IsRight(ConsoleKey input)
		{
			return input == ConsoleKey.RightArrow;
		}

		public bool IsEnter(ConsoleKey input)
		{
			return input == ConsoleKey.Enter;
		}

		public bool IsTab(ConsoleKey input)
		{
			return input == ConsoleKey.Tab;
		}

		public bool IsEscape(ConsoleKey input)
		{
			return input == ConsoleKey.Escape;
		}

		public bool IsBackspace(ConsoleKey input)
		{
			return input == ConsoleKey.Backspace;
		}
	}
}
