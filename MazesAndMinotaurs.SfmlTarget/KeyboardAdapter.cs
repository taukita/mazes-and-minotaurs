using MazesAndMinotaurs.Ui.Adapters;
using SFML.Window;

namespace MazesAndMinotaurs.SfmlTarget
{
	public class SfmlKeyboardAdapter : IKeyboardAdapter<Keyboard.Key>
	{
		public static readonly SfmlKeyboardAdapter Instance = new SfmlKeyboardAdapter();

		public bool IsDown(Keyboard.Key input)
		{
			return input == Keyboard.Key.Down;
		}

		public bool IsEnter(Keyboard.Key input)
		{
			return input == Keyboard.Key.Return;
		}

		public bool IsTab(Keyboard.Key input)
		{
			return input == Keyboard.Key.Tab;
		}

		public bool IsEscape(Keyboard.Key input)
		{
			return input == Keyboard.Key.Escape;
		}

		public bool IsBackspace(Keyboard.Key input)
		{
			return input == Keyboard.Key.BackSpace;
		}

		public bool IsLeft(Keyboard.Key input)
		{
			return input == Keyboard.Key.Left;
		}

		public bool IsRight(Keyboard.Key input)
		{
			return input == Keyboard.Key.Right;
		}

		public bool IsUp(Keyboard.Key input)
		{
			return input == Keyboard.Key.Up;
		}
	}
}
