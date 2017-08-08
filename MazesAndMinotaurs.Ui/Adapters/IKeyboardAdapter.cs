using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Ui.Adapters
{
	public interface IKeyboardAdapter<TKey>
	{
		bool isUp(TKey key);
		bool isLeft(TKey key);
		bool isDown(TKey key);
		bool isRight(TKey key);
		bool isEnter(TKey key);
	}
}
