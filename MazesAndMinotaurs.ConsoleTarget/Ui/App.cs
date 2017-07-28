using MazesAndMinotaurs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class App
	{
		private IEnumerable<Control> _controls;
		private Control _focused;
		private BufferTerminal<char, ConsoleColor> _terminal;

		public App(IEnumerable<Control> controls)
		{
			_controls = controls;
			_focused = _controls.First();
			_focused.Focus();
			_terminal = new BufferTerminal<char, ConsoleColor>(new ConsoleTerminal()) { Foreground = ConsoleColor.White, Background = ConsoleColor.Black };
		}

		public ITerminal<char, ConsoleColor> Terminal => _terminal;

		public void Run()
		{
			while (_focused != null)
			{
				Draw();
				var kpr = _focused.NotifyKeyPressed(Console.ReadKey(true).Key);
				switch(kpr)
				{
					case KeyPressedResult.Next:
						if (_focused == _controls.Last())
						{
							_focused = _controls.First();
						}
						else
						{
							_focused = _controls.SkipWhile(c => c != _focused).Skip(1).First();
						}
						_focused.Focus();
						break;
					case KeyPressedResult.Unfocus:
						_focused = null;
						break;
				}
			}
		}

		protected void Draw()
		{
			foreach (var c in _controls)
			{
				c.Draw(_terminal);
			}
			_terminal.Flush();
		}
	}
}
