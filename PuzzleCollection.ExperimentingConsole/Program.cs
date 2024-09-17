using PuzzleCollection.Util;
using System.Numerics;


void FindFibLubIdentLike()
{
    var seeds = Enumerable.Range(0, 7)
    .CombinationsWithRepeat(7)
    .Select(x => (
        a1: 0,
        b1: 1,
        n1: x.ElementAtOrDefault(2),
        a2: x.ElementAtOrDefault(3),
        b2: x.ElementAtOrDefault(4),
        n2: x.ElementAtOrDefault(5),
        a3: x.ElementAtOrDefault(6),
        b3: x.ElementAtOrDefault(0),
        n3: x.ElementAtOrDefault(1)
        ))
    .Where(x => !(x.a1 == 0 && x.b1 == 0) && !(x.a2 == 0 && x.b2 == 0) && !(x.a3 == 0 && x.b3 == 0)) // Filter out invalid 0,0 seeds
    //.Where(x => (x.n1 == x.n2 && x.n1 == x.n3 && x.a1 == 0  && x.b1 == 1 && x.a2 == 2 && x.b2 == 1 && x.a3 == 0 && x.b3 == 1) // Just pick the lucas formula
    .Where(x => !(x.n1 == x.n2 && x.n1 == x.n3) || x.n1 == 0) // Just pick the lucas formula
;
    var tests = seeds.Select(seed =>
    (Seed: seed,
    Left: new Func<int, BigInteger>(n => Fibonacci.Sequence(seed.a1, seed.b1)[(n + seed.n1) * 2]),
    Right: new Func<int, BigInteger>(n => Fibonacci.Sequence(seed.a2, seed.b2)[(n + seed.n2)] * Fibonacci.Sequence(seed.a3, seed.b3)[(n + seed.n3)])));
    var results = tests.Select(test =>
    {
        var result = Enumerable.Range(3, 50)
        .Select(n => (Left: test.Left(n), Right: test.Right(n)))
            .Select(n => (n.Left, n.Right, Result: n.Left == n.Right)).ToList();

        return new { Result = result, test.Seed };
    });



    var interestingMiddleResult = results
        .Where(x => x.Result.Where(r => r.Result).Count() > 22)
        .Memoize();

    var interestingResult = interestingMiddleResult
        .Where(x => interestingMiddleResult.TakeWhile(y => y.Seed != x.Seed).Any(y => y.Seed.a2 == x.Seed.a3 && y.Seed.b2 == x.Seed.b3 && y.Seed.n2 == x.Seed.n3));

    foreach (var result in interestingResult)
    {
        Console.WriteLine($"Seed: {result.Seed}, Result: {result.Result.Where(r => r.Result).Count()}");
    }
    Console.WriteLine("Interesting Result Count: " + interestingResult.Count());
    Console.ReadKey();
}

