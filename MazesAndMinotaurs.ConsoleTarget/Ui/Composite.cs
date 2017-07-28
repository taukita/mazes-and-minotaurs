using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public abstract class Composite : Control
	{
		public List<Control> Controls { get; } = new List<Control>();
	}
}
