using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.SfmlTarget;
using MazesAndMinotaurs.Ui.Adapters;

namespace Sokoban.SfmlTarget
{
	internal class MouseAdapter : IMouseAdapter<SfmlInput>
	{
		private readonly uint _glyphWidth;
		private readonly uint _glyphHeight;

		public MouseAdapter(uint glyphWidth, uint glyphHeight)
		{
			_glyphWidth = glyphWidth;
			_glyphHeight = glyphHeight;
		}

		public bool IsMouseInput(SfmlInput input)
		{
			return input.MouseButtonEventArgs != null;
		}

		public int GetX(SfmlInput input)
		{
			return (int)(input.MouseButtonEventArgs.X / _glyphWidth);
		}

		public int GetY(SfmlInput input)
		{
			return (int)(input.MouseButtonEventArgs.Y / _glyphHeight);
		}
	}
}
