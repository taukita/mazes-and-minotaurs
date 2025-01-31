﻿using System;
using System.Collections.Generic;
using System.Linq;
using MazesAndMinotaurs.Ui;
using SFML.Graphics;
using Sokoban.Core;

namespace Sokoban.SfmlTarget
{
	internal class SfmlGlyphProvider : IGlyphProvider<Glyph>
	{
		public SfmlGlyphProvider()
		{
			CrateGlyph = new Glyph(15, 15);
			TargetGlyph = new Glyph(9, 15);
			WallGlyph = new Glyph(8, 0);
			PlayerGlyph = new Glyph(2, 0);
			EllipsisGlyph = new Glyph(14, 7);
			SelectionGlyph = new ColorAwareGlyph(14, 3, Color.White, Color.Transparent);
			Delimiter = new[] {new Glyph(-1, -1)};
			MainMenuBorderTheme = new BorderTheme<Glyph>(
				new Glyph(9, 12), new Glyph(13, 12), new Glyph(11, 11), new Glyph(10, 11),
				new Glyph(12, 11), new Glyph(13, 12), new Glyph(8, 12), new Glyph(10, 11));
		}

		public Glyph CrateGlyph { get; }
		public Glyph TargetGlyph { get; }
		public Glyph WallGlyph { get; }
		public Glyph PlayerGlyph { get; }
		public Glyph EllipsisGlyph { get; }
		public Glyph SelectionGlyph { get; }
		public ICollection<Glyph> Delimiter { get; }
		public BorderTheme<Glyph> MainMenuBorderTheme { get; }

		public IEnumerable<Glyph> FromString(string @string)
		{
			return @string.Select(@char => new Glyph(GetX(@char), GetY(@char)));
		}

		private static int GetX(char @char)
		{
			if (@char == ' ')
				return 0;
			if (@char == ',')
				return 12;
			if (@char == '!')
				return 1;
			if (@char >= 'A' && @char <= 'O')
				return 1 + @char - 'A';
			if (@char >= 'P' && @char <= 'Z')
				return @char - 'P';
			if (@char >= 'a' && @char <= 'o')
				return 1 + @char - 'a';
			if (@char >= 'p' && @char <= 'z')
				return @char - 'p';
			if (@char >= '0' && @char <= '9')
				return @char - '0';
			throw new ArgumentOutOfRangeException(nameof(@char));
		}

		private static int GetY(char @char)
		{
			if (@char == ' ')
				return 0;
			if (@char == '!' || @char == ',')
				return 2;
			if (@char >= 'A' && @char <= 'O')
				return 4;
			if (@char >= 'P' && @char <= 'Z')
				return 5;
			if (@char >= 'a' && @char <= 'o')
				return 6;
			if (@char >= 'p' && @char <= 'z')
				return 7;
			if (@char >= '0' && @char <= '9')
				return 3;
			throw new ArgumentOutOfRangeException(nameof(@char));
		}
	}
}