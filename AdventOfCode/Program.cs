using System;
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

                Run(day);

                return;
            }

            if (args.FirstOrDefault() == "latest")
            {
                var day = days.OrderByDescending(d => d.Day).FirstOrDefault();
                if (day == null)
                    throw new IndexOutOfRangeException("Cannot find day");

                Run(day);

                return;
            }

            foreach (var day in days.OrderBy(d => d.Day))
                Run(day);
        }

        private static void Run(IDay day)
        {
            Console.WriteLine($"Day {day.Day}...");

            var part1Duration = Duration(() => day.RunPart1(false));
            var part2Duration = Duration(() => day.RunPart2(false));

            Console.WriteLine($"Duration {part1Duration}ms {part2Duration}ms");
        }

        private static double Duration(Action action)
        {
            var start = DateTime.Now;
            action();
            return (DateTime.Now - start).TotalMilliseconds;
        }
    }
}
