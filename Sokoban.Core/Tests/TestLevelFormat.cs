namespace Sokoban.Core.Tests
{
	internal class TestLevelFormat : ILevelFormat
	{
		public char Crate { get; } = 'c';
		public char Empty { get; } = '-';
		public char Player { get; } = 'p';
		public char Target { get; } = 't';
		public char Wall { get; } = 'w';
	}
}