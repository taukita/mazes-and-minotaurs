using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.ConsoleTarget.Ui.Events
{
	public class KeyPressedEventArgs : EventArgs
	{
		public KeyPressedEventArgs(ConsoleKey key)
		{
			Key = key;
		}

		public bool Handled { get; set; }

		public ConsoleKey Key { get; }
	}
}
