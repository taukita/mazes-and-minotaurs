using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core.Pseudotime
{
	public sealed class Speed
	{
		private decimal _actionsPerQuantum = 1;
		private decimal? _quantumsPerAction = 1;

		private Speed()
		{
		}

		public static Speed FromApq(decimal apq)
		{
			return new Speed {ActionsPerQuantum = apq};
		}

		public static Speed FromQpa(decimal qpa)
		{
			return new Speed {QuantumsPerAction = qpa};
		}

		public decimal ActionsPerQuantum
		{
			get { return _actionsPerQuantum; }
			set
			{
				if (value < 0m)
					throw new ArgumentException(nameof(value));
				_actionsPerQuantum = value;
				_quantumsPerAction = value == 0m ? null : (decimal?) (1 / value);
			}
		}

		public decimal? QuantumsPerAction
		{
			get { return _quantumsPerAction; }
			set
			{
				if (value <= 0m)
					throw new ArgumentException(nameof(value));
				_quantumsPerAction = value;
				_actionsPerQuantum = 1 / value ?? 0;
			}
		}
	}
}
