using System.Collections.Generic;
using System.Linq;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Adapters;
using MazesAndMinotaurs.Ui.Events;

namespace MazesAndMinotaurs.Ui.Controls.Containers
{
	public class Grid<TGlyph, TColor, TKey> : Container<TGlyph, TColor, TKey>
	{
		public List<int> Columns { get; } = new List<int>();
		public List<int> Rows { get; } = new List<int>();

		protected override void Drawing(ITerminal<TGlyph, TColor> terminal)
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

		protected override void KeyPressed(KeyPressedEventArgs<TKey> args)
		{
			if (KeyboardAdapter.IsTab(args.Key))
			{
				var index = Controls.IndexOf(Focused);
				index++;
				if (index >= Controls.Count)
				{
					index = 0;
				}
				Focused.IsFocused = false;
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
