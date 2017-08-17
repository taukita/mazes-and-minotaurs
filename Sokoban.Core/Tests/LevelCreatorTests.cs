using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sokoban.Core.Tests
{
	[TestFixture]
	internal class LevelCreatorTests
	{
		[Test]
		public void BasicTest()
		{
			const string levelData = @"
#####
#.$@-#
#####";
			var creator = new LevelCreator();
			var level = creator.Create(levelData);

			Assert.AreEqual(12, level.Walls.Count);
			Assert.AreEqual(1, level.Targets.Count);
			Assert.AreEqual(1, level.Crates.Count);

			Assert.AreEqual(3, level.PlayerX);
			Assert.AreEqual(1, level.PlayerY);

			Assert.AreEqual(2, level.Crates.Single().X);
			Assert.AreEqual(1, level.Crates.Single().Y);

			Assert.AreEqual(1, level.Targets.Single().Item1);
			Assert.AreEqual(1, level.Targets.Single().Item2);

			Assert.AreEqual(6, level.Width);
			Assert.AreEqual(3, level.Height);
		}

		[Test]
		public void CustomFormatTest()
		{
			const string levelData = @"
wwwww
wtcp-w
wwwww";

			var creator = new LevelCreator(new TestLevelFormat());
			var level = creator.Create(levelData);

			Assert.AreEqual(12, level.Walls.Count);
			Assert.AreEqual(1, level.Targets.Count);
			Assert.AreEqual(1, level.Crates.Count);

			Assert.AreEqual(3, level.PlayerX);
			Assert.AreEqual(1, level.PlayerY);

			Assert.AreEqual(2, level.Crates.Single().X);
			Assert.AreEqual(1, level.Crates.Single().Y);

			Assert.AreEqual(1, level.Targets.Single().Item1);
			Assert.AreEqual(1, level.Targets.Single().Item2);

			Assert.AreEqual(6, level.Width);
			Assert.AreEqual(3, level.Height);
		}

		[Test]
		public void CustomExtendedFormatTest()
		{
			const string levelData = @"
wwwww
wtCP-w
wwwww";

			var creator = new LevelCreator(new TestExtendedLevelFormat());
			var level = creator.Create(levelData);

			Assert.AreEqual(12, level.Walls.Count);
			Assert.AreEqual(3, level.Targets.Count);
			Assert.AreEqual(1, level.Crates.Count);

			Assert.AreEqual(3, level.PlayerX);
			Assert.AreEqual(1, level.PlayerY);

			Assert.AreEqual(2, level.Crates.Single().X);
			Assert.AreEqual(1, level.Crates.Single().Y);

			Assert.AreEqual(6, level.Width);
			Assert.AreEqual(3, level.Height);
		}

		private class TestLevelFormat : ILevelFormat
		{
			public char Crate { get; } = 'c';
			public char Player { get; } = 'p';
			public char Target { get; } = 't';
			public char Wall { get; } = 'w';
		}

		private class TestExtendedLevelFormat : TestLevelFormat, IExtendedLevelFormat
		{
			public char CrateOverTarget { get; } = 'C';
			public char PlayerOverTarget { get; } = 'P';
		}
	}
}
