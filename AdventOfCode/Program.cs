using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var selectedDay = default(int);
            if (args.Length > 0)
                int.TryParse(args.First(), out selectedDay);

            var bigBoye = args.Any(a => a == "--bigboye");

            var days = Assembly.GetExecutingAssembly()
                               .DefinedTypes
                               .Where(t => t.IsClass && t.GetInterfaces().Any(i => i == typeof(IDay)))
                               .Select(t => (IDay)Activator.CreateInstance(t))
                               .Where(d => d != null);

            if (selectedDay != default)
            {
                var day = days.FirstOrDefault(d => d.Day == selectedDay);
                if (day == null)
                    throw new IndexOutOfRangeException($"Cannot find day {selectedDay}");

                Run(day, bigBoye);

                return;
            }

            if (args.FirstOrDefault() == "latest")
            {
                var day = days.OrderByDescending(d => d.Day).FirstOrDefault();
                if (day == null)
                    throw new IndexOutOfRangeException("Cannot find day");

                Run(day, bigBoye);

                return;
            }

            foreach (var day in days.OrderBy(d => d.Day))
                Run(day, bigBoye);
        }

        private static void Run(IDay day, bool bigBoye)
        {
            Console.WriteLine($"Day {day.Day}...");
            var durations = new List<double>
            {
                Duration(() => day.RunPart1(false)),
                Duration(() => day.RunPart2(false)),
            };

            if (bigBoye)
                day.RunPart3(task => durations.Add(Duration(task)));

            Console.WriteLine($"Duration {string.Join(" ", durations.Select(d => $"{d}ms"))}");
        }

        private static double Duration(Action action)
        {
            var start = DateTime.Now;
            action();
            return (DateTime.Now - start).TotalMilliseconds;
        }
    }
}
