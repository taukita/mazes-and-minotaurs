using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public interface IFocusAwareObject<T>
	{
		T Focus(bool focus);
	}
}
