using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;

namespace MazesAndMinotaurs.SfmlTarget
{
	public class SfmlInput
	{
		public SfmlInput(KeyEventArgs keyEventArgs)
		{
			KeyEventArgs = keyEventArgs;
		}

		public SfmlInput(MouseButtonEventArgs mouseButtonEventArgs)
		{
			MouseButtonEventArgs = mouseButtonEventArgs;
		}

		public KeyEventArgs KeyEventArgs { get; }
		public MouseButtonEventArgs MouseButtonEventArgs { get; }
	}
}
