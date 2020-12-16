using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day15 : IDay
    {
        public int Day => 15;
        public void RunPart1(bool silent)
        {
            var numbers = Input.ToList();
            var indices = numbers.Select((a, b) => (a, b)).ToDictionary(p => p.a, p => new List<int> {p.b});
            var last = numbers.Last();
            for (var i = numbers.Count; i < 2020; i++)
            {
                var places = indices[last];
                if (places.Count < 2)
                    last = 0;
                else
                    last = places.First() - places.Skip(1).First();

                if (!indices.ContainsKey(last))
                    indices[last] = new List<int>();

                indices[last].Insert(0, i);
                indices[last] = indices[last].Take(2).ToList();
            }

            Console.WriteLine(last);
        }

        public void RunPart2(bool silent)
        {
            var numbers = Input.ToList();
            var indices = new Dictionary<int, List<int>>(30000000);
            foreach (var pair in numbers.Select((a, b) => (a, b)))
                indices.Add(pair.a, new List<int> {pair.b});

            var last = numbers.Last();
            for (var i = numbers.Count; i < 30000000; i++)
            {
                if (i % 10000 == 0)
                    Console.WriteLine(i);

                var places = indices[last];
                if (places.Count < 2)
                    last = 0;
                else
                    last = places.First() - places.Skip(1).First();

                if (!indices.ContainsKey(last))
                    indices[last] = new List<int>();

                indices[last].Insert(0, i);
            }

            Console.WriteLine(last);
        }

        private int[] Input =
        {
            18,8,0,5,4,1,20
        };
    }
}
