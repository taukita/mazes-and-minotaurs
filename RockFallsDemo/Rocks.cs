using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;

namespace RockFallsDemo
{
	internal class Rocks
	{
		private int[] _ys = {0, 0, 0, 0, 0};
		private int _maxY = 20;
		private int[] _delays = {1, 2, 3, 4, 5};
		private int _counter;
		private int _maxDelay;

		public Rocks()
		{
			_maxDelay = _delays.Max();
		}

		public void Update()
		{
			_counter++;

			for (var i = 0; i < _ys.Length; i++)
			{
				if (_ys[i] < _maxY && _counter % _delays[i] == 0)
				{
					_ys[i]++;
				}
			}

			if (_counter == _maxDelay)
				_counter = 0;
		}

		public void DrawOn(ITerminal<char, ConsoleColor> terminal)
		{
			for (var i = 0; i < _ys.Length; i++)
			{
				terminal.Draw(i, _ys[i], '*', ConsoleColor.White, ConsoleColor.Black);
				terminal.Draw(i, _maxY + 1, '=', ConsoleColor.White, ConsoleColor.Black);
			}
		}

		public bool IsFall => _ys.All(y => y == _maxY);
	}
}
