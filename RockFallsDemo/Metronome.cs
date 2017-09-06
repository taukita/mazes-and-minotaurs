using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockFallsDemo
{
	internal class Metronome
	{
		private int _accumutalor;
		private readonly int _threshold;

		public Metronome(int bpm)
		{
			Bpm = bpm;
			_threshold = 1000000 * 60 / Bpm;
		}

		public int Bpm { get; }

		public event Action Beat;

		public void Update(int microseconds)
		{
			_accumutalor += microseconds;
			while (_accumutalor > _threshold)
			{
				_accumutalor -= _threshold;
				Beat?.Invoke();
			}
		}
	}
}
