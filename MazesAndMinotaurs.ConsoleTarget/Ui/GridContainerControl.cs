using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.ConsoleTarget.Ui.Events;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class GridContainerControl : ContainerControl
	{
		public List<int> Columns { get; } = new List<int>();
		public List<int> Rows { get; } = new List<int>();

		protected override void Drawing(ITerminal<char, ConsoleColor> terminal)
		{
			var allColumns = Columns.Sum();
			var allRows = Rows.Sum();

			var top = Top;
			var left = Left;

			for (var y = 0; y < Rows.Count; y++)
			{
				var height = Rows[y] * Height / allRows;
				for (var x = 0; x < Columns.Count; x++)
				{
					var width = Columns[x] * Width / allColumns;
					var control = Controls[x + y * Columns.Count];
					control.Left = left;
					control.Top = top;
					control.Width = width;
					control.Height = height;
					control.Draw(terminal);
					left += width;
				}
				top += height;
				left = Left;
			}
		}

		protected override void KeyPressed(KeyPressedEventArgs args)
		{
			if (args.Key == ConsoleKey.Tab)
			{
				var index = Controls.IndexOf(Focused);
				index++;
				if (index >= Controls.Count)
				{
					index = 0;
				}
				Focused = Controls[index];
				Focused.IsFocused = true;
			}
			else
			{
				Focused.NotifyKeyPressed(args.Key);
			}
		}
	}
}
