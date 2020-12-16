using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day13 : IDay
    {
        public int Day => 13;

        public void RunPart1(bool silent)
        {
            var busses = Input.Where(c => c != "x").Select(long.Parse).ToList();

            foreach (var bus in busses)
            {
                var multi = Start / bus + 1;
                Console.WriteLine($"{bus} -> {bus * multi} -> {bus * multi - Start}");
            }
        }

        public void RunPart2(bool silent)
        {
            List<(long Bus, long Index)> rules = Input.Select((s, i) => (s, (long)i)).Where(s => s.s != "x").Select(s => (long.Parse(s.s), s.Item2)).ToList();

            var t = 0L;
            var matched = new HashSet<long>();
            foreach (var rule in rules)
            {
                var lcm = Lcm(matched.ToArray());
                while ((t + rule.Index) % rule.Bus != 0)
                    t += lcm;

                matched.Add(rule.Bus);
            }

            Console.WriteLine(t);
        }

        private bool ValidMulti(long t, List<(long Bus, long Index)> rules)
        {
            foreach (var (bus, index) in rules)
            {
                var raw = bus * t - index;
                if (Valid(raw, rules))
                    return true;
            }

            return false;
        }

        private bool Valid(long t, List<(long Bus, long Index)> rules)
        {
            foreach (var (bus, index) in rules)
            {
                if ((t + index) % bus != 0)
                    return false;
            }

            return true;
        }

        private long Start = 1002578;

        private string[] Debug = "1789,37,47,1889".Split(',');
        private string[] Input = "19,x,x,x,x,x,x,x,x,x,x,x,x,37,x,x,x,x,x,751,x,29,x,x,x,x,x,x,x,x,x,x,13,x,x,x,x,x,x,x,x,x,23,x,x,x,x,x,x,x,431,x,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,17".Split(',');

        static long Gcf(long a, long b)
        {
            while (a != b)
            {
                if (a > b)
                    a = a - b;
                else
                    b = b - a;
            }
            return a;
        }

        private static long Lcm(params long[] nums)
        {
            switch (nums.Length)
            {
                case 0:
                    return 0;
                case 1:
                    return nums[0];
                case 2:
                    return nums[0] * nums[1] / Gcf(nums[0], nums[1]);
                default:
                    return Lcm(nums[2..].Union(new [] {Lcm(nums[..2])}).ToArray());
            }
        }

        // Returns modulo inverse of
        // 'a' with respect to 'm'
        // using extended Euclid Algorithm.
        // Refer below post for details:
        // https://www.geeksforgeeks.org/multiplicative-inverse-under-modulo-m/
        static long inv(long a, long m)
        {
            long m0 = m, t, q;
            long x0 = 0, x1 = 1;

            if (m == 1)
                return 0;

            // Apply extended
            // Euclid Algorithm
            while (a > 1)
            {
                // q is quotient
                q = a / m;

                t = m;

                // m is remainder now,
                // process same as
                // euclid's algo
                m = a % m;
                a = t;

                t = x0;

                x0 = x1 - q * x0;

                x1 = t;
            }

            // Make x1 positive
            if (x1 < 0)
                x1 += m0;

            return x1;
        }

        // k is size of num[] and rem[].
        // Returns the smallest number
        // x such that:
        // x % num[0] = rem[0],
        // x % num[1] = rem[1],
        // ..................
        // x % num[k-2] = rem[k-1]
        // Assumption: Numbers in num[]
        // are pairwise coprime (gcd
        // for every pair is 1)
        public static long findMinX(long[] num,
            long[] rem,
            long k)
        {
            // Compute product
            // of all numbers
            long prod = 1;
            for (long i = 0; i < k; i++)
                prod *= num[i];

            // Initialize result
            long result = 0;

            // Apply above formula
            for (long i = 0; i < k; i++)
            {
                long pp = prod / num[i];
                result += rem[i] *
                          inv(pp, num[i]) * pp;
            }

            return result % prod;
        }
    }
}
