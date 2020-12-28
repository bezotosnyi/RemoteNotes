using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RemoteNotes.UI.Utility
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
        {
            return new ObservableCollection<T>(col);
        }
    }
}