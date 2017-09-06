using MazesAndMinotaurs.Ui.Adapters;
using SFML.Window;

namespace MazesAndMinotaurs.SfmlTarget
{
	public class SfmlKeyboardAdapter : IKeyboardAdapter<SfmlInput>
	{
		public static readonly SfmlKeyboardAdapter Instance = new SfmlKeyboardAdapter();

		public bool IsKeyboardInput(SfmlInput input)
		{
			return input.KeyEventArgs != null;
		}

		public bool IsUp(SfmlInput input)
		{
			return input.KeyEventArgs.Code == Keyboard.Key.Up;
		}

		public bool IsLeft(SfmlInput input)
		{
			return input.KeyEventArgs.Code == Keyboard.Key.Left;
		}

		public bool IsDown(SfmlInput input)
		{
			return input.KeyEventArgs.Code == Keyboard.Key.Down;
		}

		public bool IsRight(SfmlInput input)
		{
			return input.KeyEventArgs.Code == Keyboard.Key.Right;
		}

		public bool IsEnter(SfmlInput input)
		{
			return input.KeyEventArgs.Code == Keyboard.Key.Return;
		}

		public bool IsTab(SfmlInput input)
		{
			return input.KeyEventArgs.Code == Keyboard.Key.Tab;
		}

		public bool IsEscape(SfmlInput input)
		{
			return input.KeyEventArgs.Code == Keyboard.Key.Escape;
		}

		public bool IsBackspace(SfmlInput input)
		{
			return input.KeyEventArgs.Code == Keyboard.Key.BackSpace;
		}
	}
}
