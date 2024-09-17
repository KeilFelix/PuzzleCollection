//using System.Collections;
//using System.Reactive.Linq;
//using DynamicData;

//namespace PuzzleCollection.Util.DynamicData
//{
//    public static class DynamicDataExtensions
//    {
//        public static IObservable<IChangeSet<TDestination, TKey>> DraftTransformOnObservable<TDestination, TSource, TKey>(this IObservable<IChangeSet<TSource, TKey>> source, Func<TSource, TDestination> transformFactory)
//        {
//            return source.Transform(transformFactory);
//        }
//        /// <summary>
//        /// Clones the changes  into the specified collection.
//        /// </summary>
//        /// <typeparam name="TObject">The type of the object.</typeparam>
//        /// <typeparam name="TKey">The type of the key.</typeparam>
//        /// <param name="source">The source.</param>
//        /// <param name="target">The target.</param>
//        /// <returns>An observable which emits change sets.</returns>
//        public static IObservable<IChangeSet<TObject, TKey>> Clone<TObject, TKey, TDictKey>(this IObservable<IChangeSet<TObject, TKey>> source, Func<TObject, TDictKey> keySelector, IDictionary<TDictKey, TObject> target)
//            where TKey : notnull
//        {
//            if (source is null)
//            {
//                throw new ArgumentNullException(nameof(source));
//            }

//            if (target is null)
//            {
//                throw new ArgumentNullException(nameof(target));
//            }


//            if (keySelector is null)
//            {
//                throw new ArgumentNullException(nameof(keySelector));
//            }

//            return source.Do(
//                changes =>
//                {
//                    foreach (var item in changes)
//                    {
//                        switch (item.Reason)
//                        {
//                            case ChangeReason.Add:
//                                {
//                                    target.Add(keySelector(item.Current), item.Current);
//                                }

//                                break;

//                            case ChangeReason.Update:
//                                {
//                                    target.Remove(item.Previous.Value);
//                                    target.Add(item.Current);
//                                }

//                                break;

//                            case ChangeReason.Remove:
//                                target.Remove(item.Current);
//                                break;
//                        }
//                    }
//                });
//        }
//    }
//}