//// Copyright (c) 2011-2020 Roland Pheasant. All rights reserved.
//// Roland Pheasant licenses this file to you under the MIT license.
//// See the LICENSE file in the project root for full license information.

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reactive.Concurrency;
//using System.Reactive.Disposables;
//using System.Reactive.Linq;
//using PuzzleCollection.Util.DynamicData;
//using DynamicData;

//namespace PuzzleCollection.Util.DynamicData;


///// <summary>
///// Filters source on the specified observable property using the specified predicate.
/////
///// The filter will automatically reapply when a property changes.
///// </summary>
///// <typeparam name="TObject">The type of the object.</typeparam>
///// <param name="source">The source.</param>
///// <param name="objectFilterObservable">The filter property selector. When the observable changes the filter will be re-evaluated.</param>
///// <param name="propertyChangedThrottle">The property changed throttle.</param>
///// <param name="scheduler">The scheduler used when throttling.</param>
///// <returns>An observable which emits the change set.</returns>
/////
//public static class DynamicDataObservableOverloadsEx
//{
//    public static IObservable<IChangeSet<TObject>> FilterOnObservable<TObject>(this IObservable<IChangeSet<TObject>> source, Func<TObject, IObservable<bool>> filter, TimeSpan? buffer = null, IScheduler? scheduler = null)
//    {
//        if (source == null) throw new ArgumentNullException(nameof(source));
//        if (filter == null) throw new ArgumentNullException(nameof(filter));

//        return Observable.Create<IChangeSet<TObject>>(
//            observer =>
//            {
//                var locker = new object();

//                var allItems = new List<ObjWithFilterValue<TObject>>();

//                var shared = source.Synchronize(locker).Transform(v => new ObjWithFilterValue<TObject>(v, true)) // we default to true (include all items)
//                    .Clone(allItems) // clone all items so we can look up the index when a change has been made
//                    .Publish();

//                // monitor each item observable and create change, carry the value of the observable property
//                var itemHasChanged = shared.MergeMany(v => filter(v.Obj).Select(prop => new ObjWithFilterValue<TObject>(v.Obj, prop)));

//                // create a change set, either buffered or one item at the time
//                var itemsChanged = buffer is null ?
//                    itemHasChanged.Select(t => new[] { t }) :
//                    itemHasChanged.Buffer(buffer.Value, scheduler ?? Scheduler.Default).Where(list => list.Count > 0);

//                var requiresRefresh = itemsChanged.Synchronize(locker).Select(
//                    items =>
//                    {
//                        // catch all the indices of items which have been refreshed
//                        return IndexOfMany(allItems, items, v => v.Obj, (t, idx) => new Change<ObjWithFilterValue<TObject>>(ListChangeReason.Refresh, t, idx));
//                    }).Select(changes => new ChangeSet<ObjWithFilterValue<TObject>>(changes));

//                // publish refreshes and underlying changes
//                var publisher = shared.Merge(requiresRefresh).Filter(v => v.Filter)
//                    .Transform(v => v.Obj)
//                    .SuppressRefresh() // suppress refreshes from filter, avoids excessive refresh messages for no-op filter updates
//                    .NotEmpty()
//                    .SubscribeSafe(observer);

//                return new CompositeDisposable(publisher, shared.Connect());
//            });
//    }

//    public static IObservable<IChangeSet<TObject>> FilterOnObservable2<TObject>(this IObservable<IChangeSet<TObject>> source, Func<TObject, IObservable<bool>> filter, TimeSpan? buffer = null, IScheduler? scheduler = null)
//    {
//        if (source == null) throw new ArgumentNullException(nameof(source));
//        if (filter == null) throw new ArgumentNullException(nameof(filter));

//        return Observable.Create<IChangeSet<TObject>>(
//            observer =>
//            {
//                var locker = new object();

//                var allItems = new List<ObjWithFilterValue<TObject>>();

