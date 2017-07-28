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
	}
}
