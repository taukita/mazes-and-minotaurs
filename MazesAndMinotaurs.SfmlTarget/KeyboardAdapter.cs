﻿using MazesAndMinotaurs.Ui.Adapters;
using SFML.Window;

namespace MazesAndMinotaurs.SfmlTarget
{
	public class SfmlKeyboardAdapter : IKeyboardAdapter<Keyboard.Key>
	{
		public static readonly SfmlKeyboardAdapter Instance = new SfmlKeyboardAdapter();

		public bool IsDown(Keyboard.Key key)
		{
			return key == Keyboard.Key.Down;
		}

		public bool IsEnter(Keyboard.Key key)
		{
			return key == Keyboard.Key.Return;
		}

		public bool IsTab(Keyboard.Key key)
		{
			return key == Keyboard.Key.Tab;
		}

		public bool IsEscape(Keyboard.Key key)
		{
			return key == Keyboard.Key.Escape;
		}

		public bool IsBackspace(Keyboard.Key key)
		{
			return key == Keyboard.Key.BackSpace;
		}

		public bool IsLeft(Keyboard.Key key)
		{
			return key == Keyboard.Key.Left;
		}

		public bool IsRight(Keyboard.Key key)
		{
			return key == Keyboard.Key.Right;
		}

		public bool IsUp(Keyboard.Key key)
		{
			return key == Keyboard.Key.Up;
		}
	}
}