//                var shared = source.Synchronize(locker).Transform(v => new ObjWithFilterValue<TObject>(v, true)) // we default to true (include all items)
//                    .Clone(allItems) // clone all items so we can look up the index when a change has been made
//                    .Publish();

//                // monitor each item observable and create change, carry the value of the observable property
//                var itemHasChanged = shared.MergeMany(v => filter(v.Obj).Select(prop => new ObjWithFilterValue<TObject>(v.Obj, prop)));

//                // create a change set, either buffered or one item at the time
//                var itemsChanged = buffer is null ?
//                    itemHasChanged.Select(t => new[] { t }) :
//                    itemHasChanged.Buffer(buffer.Value, scheduler ?? Scheduler.Default).Where(list => list.Count > 0);

//                var requiresRefresh = itemsChanged.Synchronize(locker).Select(
//                    items =>
//                    {
//                        // catch all the indices of items which have been refreshed
//                        return IndexOfMany(allItems, items, v => v.Obj, (t, idx) => new Change<ObjWithFilterValue<TObject>>(ListChangeReason.Refresh, t, idx));
//                    }).Select(changes => new ChangeSet<ObjWithFilterValue<TObject>>(changes));

//                // publish refreshes and underlying changes
//                var publisher = shared.Merge(requiresRefresh).Filter(v => v.Filter)
//                    .Transform(v => v.Obj)
//                    .SuppressRefresh() // suppress refreshes from filter, avoids excessive refresh messages for no-op filter updates
//                    .NotEmpty()
//                    .SubscribeSafe(observer);

//                return new CompositeDisposable(publisher, shared.Connect());
//            });
//    }

//    private static IEnumerable<TResult> IndexOfMany<TObj, TObjectProp, TResult>(IEnumerable<TObj> source, IEnumerable<TObj> itemsToFind, Func<TObj, TObjectProp> objectPropertyFunc, Func<TObj, int, TResult> resultSelector)
//    {
//        if (source is null)
//        {
//            throw new ArgumentNullException(nameof(source));
//        }

//        if (itemsToFind is null)
//        {
//            throw new ArgumentNullException(nameof(itemsToFind));
//        }

//        if (resultSelector is null)
//        {
//            throw new ArgumentNullException(nameof(resultSelector));
//        }

//        var indexed = source.Select((element, index) => new { Element = element, Index = index });
//        return itemsToFind.Join(indexed, objectPropertyFunc, right => objectPropertyFunc(right.Element), (left, right) => resultSelector(left, right.Index));
//    }

//    private readonly struct ObjWithFilterValue<TObject> : IEquatable<ObjWithFilterValue<TObject>>
//    {
//        public readonly TObject Obj;

//        public readonly bool Filter;

//        public ObjWithFilterValue(TObject obj, bool filter)
//        {
//            Obj = obj;
//            Filter = filter;
//        }

//        private static IEqualityComparer<ObjWithFilterValue<TObject>> ObjComparer { get; } = new ObjEqualityComparer();

//        public bool Equals(ObjWithFilterValue<TObject> other)
//        {
//            // default equality does _not_ include Filter value, as that would cause the Filter operator that is used later to fail
//            return ObjComparer.Equals(this, other);
//        }

//        public override bool Equals(object? obj)
//        {
//            return obj is ObjWithFilterValue<TObject> value && Equals(value);
//        }

//        public override int GetHashCode()
//        {
//            return ObjComparer.GetHashCode(this);
//        }

//        private sealed class ObjEqualityComparer : IEqualityComparer<ObjWithFilterValue<TObject>>
//        {
//            public bool Equals(ObjWithFilterValue<TObject> x, ObjWithFilterValue<TObject> y)
//            {
//                return EqualityComparer<TObject>.Default.Equals(x.Obj, y.Obj);
//            }

//            public int GetHashCode(ObjWithFilterValue<TObject> obj)
//            {
//                unchecked
//                {
//                    return (obj.Obj is null ? 0 : EqualityComparer<TObject>.Default.GetHashCode(obj.Obj)) * 397;
//                }
//            }
//        }
//    }
//}


