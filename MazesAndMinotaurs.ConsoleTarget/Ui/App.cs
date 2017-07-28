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
		private Control _rootControl;
		private readonly BufferTerminal<char, ConsoleColor> _terminal;

		public App(Control rootControl)
		{
			_rootControl = rootControl;
			_rootControl.Focus();
			_terminal = new BufferTerminal<char, ConsoleColor>(new ConsoleTerminal()) { Foreground = ConsoleColor.White, Background = ConsoleColor.Black };
		}

		public ITerminal<char, ConsoleColor> Terminal => _terminal;

		public void Run()
		{
			while (_rootControl != null)
			{
				Draw();
				var kpr = _rootControl.NotifyKeyPressed(Console.ReadKey(true).Key);
				if (kpr == KeyPressedResult.Unfocus)
				{
					_rootControl = null;
				}
			}
		}

		protected void Draw()
		{
			_rootControl.Draw(_terminal);
			_terminal.Flush();
		}
	}
}
