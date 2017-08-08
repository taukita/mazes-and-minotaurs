using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core
{
	public static class CoreExtensions
	{
		public static IEnumerable<TGlyph> Fit<TGlyph>(this IEnumerable<TGlyph> s, int count, TGlyph ellipsis)
		{
			if (s.Count() <= count)
			{
				return s;
			}
			return s.Take(count - 1).Concat(new[] { ellipsis });
		}

		public static IEnumerable<IEnumerable<TGlyph>> Split<TGlyph>(this IEnumerable<TGlyph> s, int chunkLength)
		{
			var glyphs = new List<TGlyph>(chunkLength);
			foreach (var glyph in s)
			{
				glyphs.Add(glyph);
				if (glyphs.Count == chunkLength)
				{
					yield return glyphs;
					glyphs = new List<TGlyph>(chunkLength);
				}
			}
			if (glyphs.Count > 0)
			{
				yield return glyphs;
			}
		}

		public static IEnumerable<IEnumerable<TGlyph>> Split<TGlyph>(this IEnumerable<TGlyph> source, ICollection<TGlyph> delimiter)
		{
			var window = new Queue<TGlyph>();
			var buffer = new List<TGlyph>();
			var empty = true;
			foreach (var element in source)
			{
				empty = false;
				buffer.Add(element);
				window.Enqueue(element);
				if (window.Count > delimiter.Count)
				{
					window.Dequeue();
				}
				if (window.SequenceEqual(delimiter))
				{
					var nElements = buffer.Count - window.Count;
					if (nElements > 0)
					{
						yield return buffer.Take(nElements).ToArray();
					}
					window.Clear();
					buffer.Clear();
				}
			}
			if (!empty)
			{
				yield return buffer;
			}
		}
	}
}
