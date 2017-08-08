using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Ui
{
	public static class ControlUtils<TGlyph, TColor, TKey>
	{
		public static T FindParent<T>(Control<TGlyph, TColor, TKey> control) where T : Control<TGlyph, TColor, TKey>
		{
			var parent = control.Parent;
			while (parent != null && !(parent is T))
			{
				parent = parent.Parent;
			}
			return parent as T;
		}
	}
}
