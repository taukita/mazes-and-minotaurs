using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Ui.Events
{
	public class KeyPressedEventArgs<TKey>
	{
		public KeyPressedEventArgs(TKey key)
		{
			Key = key;
		}

		public bool Handled { get; set; }

		public TKey Key { get; }
	}
}
