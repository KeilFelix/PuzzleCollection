//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reactive.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DynamicData;
//using DynamicData.Kernel;

//namespace PuzzleCollection.Util.DynamicData
//{
//    public class SourceDict<TObject, TKey> : ISourceCache<TObject, TKey>
//    {
//        private SourceCache<KeyValuePair<TKey, TObject>, TKey> _cache = new SourceCache<KeyValuePair<TKey, TObject>, TKey>(kvp => kvp.Key);
//        public Func<TObject, TKey> KeySelector => obj => _cache.Items.Single(kvp => EqualityComparer<TObject>.Default.Equals(obj, kvp.Value)).Key;

//        public int Count => _cache.Count;

//        public IEnumerable<TObject> Items => _cache.Items.Select(kvp => kvp.Value);

//        public IEnumerable<TKey> Keys => _cache.Items.Select(kvp => kvp.Key);

//        public IEnumerable<KeyValuePair<TKey, TObject>> KeyValues => _cache.Items;

//        public IObservable<int> CountChanged => _cache.CountChanged;

//        public IObservable<IChangeSet<TObject, TKey>> Connect(Func<TObject, bool>? predicate = null, bool suppressEmptyChangeSets = true)
//        {
//            throw new NotImplementedException();
//        }

//        public void Dispose()
//        {
//            throw new NotImplementedException();
//        }

//        public void Edit(Action<ISourceUpdater<TObject, TKey>> updateAction)
//        {
//            throw new NotImplementedException();
//        }

//        public IObservable<IChangeSet<TObject, TKey>> Preview(Func<TObject, bool>? predicate = null)
//        {
//            throw new NotImplementedException();
//        }

//        public IObservable<Change<TObject, TKey>> Watch(TKey key)
//        {
//            return _cache.Watch(key).Select(change => new Change<TObject, TKey>());
//        }

//        Optional<TObject> IObservableCache<TObject, TKey>.Lookup(TKey key)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
