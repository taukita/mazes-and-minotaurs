using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core.Pathfinding
{
	public sealed class AStarBuilder<T> where T: class
	{
		private IDistanceProvider<T> _distance = new DistanceProvider((f, s) => 1);
		private IDistanceProvider<T> _heuristic = new DistanceProvider((f, s) => 0);
		private INeighborsProvider<T> _neighbors = new NeighborsProvider(c => new T[0]);

		public AStarBuilder<T> Distance(IDistanceProvider<T> provider)
		{
			_distance = provider;
			return this;
		}

		public AStarBuilder<T> Distance(Func<T, T, double> func)
		{
			_distance = new DistanceProvider(func);
			return this;
		}

		public AStarBuilder<T> Heuristic(IDistanceProvider<T> provider)
		{
			_heuristic = provider;
			return this;
		}

		public AStarBuilder<T> Heuristic(Func<T, T, double> func)
		{
			_heuristic = new DistanceProvider(func);
			return this;
		}

		public AStarBuilder<T> Neighbors(INeighborsProvider<T> provider)
		{
			_neighbors = provider;
			return this;
		}

		public AStarBuilder<T> Neighbors(Func<T, IEnumerable<T>> func)
		{
			_neighbors = new NeighborsProvider(func);
			return this;
		}

		public AStar<T> Build()
		{
			return new AStar<T>(_neighbors, _distance, _heuristic);
		}

		private class DistanceProvider: IDistanceProvider<T>
		{
			private readonly Func<T, T, double> _func;

			public DistanceProvider(Func<T, T, double> func)
			{
				_func = func;
			}

			public double Get(T first, T second)
			{
				return _func(first, second);
			}
		}

		private class NeighborsProvider : INeighborsProvider<T>
		{
			private readonly Func<T, IEnumerable<T>> _func;

			public NeighborsProvider(Func<T, IEnumerable<T>> func)
			{
				_func = func;
			}

			public IEnumerable<T> Get(T current)
			{
				return _func(current);
			}
		}
	}
}
