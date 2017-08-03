using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core
{
	public static class StringExtensions
	{
		public static string Fit(this string s, int count, char ellipsis = '…') 
		{
			if (s.Length <= count)
			{
				return s;
			}
			return s.Substring(0, count - 1) + ellipsis;
		}

		public static IEnumerable<string> Split(this string s, int chunkLength)
		{
			var sb = new StringBuilder(chunkLength);
			for (var i = 0; i < s.Length; i++)
			{
				sb.Append(s[i]);
				if (sb.Length == chunkLength)
				{
					yield return sb.ToString();
					sb = new StringBuilder(chunkLength);
				}
			}
			if (sb.Length > 0)
			{
				yield return sb.ToString();
			}
		}
	}
}
