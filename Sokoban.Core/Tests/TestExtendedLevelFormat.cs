namespace Sokoban.Core.Tests
{
	internal class TestExtendedLevelFormat : TestLevelFormat, IExtendedLevelFormat
	{
		public char CrateOverTarget { get; } = 'C';
		public char PlayerOverTarget { get; } = 'P';
	}
}