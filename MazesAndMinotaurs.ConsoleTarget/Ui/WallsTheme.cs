using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class WallsTheme
	{
		public static readonly WallsTheme Default;
		public static readonly WallsTheme Box;

		static WallsTheme()
		{
			Default = new WallsTheme();

			Default[false, false, false, false] = '#';

			Default[true, false, false, false] = '#';
			Default[false, true, false, false] = '#';
			Default[false, false, true, false] = '#';
			Default[false, false, false, true] = '#';

			Default[true, true, false, false] = '#';
			Default[true, false, true, false] = '#';
			Default[true, false, false, true] = '#';
			Default[false, true, true, false] = '#';
			Default[false, true, false, true] = '#';
			Default[false, false, true, true] = '#';

			Default[true, true, true, false] = '#';
			Default[true, true, false, true] = '#';
			Default[true, false, true, true] = '#';
			Default[false, true, true, true] = '#';

			Default[true, true, true, true] = '#';

			Box = new WallsTheme();

			Box[false, false, false, false] = '┼';

			Box[true, false, false, false] = '│';
			Box[false, true, false, false] = '─';
			Box[false, false, true, false] = '│';
			Box[false, false, false, true] = '─';

			Box[true, true, false, false] = '┘';
			Box[true, false, true, false] = '│';
			Box[true, false, false, true] = '└';
			Box[false, true, true, false] = '┐';
			Box[false, true, false, true] = '─';
			Box[false, false, true, true] = '┌';

			Box[true, true, true, false] = '┤';
			Box[true, true, false, true] = '┴';
			Box[true, false, true, true] = '├';
			Box[false, true, true, true] = '┬';

			Box[true, true, true, true] = '┼';
		}

		private Dictionary<Direction, char> _dict = new Dictionary<Direction, char>();

		private WallsTheme()
		{
		}

		public char this[bool up, bool left, bool down, bool right]
		{
			get
			{
				return _dict[GetDirection(up, left, down, right)];
			}
			private set
			{
				_dict[GetDirection(up, left, down, right)] = value;
			}
		}

		private Direction GetDirection(bool up, bool left, bool down, bool right)
		{
			return (up ? Direction.Up : 0) |
				(left ? Direction.Left : 0) |
				(down ? Direction.Down : 0) |
				(right ? Direction.Right : 0);
		}

		[Flags]
		private enum Direction
		{
			Up = 1 << 0,
			Left = 1 << 1,
			Down = 1 << 2,
			Right = 1 << 3
		}
	}
}
