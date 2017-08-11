using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.Ui.Controls.Containers
{
	public class Panel<TGlyph, TColor, TKey> : Container<TGlyph, TColor, TKey>
	{
		public bool Vertical { get; set; }

		protected override void Drawing(ITerminal<TGlyph, TColor> terminal)
		{
			if (Vertical)
			{
				var sumH = Controls.Sum(c => c.Height);
				var top0 = (Height - sumH) / 2;
				foreach (var control in Controls)
				{
					control.Left = (Width - control.Width) / 2;
					control.Top = top0;
					top0 += control.Height;

					control.Draw(terminal);
				}
			}
			else
			{
				var sumW = Controls.Sum(c => c.Width);
				var left0 = (Width - sumW) / 2;
				foreach (var control in Controls)
				{
					control.Left = left0;
					control.Top = (Height - control.Height) / 2;
					left0 += control.Width;

					control.Draw(terminal);
				}
			}
		}
	}
}
