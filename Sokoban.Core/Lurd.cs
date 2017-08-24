using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Core
{
	internal class Lurd
	{
		private readonly string _lurd;

		public Lurd(string lurd)
		{
			if (lurd.Any(@char => "LlUuRrDd".IndexOf(@char) < 0))
			{
				throw new ArgumentException(nameof(lurd));
			}
			_lurd = lurd;
		}

		public static IEnumerable<Lurd> All(int length)
		{
			var count = (int) Math.Pow(4, length);
			for (var y = 0; y < count; y++)
			{
				var sb = new StringBuilder(length);
				for (var x = 0; x < length; x++)
				{
					sb.Append(F(x, y, length - 1));
				}
				yield return new Lurd(sb.ToString());
			}
		}

		public override string ToString()
		{
			return _lurd;
		}

		public bool TryOn(Level level)
		{
			var steps = 0;
			var moved = false;
			foreach (var @char in _lurd)
			{
				switch (@char)
				{
					case 'L':
					case 'l':
						moved = level.TryMoveLeft();
						break;
					case 'U':
					case 'u':
						moved = level.TryMoveUp();
						break;
					case 'R':
					case 'r':
						moved = level.TryMoveRight();
						break;
					case 'D':
					case 'd':
						moved = level.TryMoveDown();
						break;
				}
				if (!moved)
					break;
				steps++;
			}

			if (!moved)
			{
				for (var i = 0; i < steps; i++)
				{
					level.Undo();
				}
			}

			return moved;
		}

		internal static char F(int x, int y, int mx)
		{
			var d = (int) Math.Pow(4, mx - x);
			return "lurd"[y/d%4];
		}
	}
}