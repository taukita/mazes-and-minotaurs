using System;

namespace MazesAndMinotaurs.Core.Pseudotime
{
	public sealed class TimeAwareObject
	{
		private readonly Speed _speed;
		private decimal _quantums;

		public TimeAwareObject(Speed speed)
		{
			_speed = speed;
		}

		public event Action Action;

		public void Quantum()
		{
			if (!_speed.QuantumsPerAction.HasValue)
			{
				_quantums = 0;
				return;
			}

			_quantums++;
			while (_quantums >= _speed.QuantumsPerAction.Value)
			{
				Action?.Invoke();
				_quantums -= _speed.QuantumsPerAction.Value;
			}
		}
	}
}