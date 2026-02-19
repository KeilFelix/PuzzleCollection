// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using PuzzleCollection.Util;

namespace PuzzleCollection.ProjectEuler;

public class Problem46_GoldbachsOtherConjecture : IPuzzle
{
    public string GetSolution()
    {
        IEnumerable<long> Odds() => NumberSequences.NaturalNumbers().Select(n => n * 2 - 1);
        var odds = Odds().Skip(2).GetEnumerator();
        var goldbachSeries = GoldbachConjectureSeries().Where(n => n % 2 == 1).GetEnumerator();
        var primes = NumberSequences.Primes().GetEnumerator();

        odds.MoveNext();
        goldbachSeries.MoveNext();
        primes.MoveNext();
        var lastGoldback = goldbachSeries.Current;
        var lastPrime = primes.Current;

        while (odds.Current == lastPrime || odds.Current == lastGoldback)
        {
            odds.MoveNext();

            while (odds.Current >= primes.Current)
            {
                lastPrime = primes.Current;
                primes.MoveNext();
            }

            while (odds.Current >= goldbachSeries.Current)
            {
                lastGoldback = goldbachSeries.Current;
                goldbachSeries.MoveNext();
            }
        }

        return $"The first odd composite not in Goldbach Conjecture is {odds.Current}";
    }


    public IEnumerable<long> GoldbachConjectureSeries()
    {
        IEnumerable<long> GoldbachPartialSeries(long prime) => NumberSequences.Squares().Select(s => prime + 2 * s);

        var primer = NumberSequences.Primes().GetEnumerator();
        primer.MoveNext();

        List<IEnumerator<long>> goldbackPartialEnumerators = new List<IEnumerator<long>>();
        var firstPartialSeries = GoldbachPartialSeries(primer.Current).GetEnumerator();
        firstPartialSeries.MoveNext();
        primer.MoveNext();
        goldbackPartialEnumerators.Add(firstPartialSeries);
        long lastYielded = 0;
        while (true)
        {
            while (goldbackPartialEnumerators.Any(e => e.Current > primer.Current))
            {
                var partialSeries = GoldbachPartialSeries(primer.Current).GetEnumerator();
                partialSeries.MoveNext();
                primer.MoveNext();
                goldbackPartialEnumerators.Add(partialSeries);
            }

            var smallestEnumerator = goldbackPartialEnumerators.MinBy(e => e.Current);

            if (smallestEnumerator!.Current != lastYielded)
            {
                yield return lastYielded = smallestEnumerator.Current;
            }

            smallestEnumerator.MoveNext();
        }
    }
}

