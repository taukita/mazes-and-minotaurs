using System.ComponentModel;

namespace MazesAndMinotaurs.Core
{
	public class PropertyChangedExtendedEventArgs<T> : PropertyChangedEventArgs
	{
		public virtual T OldValue { get; }
		public virtual T NewValue { get; }
		public bool Handled { get; set; }

		public PropertyChangedExtendedEventArgs(string propertyName, T oldValue, T newValue)
			: base(propertyName)
		{
			OldValue = oldValue;
			NewValue = newValue;
		}
	}
}
