using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public abstract class ContainerControl : Control
	{
		protected Control Focused;

		public override bool IsFocused
		{
			get
			{
				return base.IsFocused;
			}
			set
			{
				base.IsFocused = value;
				if (value)
				{
					Focused = Controls[0];
					Focused.IsFocused = true;
				}
			}
		}

		public Controls Controls { get; } = new Controls();
	}
}
