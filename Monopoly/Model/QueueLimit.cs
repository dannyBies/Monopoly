using System.Collections.ObjectModel;

namespace Monopoly.Model {
    /// <summary>
    /// A Queue that has a fixed size and can easily be used for databinding
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueueLimit<T> : ObservableCollection<T> {
        public int Limit { get; set; }

        public QueueLimit(int limit) {
            Limit = limit;
        }

        public void Enqueue(T item) {
            while (Count >= Limit) {
                RemoveAt(Count - 1);
            }
            Insert(0, item);
        }
    }
}
