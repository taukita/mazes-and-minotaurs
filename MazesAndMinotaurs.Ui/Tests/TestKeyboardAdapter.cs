using MazesAndMinotaurs.Ui.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Ui.Tests
{
	internal class TestKeyboardAdapter : IKeyboardAdapter<TestKey>
	{
		public bool IsDown(TestKey input)
		{
			return input == TestKey.Down;
		}

		public bool IsEnter(TestKey input)
		{
			return input == TestKey.Enter;
		}

		public bool IsTab(TestKey input)
		{
			return input == TestKey.Tab;
		}

		public bool IsEscape(TestKey input)
		{
			return input == TestKey.Escape;
		}

		public bool IsBackspace(TestKey input)
		{
			return input == TestKey.Backspace;
		}

		public bool IsLeft(TestKey input)
		{
			return input == TestKey.Left;
		}

		public bool IsRight(TestKey input)
		{
			return input == TestKey.Right;
		}

		public bool IsKeyboardInput(TestKey input)
		{
			return true;
		}

		public bool IsUp(TestKey input)
		{
			return input == TestKey.Up;
		}
	}
}
