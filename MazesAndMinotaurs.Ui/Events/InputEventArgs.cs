namespace MazesAndMinotaurs.Ui.Events
{
	public class InputEventArgs<TInput>
	{
		public InputEventArgs(TInput input)
		{
			Input = input;
		}

		public bool Handled { get; set; }

		public TInput Input { get; }
	}
}