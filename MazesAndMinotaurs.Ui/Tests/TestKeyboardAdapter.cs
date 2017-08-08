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
		public bool isDown(TestKey key)
		{
			return key == TestKey.Down;
		}

		public bool isEnter(TestKey key)
		{
			return key == TestKey.Enter;
		}

		public bool isLeft(TestKey key)
		{
			return key == TestKey.Left;
		}

		public bool isRight(TestKey key)
		{
			return key == TestKey.Right;
		}

		public bool isUp(TestKey key)
		{
			return key == TestKey.Up;
		}
	}
}
