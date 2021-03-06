using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Days
{
    public class Day03 : IDay
    {
        public int Day => 3;
        public void RunPart1(bool silent)
        {
            Console.WriteLine(Part1());
        }

        public void RunPart2(bool silent)
        {
            Console.WriteLine(Part2());
        }

        public void RunPart3(Action<Action> timer)
        {
            timer(() => Console.WriteLine(Part3()));
        }

        public static int Part1()
        {
            var map = Input.Select(r => r.Select(c => c == '#').ToArray()).ToArray();

            return FindHits(map, 3, 1);
        }

        public static long Part2()
        {
            var map = Input.Select(r => r.Select(c => c == '#').ToArray()).ToArray();

            var hits = new (int x, int y)[] {( 1, 1), (3, 1), (5, 1), (7, 1), (1, 2)}.Select(pair => FindHits(map, pair.x, pair.y));

            return hits.Aggregate(1L, (acc, i) => acc * i);
        }

        public static BigInteger Part3()
        {
            var map = File.ReadLines("bigboye.3.txt").Select(r => r.Select(c => c == '#').ToArray()).ToArray();
            var xSlopes = new[] {2, 3, 4, 6, 8, 9, 12, 16, 18, 24, 32, 36, 48, 54, 64};
            var ySlopes = new[] {1, 5, 7, 11, 13, 17, 19, 23, 25, 29, 31, 35, 37, 41, 47};

            var slopes = xSlopes.SelectMany(x => ySlopes.Select(y => (x, y)));

            var hits = slopes.Select(pair => FindHits(map, pair.x, pair.y)).ToList();

            return hits.Aggregate(BigInteger.One, (acc, i) => i * acc);
        }

        private static int FindHits(bool[][] map, int xSlope, int ySlope)
        {
            var hits = 0;
            var x = 0;
            var y = 0;
            do
            {
                if (map[y][x % map[y].Length])
                    hits++;

                x += xSlope;
                y += ySlope;
            } while (y < map.Length);

            return hits;
        }

        public static string[] Input =
        {
"....##..#........##...#.#..#.##",
".#.#..#....##....#...#..##.....",
"##.#..##..#...#..........##.#..",
".#.##.####..#......###.........",
"#.#.#...........#.....#...#....",
"#.......#....#.#.##..###..##..#",
".#...#...##....#.........#.....",
"..........##.#.#.....#....#.#..",
".......##..##...#.#.#...#......",
".#.#.#...#...##...#.##.##..#...",
"........##.#.#.###.........##..",
"#.#..#.#.#.....#...#...#......#",
".#.#.#...##......#...#.........",
".#..##.##.#...#...##....#.#....",
".##...#..#..#......##.###....##",
".....#...#.###.....#.#.........",
"#.##..#....#.#.#.#.............",
"........#...#......#...#..#....",
"##..##...##.##...#...#.###...##",
"#.#....##.#...###......#..#.#..",
"..#.....#.##......#..........#.",
"#.......#..#......#.....#....#.",
".....###...........#....#.##...",
"#.#........##.......#.#...#.##.",
".#.#.#........#........#.#.....",
"#..#..##.....#.###..#.#.#.##..#",
"..#.#...#..##.#.#.#.......###..",
"........#........#..#..#...#...",
"##............#...#..##.##...#.",
"#....#.#.....##...#............",
"............#...#..#.#.#....#..",
"#.#.#...##.##.#....#....#......",
"................###.....#.....#",
"##.#####.#..#...###..#...###...",
"...#.....#...#.#....#...#..#...",
".......#....##.##.#.##.........",
"..#..#..##.....#...#.#.....#...",
"...#...#.#.##.#..###.......#...",
"...#...........#.#####..##..#..",
"#.#...#........####..#......#.#",
"#..#.##...........#.#......#.##",
"#.#..#....##..#..##.#..........",
".....#..#.....#........##..#...",
"...###.......#.##.......#......",
"...##..#..#...#....#.###...#...",
"....####....#........#.##.#.#.#",
"#....#.....#.###.##...##..##.##",
".##.#...#.##.#......#..##.#....",
"...#.............#.............",
"..##..##.#.....#........##....#",
"#.....#....#.......####...#..#.",
"..#...#..#...#...##.#....##....",
".#...##....#....#..#....#......",
"##..#.#...##......#..#.......##",
"..#...#.##..#.....#.#...#..#.#.",
"#..##....#..........#..........",
".#........#..#......#......#.#.",
"...##.#.........#.#....#.#...#.",
"#.....#.#..#...#...#..#...#...#",
"#.........#.#.........##.......",
".#.......#......#.........###..",
".#..#..##...........#.....#..#.",
".#....................#.....#..",
".##.....#....#....#.###.....#..",
"...##.#.....#.#....#.........#.",
".........##.....#.....#.##..#..",
"......#......#.#..#..###...#..#",
"..##...#.#..#...#.#....#.......",
"..#..##.###.#..#..#..#......#..",
".....##...##.........#...##...#",
"###.#..##....##...##.#..###....",
"#...#.#..##......##...#.#..#...",
"..........#....###....#........",
"#.#.#.#.#.....#..##.##.....#...",
".##.....#...#.....#......#.....",
".#..........#.#.............#..",
".#..##..#.#..##...#....#.##...#",
"..#.#..........#...#..........#",
".#.......#.......#...#..#.....#",
"##.#...##...#......#.#..#......",
"#####..#....#..#...#...#.#.....",
"....#.......#.#..#.............",
"#..#..#.#.####...#....#....##..",
"#..#.##.#......#...#......#....",
"#...##.##...#....#..........##.",
"..#..#.......##.#....#...#.#...",
".....#.##..............##.....#",
"..##.##...##.....#.........#.#.",
".#....##...##...##..#....##..#.",
".#...#....#..#......#.#........",
"#....#.#.#..............#....##",
"..##..#..#....#####.#....#.#.##",
"#....#...#.##..#.##.........###",
"#..#..#....#...............#.#.",
"#....##...##........##.##.#.##.",
"......#......##....##.....#.###",
"#...##..#..#.....#.#........##.",
"..#.#.##...#...#....#..###...#.",
"#..##..#.###..##.#.#....#......",
"..###..#.##........###.....#...",
"#.............#.............#..",
".#.##....#..##.#...#....#.#####",
"###.....###.#......##..#..##...",
".#.#......##.#....#....#.#..#..",
"###..#..#....#......###.##.....",
"......#.......#......#..##..##.",
"..#..#...#..#....#.##....#.#..#",
".......##..#........#.#.##.....",
".#...#..#........#..#.#..#..#.#",
".#..#.##.......#......#...#.#..",
".##..##......##.#...#......####",
".....#....#......#.....#......#",
"..........#.#.#...##.#..#.#....",
"...#.......##......#..#.#.##...",
".###..#.#.#....................",
"##...#...#.##............#.....",
"....#....#.##...#..#.#...##....",
"..#.#....#.###...#...#.#.####.#",
"..#..#.#...#.#......##.........",
"#..#..####.##.#.#..###....#...#",
"....#..........#.##.#..#.#.#.#.",
"#.#.##.........#.....##...#..##",
"#......#...#.##.#......#..#.#..",
"#...#........#..#..#...##...#..",
".....#.####..##..#.#.##..#...#.",
"#..#........#........#...#....#",
"...........#..#.....#.#.#.#....",
"....#......#....#...#....##....",
".#.#..#...#.#....#..#.#....##.#",
"....#...#...#.##..#...#..##...#",
"#######...............##.....##",
".#.#..............#....#..#.###",
"#......#.#......###....###.....",
"##..#...#.##..##..##.#....#....",
"#....##..#..#...#.#.#...#......",
"..........#..#.##..##.##.#..##.",
"....#.#.#.....##........#..#...",
"..###...#.....##.##.....##..##.",
"....#.#..#.#.......#.......#...",
"..##.#..#.....##...###...#...#.",
"..#.........#...##...#...#..#..",
"..#..#..#..#..##.#...##..#.#...",
"...##..#..##..#..####...#.....#",
"............#............###...",
".#.#.###.#.....#.#.#..#.###..#.",
"...#.........#.....####........",
"....##.#..##.#.............#...",
"....#.##.....#..#.....#......##",
"..........#.............#...##.",
"#..#.....#.......##..###.......",
"..##.#...........#.......#..#..",
"...#...#.#.##.###....#.#..#....",
"...#..........##..#..#..#...###",
".........#.....#..##.....#..#..",
"#........#...#...#..#.#....##..",
".#.#.....####..#.##.#..........",
"###.......##..#.##...#.....#...",
"..###.##.#..#..#..#.....##...#.",
".........#.....##.#..#..##.....",
"#..#..##...###..............#..",
"#....#.#....#..#.....#..####...",
"####..#.....##...#..#.#.#.#...#",
"...#....#.....#.##.#.#.#....##.",
"..........#...#.....###....#.##",
"#....#.#.#....#..#..#.....#....",
".....#.#...#......#....#......#",
".####....##...#...#......##..#.",
".#...#..#....#..#..............",
"##.#...##...#.##..#......#.....",
"..####.##..#....#.#......#.#.##",
"........#.....##...#.#..##....#",
"....#.#.#.#.###...#.#...##...##",
"#.....#...####.#....#.#........",
"..#.....#...##.........###.....",
"....#....#....#..#......#####.#",
"###.....#..#.#.#......#.##.#...",
"....#.##......#..#.#...........",
".#....#....#.#..#.......#...##.",
"...................#.#.#..#....",
"##...#.....#........#....#...#.",
"........##......#...##.#..#.#.#",
"#.#..###...#....#.#...#.......#",
"#..........##......#..#..#.....",
".............#...##.#...#......",
"#..#....##..#.........#..#.###.",
".....#..........#....##.#...##.",
"###....................#.#.##..",
"#........##...##......#....###.",
"#....#.............#....#...#..",
"##.......##.#.......#....#..#..",
"##...#............#..#.#....##.",
"..#.#..#...#####..........#....",
"..#.........##.....#.#...####..",
"....#..............#...........",
"..#...#.#.#..#.......##.#.#.#..",
"....#.##.....##..#.....#..####.",
"#...#...#...#.......#.........#",
"......#..#.####...###.#.#.....#",
".......#..#..#.....#.#..##.#..#",
".#......##..#............#.....",
".#........#.#....#....#........",
".....#.#..#.##.#..##....#..#...",
"#.#...........#....##.....#....",
"..#..#..##.###..##..#..###.#.##",
"##.#..#...##.#.........#...#.#.",
"......#..#..##...#....#...#.##.",
"#.##.......................#...",
".......#..#..#.##..##......#...",
"..#.#...#.....#..###....#...#..",
"##..#.....#..#..#.##.....#...##",
"#...##...###...#.#..###....#...",
"...#.#.#..####.....#...##....#.",
".#.#..##.....#..#.....##..##..#",
"#...#..........#.##.#.#........",
"..##....#.##....#..##......#...",
"....#..........###.....####..##",
"...........###....##.#.#.#.#...",
"...#......................####.",
"#.#.#...#.#.#.#.#......#.....##",
"..###...#.####...#..##..#....#.",
"....#....#.......#...#.........",
".#.###.............##..#...#...",
"....#.#....##...#.....#.##.....",
"#.......##.....#.#.....#....##.",
"....##.....###..#.#..#....#...#",
"......#..##...#......#.....#.##",
".#.....#.##.###....#.....#..###",
"...#..#.###.#....#..#..#...##.#",
"...##..#...#..#.#.#..#...#.....",
"##.####...##..#.#.#....#.......",
"..##..#.#.......##.#......##.#.",
"....##....#....#..#....#..##.#.",
"..##..........##....#...#.#..#.",
"#.#...#.#.###.#.#..##.#...#....",
".....#..#.............#...#...#",
"....#.#..#...##...#....#.##....",
"#..#...#.###.....#...#.....#.#.",
"#####....#....#....#.......#.##",
"#...##....##.#.#...#.....##.#..",
"#.......#...#..#..#...#....#...",
"....#......#.#..........#....##",
"#.###...#.#..##..#.##........#.",
"#..#.....##.......#..#..#.#....",
"...#...#.##...#....#.#.#.#...#.",
"...##..#.#....#......###......#",
"#.#....#...#..#..#....#........",
"..#..#.#...##..........#.###...",
"#..........#...#..#....#....###",
"..#..#.#....#..............#...",
"...#........#...#.#....###.#..#",
"....#.#.#................#..#.#",
"..#........##.#....#.#..#......",
"...##..#..#.......#..#......#.#",
"..#..#....#.........#....#.##..",
"#.....#....###.#..#..#...#...#.",
"..#..##.###.#..##....#.###.....",
"...#...####..#........###.#....",
".........#.#...#..#..#.#.......",
".##.........##.#..............#",
"..#.#.#.....###........#.#.#..#",
"....##..#....#....#.#..#.......",
"#.#.....#...#........##........",
".##.#.#..#..#.#.#.........#....",
"#.....#..#.##...#...#..........",
"##..#....#....##.#..#.........#",
"................#.##.#......#.#",
"..#..#.#........##...###..#...#",
"##........#.......#...##.##..#.",
"##....#.....#..##....#.......#.",
"#.#....#.#........#..#.........",
"......##......#...#.....#.##...",
"###....#..........##.#.#......#",
"......#...###.........###..#...",
".####....#...##..#.#.....#...#.",
".##...#...###....#...#.#..###..",
"#..#......##...#.###..###...#..",
"#....#.#.#..#....##...#.##..#..",
"..#.....#...#..........#.##.###",
"#.....#....###.......##..##.#..",
"#..##...#..#....#.###......#...",
"#..#........##..#.....#.#.#....",
"#.##.#.#..#....#.#.............",
".#...............#....##.......",
".#.##......##........#...#..#.#",
".#...#....#....#...#..#...##...",
".....#..###...##........#.#....",
"...#.......#....##..#..#....#..",
"...###....#........#..#.###.#..",
"......##..##..............###.#",
".......#.####..##....#.#....#..",
"#...#......#...#..#.....##....#",
".#..#..###....#..##.##.#.......",
"#......##......#..##....#..##..",
".....#..#.#......##.##..##.....",
"...#..#.......#......#.........",
"....#..####......#..#....#...#.",
"..#.#..#...#....#....#.......#.",
"####..#........#.###...##.#.#.#",
".......##........#.#.#...##....",
"...#.........#..#.#..##....#...",
".....#..#...#.#....#...#.#.##.#",
"#..##.....#.....##.......#...#.",
".......##.#.#.....#....#......#",
"...#...#.##...#......#....#....",
"..#..#.#...#..#.....#...###.#..",
".........#...#..#.......##.....",
"..##...................#..#.###",
".##.##..#.#...#.#....#.....##..",
"#.#...##...#...#...##..#......#",
"....#..#...#.....##.#.....#..##",
"##.#..........###..#...#..#....",
"...##....#.##....#......#......",
".....#.........#....#.#.......#",
".......#............#.#.....#..",
"..#..#...#..#####..#....##.....",
"...##......##...#.#........##..",
".....#..###...##.#.#.##.#...#..",
"..#.#.#..##..#.##...##.#.#.....",
"......##...#..##......#.#......",
"......................#........",
"#...#..#....#..#.#.##.#.....#.#",
".#......#.#....#.#.#..#....#...",
".#..#.#.#..#....#..............",
        };
    }
}
