using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day10 : IDay
    {
        public int Day => 10;
        public void RunPart1(bool silent)
        {
            Console.WriteLine(Part1());
        }

        public void RunPart2(bool silent)
        {
            Console.WriteLine(Part2());
        }

        public static int Part1()
        {
            var deviceRating = Input.Max() + 3;
            var last = 0;
            var differences = new List<int>();
            foreach (var jolt in Input.OrderBy(j => j))
            {
                differences.Add(jolt - last);
                last = jolt;
            }

            differences.Add(deviceRating - last);

            var aggregate = differences.GroupBy(j => j).Aggregate(1, (acc, i) => acc * i.Count());
            return aggregate;
        }

        public static long Part2()
        {
            var validConnections = Input.ToDictionary(a => a, adapter => Input.Where(a => a > adapter && a <= adapter + 3).ToList());

            var partials = new Dictionary<int, long>();
            foreach (var adapter in validConnections.OrderByDescending(a => a.Key))
                partials[adapter.Key] = adapter.Value.Any() ? adapter.Value.Select(a => partials[a]).Sum() : 1;

            var stems = Input.Where(a => a > 0 && a <= 0 + 3);
            var value = stems.Select(s => partials[s]).Sum();
            return value;
        }

        private static int[] Input =
        {
            73,
            114,
            100,
            122,
            10,
            141,
            89,
            70,
            134,
            2,
            116,
            30,
            123,
            81,
            104,
            42,
            142,
            26,
            15,
            92,
            56,
            60,
            3,
            151,
            11,
            129,
            167,
            76,
            18,
            78,
            32,
            110,
            8,
            119,
            164,
            143,
            87,
            4,
            9,
            107,
            130,
            19,
            52,
            84,
            55,
            69,
            71,
            83,
            165,
            72,
            156,
            41,
            40,
            1,
            61,
            158,
            27,
            31,
            155,
            25,
            93,
            166,
            59,
            108,
            98,
            149,
            124,
            65,
            77,
            88,
            46,
            14,
            64,
            39,
            140,
            95,
            113,
            54,
            66,
            137,
            101,
            22,
            82,
            21,
            131,
            109,
            45,
            150,
            94,
            36,
            20,
            33,
            49,
            146,
            157,
            99,
            7,
            53,
            161,
            115,
            127,
            152,
            128,
        };
    }
}
