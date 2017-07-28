using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public abstract class ContainerControl : Control
	{
		public Controls Controls { get; } = new Controls();
	}
}
