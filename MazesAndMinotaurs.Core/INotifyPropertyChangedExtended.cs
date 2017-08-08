using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core
{
	public interface INotifyPropertyChangedExtended<T>
	{
		event PropertyChangedExtendedEventHandler<T> PropertyChanged;
	}

	public delegate void PropertyChangedExtendedEventHandler<T>(object sender, PropertyChangedExtendedEventArgs<T> e);
}
