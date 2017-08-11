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
		public bool IsUp(ConsoleKey key)
		{
			return key == ConsoleKey.UpArrow;
		}

		public bool IsLeft(ConsoleKey key)
		{
			return key == ConsoleKey.LeftArrow;
		}

		public bool IsDown(ConsoleKey key)
		{
			return key == ConsoleKey.DownArrow;
		}

		public bool IsRight(ConsoleKey key)
		{
			return key == ConsoleKey.RightArrow;
		}

		public bool IsEnter(ConsoleKey key)
		{
			return key == ConsoleKey.Enter;
		}

		public bool IsTab(ConsoleKey key)
		{
			return key == ConsoleKey.Tab;
		}

		public bool IsEscape(ConsoleKey key)
		{
			return key == ConsoleKey.Escape;
		}
	}
}
