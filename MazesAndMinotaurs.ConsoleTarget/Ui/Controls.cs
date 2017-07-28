using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class Controls : ObservableCollection<Control>
	{
		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			base.OnCollectionChanged(e);
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (var item in e.NewItems.Cast<Control>())
					{
						item.OnFocus += OnFocus;
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (var item in e.OldItems.Cast<Control>())
					{
						// ReSharper disable once DelegateSubtraction
						item.OnFocus -= OnFocus;
					}
					break;
				case NotifyCollectionChangedAction.Replace:
					throw new NotSupportedException();
				case NotifyCollectionChangedAction.Move:
					throw new NotSupportedException();
				case NotifyCollectionChangedAction.Reset:
					throw new NotSupportedException();
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void OnFocus(Control control)
		{
			foreach (var item in this.Where(i => i != control))
			{
				item.IsFocused = false;
			}
		}
	}
}
