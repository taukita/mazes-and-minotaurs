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
	}
}