void FindSimple()
{
    var seeds = Enumerable.Range(0, 9)
    .CombinationsWithRepeat(7)
    .Select(x => (
        a1: 0,
        b1: 1,
        n1: x.ElementAtOrDefault(2),
        a2: x.ElementAtOrDefault(3),
        b2: x.ElementAtOrDefault(4),
        n2: x.ElementAtOrDefault(5),
        a3: x.ElementAtOrDefault(6),
        b3: x.ElementAtOrDefault(0),
        n3: x.ElementAtOrDefault(1)
        ))
    .Where(x => !(x.a1 == 0 && x.b1 == 0) && !(x.a2 == 0 && x.b2 == 0) && !(x.a3 == 0 && x.b3 == 0)) // Filter out invalid 0,0 seeds
    //.Where(x => (x.n1 == x.n2 && x.n1 == x.n3 && x.a1 == 0  && x.b1 == 1 && x.a2 == 2 && x.b2 == 1 && x.a3 == 0 && x.b3 == 1) // Just pick the lucas formula
    .Where(x => !(x.n1 == x.n2 && x.n1 == x.n3) || x.n1 == 0) // Just pick the lucas formula
;
    var tests = seeds.Select(seed =>
    (Seed: seed,
    Left: new Func<int, BigInteger>(n => Fibonacci.Sequence(seed.a1, seed.b1)[(n + seed.n1) * 2]),
    Right: new Func<int, BigInteger>(n => Fibonacci.Sequence(seed.a2, seed.b2)[(n + seed.n2)] * Fibonacci.Sequence(seed.a3, seed.b3)[(n + seed.n3)])));
    var results = tests.Select(test =>
    {
        var result = Enumerable.Range(3, 50)
        .Select(n => (Left: test.Left(n), Right: test.Right(n)))
            .Select(n => (n.Left, n.Right, Result: n.Left == n.Right)).ToList();

        return new { Result = result, test.Seed };
    });



    var interestingMiddleResult = results
        .Where(x => x.Result.Where(r => r.Result).Count() > 22)
        .Memoize();

    var interestingResult = interestingMiddleResult
        .Where(x => interestingMiddleResult.TakeWhile(y => y.Seed != x.Seed).Any(y => y.Seed.a2 == x.Seed.a3 && y.Seed.b2 == x.Seed.b3 && y.Seed.n2 == x.Seed.n3));

    foreach (var result in interestingResult)
    {
        Console.WriteLine($"Seed: {result.Seed}, Result: {result.Result.Where(r => r.Result).Count()}");
    }
    Console.WriteLine("Interesting Result Count: " + interestingResult.Count());
    Console.ReadKey();
}
//BigInteger CallMemoizedFibonacciOverLucasParallel(long n)
//{
//    var task = FibonacciAlgs.MemoizedFibonacciOverLucasParallel(n);
//    return task.Result;
//}

//List<Func<long, BigInteger>> fibonacciAlgorithms = new List<Func<long, BigInteger>>
//{
//    //{ FibonacciAlgs.MemoizedFibonacciCogitoErgo },
//    //{ FibonacciAlgs.MemoizedFibonacciCogitoErgo },
//    //{ FibonacciAlgs.MemoizedFibonacciCogitoErgo },
//    //{ FibonacciAlgs.MemoizedFibonacciCogitoErgo },
//    //{ FibonacciAlgs.MemoizedFibonacciCogitoErgo },
//    //{ FibonacciAlgs.MemoizedFibonacciCogitoErgo },
//    { FibonacciAlgs.MemoizedFibonacciOverLucas },
//    //{ FibonacciAlgs.MemoizedFibonacciOverLucas },
//    //{ FibonacciAlgs.MemoizedFibonacciOverLucas },
//    //{ FibonacciAlgs.MemoizedFibonacciOverLucas },
//    //{ FibonacciAlgs.MemoizedFibonacciOverLucas },
//    //{ FibonacciAlgs.MemoizedFibonacciOverLucas },
//    //{ CallMemoizedFibonacciOverLucasParallel },
//    //{ CallMemoizedFibonacciOverLucasParallel },
//    //{ CallMemoizedFibonacciOverLucasParallel },
//    //{ CallMemoizedFibonacciOverLucasParallel },
//    //{ CallMemoizedFibonacciOverLucasParallel },
//    //{ CallMemoizedFibonacciOverLucasParallel },
//};

//fibonacciAlgorithms.Select(alg =>
//{
//    Stopwatch sw = new Stopwatch();
//    FibonacciAlgs.InitCaches();

//    //var test = (long) BigInteger.Pow(2, 25) + 23;
//    var test = 2067;
//    sw.Start();
//    //var result = alg(test);
//    var result = alg(test);
//    sw.Stop();
//    return new { Alg = alg.Method.Name, Elapsed = sw.ElapsedMilliseconds, Result = result};
//}).ForEach(Console.WriteLine);


//Console.ReadKey();