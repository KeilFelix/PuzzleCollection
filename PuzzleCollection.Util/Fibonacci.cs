using System.Collections;
using System.Collections.Concurrent;
using System.Numerics;

namespace PuzzleCollection.Util;

public class Fibonacci : IEnumerable<BigInteger>
{
    private Fibonacci(BigInteger a, BigInteger b)
    {
        Memoize(a);
        Memoize(b);
    }

    private readonly List<BigInteger> _numbers = new();
    private readonly Dictionary<BigInteger, int> _lookup = new();
    
    private void Memoize(BigInteger value)
    {
        _numbers.Add(value);
        _lookup[value] = _numbers.Count;
    }

    public IReadOnlyDictionary<BigInteger, int> Lookup => _lookup;


    public BigInteger this[int n]
    {
        get
        { 
            if(n >= _numbers.Count)
            {
                Generate(_numbers[_numbers.Count-2], _numbers[_numbers.Count - 1], n - _numbers.Count + 1)
                    .ForEach(value => Memoize(value));
            }
            return _numbers[n];
        }
    }
    public BigInteger A => _numbers[0];
    public BigInteger B => _numbers[1];


    private static IEnumerable<BigInteger> Generate(BigInteger a, BigInteger b, int n)
    {
        for (int i = 0; i < n; i++)
        {
            BigInteger temp = a;
            a = b;
            b = temp + b;
            yield return b;

        }
    }

    private IEnumerable<BigInteger> Iterator()
    {
        int n = 0;
        while(true)
        {
            yield return this[n];
            n++;
        }
    }

    public IEnumerator<BigInteger> GetEnumerator() => Iterator().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Iterator().GetEnumerator();

    private static ConcurrentDictionary<(BigInteger A, BigInteger B), Fibonacci> sequences = new();
    public static Fibonacci Sequence(BigInteger a, BigInteger b) => sequences.GetOrAdd((a, b), key => new Fibonacci(key.A, key.B));
    public static Fibonacci Sequence() => sequences.GetOrAdd((0, 1), key => new Fibonacci(key.A, key.B));
}