using MazesAndMinotaurs.ConsoleTarget;
using MazesAndMinotaurs.ConsoleTarget.Ui;
using MazesAndMinotaurs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.ConsoleTarget.Ui.Events;

namespace MazesAndMinotaurs.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			//new ConsoleTargetApp().Run();
			new SfmlTargetApp().Run();
		}
	}
}

