﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core
{
	public class BufferTerminal<TGlyph, TTerminalColor> : AbstractTerminal<TGlyph, TTerminalColor>
	{
		private readonly ITerminal<TGlyph, TTerminalColor> _terminal;
		private Buffer _new = new Buffer();
		private Buffer _old = new Buffer();

		public BufferTerminal(ITerminal<TGlyph, TTerminalColor> terminal)
		{
			_terminal = terminal;
		}

		public override void Clear(int x, int y)
		{
			_new.Remove(Tuple.Create(x, y));
		}

		protected override void Drawing(int x, int y, TGlyph glyph, TTerminalColor foreground, TTerminalColor background)
		{
			_new[Tuple.Create(x, y)] = Tuple.Create(glyph, foreground, background);
		}

		public void Flush()
		{
			foreach (var pair in _old)
			{
				if (!_new.ContainsKey(pair.Key))
				{
					_terminal.Clear(pair.Key.Item1, pair.Key.Item2);
				}
			}
			foreach (var pair in _new)
			{
				if (_old.ContainsKey(pair.Key) && _old[pair.Key].Equals(pair.Value))
				{
					continue;
				}
				_terminal.Draw(pair.Key.Item1, pair.Key.Item2, pair.Value.Item1, pair.Value.Item2, pair.Value.Item3);
			}
			_old = _new;
			_new = new Buffer();
		}

		private class Buffer : Dictionary<Tuple<int, int>, Tuple<TGlyph, TTerminalColor, TTerminalColor>>
		{
		}
	}
}