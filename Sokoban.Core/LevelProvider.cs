using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Sokoban.Core.Properties;

namespace Sokoban.Core
{
	internal class LevelProvider : ILevelProvider
	{
		private const string TestLevelData = @"
#######
#.-$--#
#-#-#-#
#--@--#
#-#-#-#
#.-$--#
#######";

		private readonly XDocument _data;
		private readonly LevelCreator _levelCreator = new LevelCreator();

		public LevelProvider()
		{
			_data = XDocument.Parse(Encoding.UTF8.GetString(Resources.Original));
			Count = _data.Root?.Element("LevelCollection")?.Elements("Level").Count() ?? 0;
		}

		public int Count { get; }

		public Level GetLevel(int index)
		{
			Level level;
			if (index == -1)
			{
				level = _levelCreator.Create(TestLevelData);
			}
			else
			{
				var levelData = _data.Root?.Element("LevelCollection")?.Elements("Level").ElementAt(index);
				if (levelData == null)
					throw new ArgumentNullException(nameof(levelData));
				level = _levelCreator.Create(levelData.Elements("L").Select(e => e.Value));
			}
			level.Index = index;
			level.ProviderType = GetType();
			return level;
		}
	}
}