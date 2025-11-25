using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleCollection.AdventOfCode.Year2022.Day11_MonkeyInTheMiddle
{
    public class Item
    {
        public BigInteger WorryLevel { get; set; }
    }
    public class Monkey
    {
        private readonly Func<Monkey> _getPositiveMonkey;
        private readonly Func<Monkey> _getNegativeMonkey;

        public Monkey(Func<Monkey> getPositiveMonkey, Func<Monkey> getNegativeMonkey)
        {
            _getPositiveMonkey = getPositiveMonkey;
            _getNegativeMonkey = getNegativeMonkey;
        }
        public int Id { get; init; }
        public List<Item> Items { get; init; }
        public Func<BigInteger, BigInteger> Operation { get; init; }

        public Func<BigInteger, BigInteger>? WorryLevelAdaption { get; init; }

        public Predicate<BigInteger> Test { get; init; }

        public Monkey PositiveTestMonkey => _getPositiveMonkey();

        public Monkey NegativeTestMonkey => _getNegativeMonkey();

        public void Turn()
        {
            foreach (var item in Items)
            {
                InspectionCount++;
                item.WorryLevel = Operation(item.WorryLevel);

                if (WorryLevelAdaption != null)
                {
                    item.WorryLevel = WorryLevelAdaption(item.WorryLevel);
                }

                if (Test(item.WorryLevel))
                {
                    PositiveTestMonkey.Items.Add(item);
                }
                else
                {
                    NegativeTestMonkey.Items.Add(item);
                }
            }
            Items.Clear();
        }

        public long InspectionCount { get; private set; }
    }
}
