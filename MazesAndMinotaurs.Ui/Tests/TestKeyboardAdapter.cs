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
		public bool IsDown(TestKey key)
		{
			return key == TestKey.Down;
		}

		public bool IsEnter(TestKey key)
		{
			return key == TestKey.Enter;
		}

		public bool IsTab(TestKey key)
		{
			return key == TestKey.Tab;
		}

		public bool IsEscape(TestKey key)
		{
			return key == TestKey.Escape;
		}

		public bool IsBackspace(TestKey key)
		{
			return key == TestKey.Backspace;
		}

		public bool IsLeft(TestKey key)
		{
			return key == TestKey.Left;
		}

		public bool IsRight(TestKey key)
		{
			return key == TestKey.Right;
		}

		public bool IsUp(TestKey key)
		{
			return key == TestKey.Up;
		}
	}
}
