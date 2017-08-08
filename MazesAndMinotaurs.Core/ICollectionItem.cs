using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core
{
	public interface ICollectionItem<T>
	{
		ObservableCollection<T> Collection
		{
			get;
			set;
		}
	}
}
