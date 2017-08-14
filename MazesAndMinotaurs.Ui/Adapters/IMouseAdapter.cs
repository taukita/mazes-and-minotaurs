using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Ui.Adapters
{
	public interface IMouseAdapter<in TInput>
	{
		bool IsMouseInput(TInput input);

		int GetX(TInput input);
		int GetY(TInput input);
	}
}

