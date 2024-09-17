//// Copyright (c) 2011-2020 Roland Pheasant. All rights reserved.
//// Roland Pheasant licenses this file to you under the MIT license.
//// See the LICENSE file in the project root for full license information.

//using System;
//using System.Reactive.Concurrency;
//using System.Reactive.Disposables;
//using System.Reactive.Linq;

//using DynamicData.Kernel;
//using DynamicData.List.Internal;

//namespace DynamicData.Cache.Internal;

//internal sealed class TransformOnObservable<TDestination, TSource, TKey>
//    where TKey : notnull
//{
//    private readonly Action<Error<TSource, TKey>>? _exceptionCallback;

//    private readonly IObservable<IChangeSet<TSource, TKey>> _source;

//    private readonly Func<TSource, Optional<TSource>, TKey, IObservable<TDestination>> _transformFactory;

//    private readonly bool _transformOnRefresh;
//    private readonly TimeSpan? _buffer;
//    private readonly IScheduler _scheduler;

//    public TransformOnObservable(
//        IObservable<IChangeSet<TSource, TKey>> source, 
//        Func<TSource, Optional<TSource>, TKey, IObservable<TDestination>> transformFactory,
//        TimeSpan? buffer = null, 
//        IScheduler? scheduler = null,
//        Action<Error<TSource, TKey>>? exceptionCallback = null, 
//        bool transformOnRefresh = false)
//    {
//        _source = source;
//        _exceptionCallback = exceptionCallback;
//        _transformOnRefresh = transformOnRefresh;
//        _transformFactory = transformFactory;
//        _buffer = buffer;
//        _scheduler = scheduler;
//    }

//    public IObservable<IChangeSet<TDestination, TKey>> Run() => Observable.Defer(RunImplVariant1);

//    //private IObservable<IChangeSet<TDestination, TKey>> RunImplVariant1()
//    //{
//    //    var toReturn = Observable.Create<IChangeSet<TDestination, TKey>>(
//    //        observer =>
//    //        {
//    //            var locker = new object();

//    //            var srcWithObservableShared = new Transform<(TSource Source, IObservable<TDestination> Destination), TSource, TKey>(_source, (a,b,c) => (a, _transformFactory(a,b,c)), _exceptionCallback, _transformOnRefresh).Run().Publish();

//    //            // monitor each item observable and create change, carry the value of the observable property
//    //            var destinations = srcWithObservableShared.MergeMany(t => t.Destination.Select(dest => (Source: t.Source, Destination: dest)));

//    //            // create a change set, either buffered or one item at the time
//    //            var destinationsBuffered = _buffer is null
//    //                ? destinations.Select(t => new[] { t })
//    //                : destinations.Buffer(_buffer.Value, _scheduler ?? Scheduler.Default).Where(list => list.Count > 0);

//    //            _source.Tran

//    //            //TODO: Some scan mechanism must be used here, like in original Transform to build the changeset where only Destinations are included
//    //            return new CompositeDisposable();

//    //        });

//    //    return toReturn;
//    //}

//    //private IObservable<IChangeSet<TDestination, TKey>> RunImplVariant2()
//    //{
//    //    var toReturn = Observable.Create<IChangeSet<TDestination, TKey>>(
//    //        observer =>
//    //        {
//    //            var locker = new object();

//    //            var srcWithObservableShared = _source.Synchronize(locker).Transform(v => new FilterOnObservable<>.ObjWithFilterValue(v, true))

//    //            // monitor each item observable and create change, carry the value of the observable property
//    //            var destinations = srcWithObservableShared.Sw(t => t.Destination.Select(dest => (Source: t.Source, Destination: dest)));

//    //            // create a change set, either buffered or one item at the time
//    //            var destinationsBuffered = _buffer is null
//    //                ? destinations.Select(t => new[] { t })
//    //                : destinations.Buffer(_buffer.Value, _scheduler ?? Scheduler.Default).Where(list => list.Count > 0);


//    //            var requiresRefresh = destinationsBuffered.Synchronize(locker).Select(
//    //                items =>
//    //                {
//    //                    // catch all the indices of items which have been refreshed
//    //                    return IndexOfMany(allItems, items, v => v.Obj, (t, idx) => new Change<ObjWithFilterValue>(ListChangeReason.Refresh, t, idx));
//    //                }).Select(changes => new ChangeSet<ObjWithFilterValue>(changes));

//    //            // publish refreshes and underlying changes
//    //            var publisher = shared.Merge(requiresRefresh).Filter(v => v.Filter)
//    //                .Transform(v => v.Obj)
//    //                .SuppressRefresh() // suppress refreshes from filter, avoids excessive refresh messages for no-op filter updates
//    //                .NotEmpty()
//    //                .SubscribeSafe(observer);

//    //            return new CompositeDisposable(publisher, shared.Connect());

//    //        });

//    //    return toReturn;
//    //}

//    private IObservable<IChangeSet<TDestination, TKey>> RunImpl()
//    {
//        var toReturn = Observable.Create<IChangeSet<TDestination, TKey>>(
//            observer =>
//            {
//                var locker = new object();

//                var srcWithObservableShared = _source.Synchronize(locker).Transform(v => new FilterOnObservable<>.ObjWithFilterValue(v, true))

//                // monitor each item observable and create change, carry the value of the observable property
//                var destinations = srcWithObservableShared.Sw(t => t.Destination.Select(dest => (Source: t.Source, Destination: dest)));

//                // create a change set, either buffered or one item at the time
//                var destinationsBuffered = _buffer is null
//                    ? destinations.Select(t => new[] { t })
//                    : destinations.Buffer(_buffer.Value, _scheduler ?? Scheduler.Default).Where(list => list.Count > 0);


//                var requiresRefresh = destinationsBuffered.Synchronize(locker).Select(
//                    items =>
//                    {
//                        // catch all the indices of items which have been refreshed
//                        return IndexOfMany(allItems, items, v => v.Obj, (t, idx) => new Change<ObjWithFilterValue>(ListChangeReason.Refresh, t, idx));
//                    }).Select(changes => new ChangeSet<ObjWithFilterValue>(changes));

//                // publish refreshes and underlying changes
//                var publisher = shared.Merge(requiresRefresh).Filter(v => v.Filter)
//                    .Transform(v => v.Obj)
//                    .SuppressRefresh() // suppress refreshes from filter, avoids excessive refresh messages for no-op filter updates
//                    .NotEmpty()
//                    .SubscribeSafe(observer);

//                return new CompositeDisposable(publisher, shared.Connect());

//            });

//        return toReturn;
//    }
//}
