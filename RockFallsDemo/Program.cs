using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MazesAndMinotaurs.ConsoleTarget;

namespace RockFallsDemo
{
	class Program
	{
		private static Metronome _metronome;
		private static DateTime _old;

		static void Main(string[] args)
		{
			Console.CursorVisible = false;
			var autoEvent = new AutoResetEvent(false);
			var rocks = new Rocks();
			var terminal = new ConsoleTerminal();
			_metronome = new Metronome(60);
			_metronome.Beat += () =>
			{
				rocks.Update();
				Console.Clear();
				rocks.DrawOn(terminal);
				if (rocks.IsFall)
					autoEvent.Set();
			};
			rocks.DrawOn(terminal);
			var stateTimer = new Timer(Update, autoEvent, 1000, 100);
			autoEvent.WaitOne();
			stateTimer.Dispose();
			Console.ReadKey(true);
		}

		private static void Update(object state)
		{
			var now = DateTime.Now;
			_metronome.Update((now - _old).Milliseconds * 1000);
			_old = now;
		}
	}
}
