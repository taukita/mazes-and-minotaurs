using MazesAndMinotaurs.Ui.Controls.Containers;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.SfmlTarget.Ui
{
	public sealed class Pages : Pages<SfmlGlyph, Color, Keyboard.Key>
	{
		public Pages()
		{
			KeyboardAdapter = SfmlKeyboardAdapter.Instance;
		}
	}
}
