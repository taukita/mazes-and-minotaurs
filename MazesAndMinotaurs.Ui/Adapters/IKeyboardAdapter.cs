using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Ui.Adapters
{
	public interface IKeyboardAdapter<in TInput>
	{
		bool IsKeyboardInput(TInput input);

		bool IsUp(TInput input);
		bool IsLeft(TInput input);
		bool IsDown(TInput input);
		bool IsRight(TInput input);
		bool IsEnter(TInput input);
		bool IsTab(TInput input);
		bool IsEscape(TInput input);
		bool IsBackspace(TInput input);
	}
}
