using System;
using System.Collections.Generic;
using System.Linq;

namespace MazesAndMinotaurs.Core.Pathfinding
{
	public class AStar<T> where T : class
	{
		private readonly IDistanceProvider<T> _distanceBetweenProvider;
		private readonly IDistanceProvider<T> _heuristicCostEstimateProvider;
		private readonly INeighborsProvider<T> _neighborsProvider;

		public AStar(INeighborsProvider<T> neighborsProvider, IDistanceProvider<T> distanceBetweenProvider,
			IDistanceProvider<T> heuristicCostEstimateProvider)
		{
			_heuristicCostEstimateProvider = heuristicCostEstimateProvider;
			_neighborsProvider = neighborsProvider;
			_distanceBetweenProvider = distanceBetweenProvider;
		}

		public IEnumerable<T> Search(T start, T goal)
		{
			var closedSet = new HashSet<T>();
			var openSet = new HashSet<T> {start};

			var cameFrom = new Dictionary<T, T>();

			var gScore = new MapWithDefaultValueOfInfinity {[start] = 0};
			var fScore = new MapWithDefaultValueOfInfinity();

			while (openSet.Any())
			{
				var current = NodeHavingTheLowestValue(openSet, fScore);
				if (current.Equals(goal))
					return ReconstructPath(current, cameFrom);

				openSet.Remove(current);
				closedSet.Add(current);

				foreach (var neighbor in _neighborsProvider.Get(current))
				{
					if (closedSet.Contains(neighbor))
						continue;

					if (!openSet.Contains(neighbor))
						openSet.Add(neighbor);

					var tentativeGScore = gScore[current] + _distanceBetweenProvider.Get(current, neighbor);
					if (tentativeGScore > gScore[neighbor])
						continue;

					cameFrom[neighbor] = current;
					gScore[neighbor] = tentativeGScore;
					fScore[neighbor] = gScore[neighbor] + _heuristicCostEstimateProvider.Get(neighbor, goal);
				}
			}

			return null;
		}

		private static T NodeHavingTheLowestValue(IEnumerable<T> nodes, MapWithDefaultValueOfInfinity values)
		{
			T result = null;
			foreach (var node in nodes)
				if (result == null || values[node] < values[result])
					result = node;
			if (result == null)
				throw new Exception("Something goes wrong.");
			return result;
		}

		private static IEnumerable<T> ReconstructPath(T current, IReadOnlyDictionary<T, T> cameFrom)
		{
			var result = new List<T> {current};
			while (cameFrom.ContainsKey(current))
			{
				current = cameFrom[current];
				result.Add(current);
			}
			return result;
		}

		private class MapWithDefaultValueOfInfinity
		{
			private readonly Dictionary<T, double> _map = new Dictionary<T, double>();

			public double this[T node]
			{
				get { return _map.ContainsKey(node) ? _map[node] : double.PositiveInfinity; }
				set
				{
					if (double.IsPositiveInfinity(value))
						_map.Remove(node);
					else
						_map[node] = value;
				}
			}
		}
	}
}