using MazesAndMinotaurs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Ui
{
	public sealed class ControlsCollection<TGlyph, TColor, TKey> : SmartCollection<Control<TGlyph, TColor, TKey>, Control<TGlyph, TColor, TKey>>
	{
		public ControlsCollection(Control<TGlyph, TColor, TKey> owner) : base(owner)
		{
		}
	}
}
