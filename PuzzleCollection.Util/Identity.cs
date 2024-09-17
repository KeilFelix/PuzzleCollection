using System.Numerics;

namespace PuzzleCollection.Util;

public class FibIdentity
{
    public FibIdentity(Func<int, int , int, BigInteger> leftHand, Func<int, int, int, BigInteger> rightHand)
    {
        LeftHand = leftHand;
        RightHand = rightHand;
    }

    public Func<int, int, int, BigInteger> LeftHand { get; }
    public Func<int, int, int, BigInteger> RightHand { get; }

    public bool IsIdentity(int a, int b, int n)
    {
        return Result(a, b, n).Equality;
    }

    public (bool Equality, BigInteger Left, BigInteger Right) Result(int a, int b, int n)
    {
        var left = LeftHand(a, b, n);
        var right = RightHand(a, b, n);
        return (left == right, left, right);
    }

    public IEnumerable<(bool Equality, BigInteger Left, BigInteger Right)> Results(int a, int b) => Enumerable.Range(2, 100).Select(i => Result(a, b, i));



    /// Collected identities

    public static FibIdentity FibonacciLucasIdentity = new FibIdentity((a, b, n) => Fibonacci.Sequence(a, b)[2*n], (a, b, n) => Fibonacci.Sequence(a, b)[n] * Fibonacci.Sequence(a + 2, b)[n]);
}


public static class  IdentityParts
{
    public static Func<int, int, int, BigInteger> FibonacciLucasLeft = (a, b, n) => Fibonacci.Sequence(a, b)[2 * n];
    public static Func<int, int, int, BigInteger> FibonacciLucasRight = (a, b, n) => Fibonacci.Sequence(a, b)[n] * Fibonacci.Sequence(a + 2, b)[n];
    
}

public static class Constants
{
    public static readonly double Phi = (1 + Math.Sqrt(5)) / 2;
    public static readonly double Psi = (1 - Math.Sqrt(5)) / 2;


}

public static class Operators
{
    public static Func<BigInteger, BigInteger, BigInteger> Add = (a, b) => a + b;
    public static Func<BigInteger, BigInteger, BigInteger> Subtract = (a, b) => a - b;
    public static Func<BigInteger, BigInteger, BigInteger> Multiply = (a, b) => a * b;
    public static Func<BigInteger, BigInteger, BigInteger> Power = (a, b) => BigInteger.Pow(a, (int)b);
    public static Func<BigInteger, BigInteger, BigInteger> Divide = (a, b) => a / b;
    public static Func<BigInteger, BigInteger, BigInteger> Modulo = (a, b) => a % b;
    public static Func<BigInteger, BigInteger, BigInteger> Gcd = (a, b) => BigInteger.GreatestCommonDivisor(a, b);
    public static Func<BigInteger, BigInteger, BigInteger> Lcm = (a, b) => a * b / Gcd(a, b);
    public static Func<BigInteger, BigInteger, BigInteger> Max = (a, b) => BigInteger.Max(a, b);
    public static Func<BigInteger, BigInteger, BigInteger> Min = (a, b) => BigInteger.Min(a, b);
    public static Func<BigInteger, BigInteger> Abs = (a) => BigInteger.Abs(a);
    public static Func<BigInteger, BigInteger> Negate = (a) => -a;


    //public static Func<BigInteger, BigInteger> Sign = (a) => Math.Sign(a);
    //public static Func<BigInteger, BigInteger, BigInteger> Log = (a, b) => (int)Math.Log((double)a, (double)b);
    //public static Func<BigInteger, BigInteger, BigInteger> Log10 = (a, b) => (int)Math.Log10((double)a);
    //public static Func<BigInteger, BigInteger, BigInteger> Log2 = (a, b) => (int)Math.Log2((double)a);
    //public static Func<BigInteger, BigInteger, BigInteger> Sqrt = (a, b) => (int)Math.Sqrt((double)a);


}
