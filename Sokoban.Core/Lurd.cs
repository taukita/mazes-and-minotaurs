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
	}
}
