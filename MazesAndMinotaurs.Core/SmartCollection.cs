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
	public class SmartCollection<TItem, TOwner> : ObservableCollection<TItem> where TItem : ICollectionItem<TItem>
	{
		public SmartCollection(TOwner owner)
		{
			Owner = owner;
		}

		public TOwner Owner { get; }

		protected override void ClearItems()
		{
			List<TItem> removed = new List<TItem>(this);
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
					foreach (var item in e.NewItems.Cast<ICollectionItem<TItem>>())
					{
						item.Collection = this;
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (var item in e.OldItems.Cast<ICollectionItem<TItem>>())
					{
						item.Collection = null;
					}
					break;
			}
		}
	}
}
