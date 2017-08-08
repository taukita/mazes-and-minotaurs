using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Ui.Adapters
{
	public interface IKeyboardAdapter<in TKey>
	{
		bool IsUp(TKey key);
		bool IsLeft(TKey key);
		bool IsDown(TKey key);
		bool IsRight(TKey key);
		bool IsEnter(TKey key);
		bool IsTab(TKey key);
	}
}
