using System.Collections.Generic;

namespace System.Collections.ObjectModel {
    public static class IEnumerableExtension {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection) {
            ObservableCollection<T> vObservableCollection = new ObservableCollection<T>();
            foreach (var item in collection) {
                vObservableCollection.Add(item);
            }
            return vObservableCollection;
        }
    }
}
