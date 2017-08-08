using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core
{
	public sealed class SmartCollection<T> : ObservableCollection<T> where T : ICollectionItem<T>
	{
		protected override void ClearItems()
		{
			List<T> removed = new List<T>(this);
			base.ClearItems();
			foreach(var item in removed)
			{
				item.Collection = null;
			}
		}

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			base.OnCollectionChanged(e);
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (var item in e.NewItems.Cast<ICollectionItem<T>>())
					{
						item.Collection = this;
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (var item in e.OldItems.Cast<ICollectionItem<T>>())
					{
						item.Collection = null;
					}
					break;
			}
		}
	}
}
