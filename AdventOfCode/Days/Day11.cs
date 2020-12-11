using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AdventOfCode.Days
{
    public class Day11 : IDay
    {
        public int Day => 11;
        public void RunPart1(bool silent)
        {
            var state = ParseSeats(Input);
            var done = false;
            while (!done)
            {
                (done, state) = Mutate(state);
            }

            var occupied = 0;
            for (var y = 0; y < state.GetLength(1); y++)
            for (var x = 0; x < state.GetLength(0); x++)
            {
                if (state[x, y] == '#')
                    occupied++;
            }

            Console.WriteLine(occupied);
        }

        private (bool Mutated, char[,] State) Mutate(char[,] state)
        {
            var newState = new char[state.GetLength(0), state.GetLength(1)];
            var mutated = false;

            for (var y = 0; y < state.GetLength(1); y++)
            for (var x = 0; x < state.GetLength(0); x++)
            {
                switch (state[x, y])
                {
                    case '.':
                        newState[x, y] = state[x, y];
                        break;
                    case '#':
                        if (CountAdjacent(state, x, y) >= 4)
                        {
                            mutated = true;
                            newState[x, y] = 'L';
                        }
                        else
                        {
                            newState[x, y] = '#';
                        }
                        break;
                    case 'L':
                        if (CountAdjacent(state, x, y) == 0)
                        {
                            mutated = true;
                            newState[x, y] = '#';
                        }
                        else
                        {
                            newState[x, y] = 'L';
                        }
                        break;
                }
            }

            return (!mutated, newState);
        }

        private int CountAdjacent(char[,] state, int x, int y)
        {
            var width = state.GetLength(0) - 1;
            var height = state.GetLength(1) - 1;
            var adj = 0;
            if (x > 0)
            {
                if (y > 0 && state[x - 1, y - 1] == '#')
                    adj++;
                if (state[x - 1, y] == '#')
                    adj++;
                if (y < height && state[x - 1, y + 1] == '#')
                    adj++;
            }

            if (y > 0)
            {
                if (state[x, y - 1] == '#')
                    adj++;
                if (x < width && state[x + 1, y - 1] == '#')
                    adj++;
            }

            if (x < width)
            {
                if (state[x + 1, y] == '#')
                    adj++;
                if (y < height && state[x + 1, y + 1] == '#')
                    adj++;
            }

            if (y < height && state[x, y + 1] == '#')
                adj++;

            return adj;
        }

        private static char[,] ParseSeats(string[] input)
        {
            var seats = new char[input.Length, input[0].Length];
            for (var x = 0; x < input.Length; x++)
            {
                var line = input[x];
                for (var y = 0; y < line.Length; y++)
                {
                    seats[x, y] = input[x][y];
                }
            }

            return seats;
        }

        public void RunPart2(bool silent)
        {
            var state = ParseSeats(Input);
            var done = false;

            while (!done)
            {
                (done, state) = Mutate2(state);
            }

            var occupied = 0;
            for (var y = 0; y < state.GetLength(1); y++)
            for (var x = 0; x < state.GetLength(0); x++)
            {
                if (state[x, y] == '#')
                    occupied++;
            }

            Console.WriteLine(occupied);
        }

        private void PrintState(char[,] state)
        {
            for (var y = 0; y < state.GetLength(0); y++)
            {
                var line = new StringBuilder(state.GetLength(0));
                for (var x = 0; x < state.GetLength(1); x++)
                {
                    line.Append(state[y, x]);
                }

                Console.WriteLine(line);
            }

            Console.WriteLine();
        }

        private (bool Mutated, char[,] State) Mutate2(char[,] state)
        {
            var newState = new char[state.GetLength(0), state.GetLength(1)];
            var mutated = false;

            for (var y = 0; y < state.GetLength(1); y++)
            for (var x = 0; x < state.GetLength(0); x++)
            {
                switch (state[x, y])
                {
                    case '.':
                        newState[x, y] = state[x, y];
                        break;
                    case '#':
                        if (CountAdjacent2(state, x, y) >= 5)
                        {
                            mutated = true;
                            newState[x, y] = 'L';
                        }
                        else
                        {
                            newState[x, y] = '#';
                        }
                        break;
                    case 'L':
                        if (CountAdjacent2(state, x, y) == 0)
                        {
                            mutated = true;
                            newState[x, y] = '#';
                        }
                        else
                        {
                            newState[x, y] = 'L';
                        }
                        break;
                }
            }

            return (!mutated, newState);
        }

        private int CountAdjacent2(char[,] state, int x, int y)
        {
            var width = state.GetLength(0) - 1;
            var height = state.GetLength(1) - 1;
            var adj = 0;
            var found = new List<(int X, int Y)>();

            // left
            for (var x2 = x - 1; x2 >= 0; x2--)
            {
                if (state[x2, y] == '#')
                {
                    found.Add((x2, y));
                    adj++;
                    break;
                }

                if (state[x2, y] == 'L')
                    break;
            }

            // right
            for (var x2 = x + 1; x2 <= width; x2++)
            {
                if (state[x2, y] == '#')
                {
                    found.Add((x2, y));
                    adj++;
                    break;
                }

                if (state[x2, y] == 'L')
                    break;
            }

            // up
            for (var y2 = y - 1; y2 >= 0; y2--)
            {
                if (state[x, y2] == '#')
                {
                    found.Add((x, y2));
                    adj++;
                    break;
                }

                if (state[x, y2] == 'L')
                    break;
            }

            // down
            for (var y2 = y + 1; y2 <= height; y2++)
            {
                if (state[x, y2] == '#')
                {
                    found.Add((x, y2));
                    adj++;
                    break;
                }

                if (state[x, y2] == 'L')
                    break;
            }

            // up-left
            for (var offset = 1; x - offset >= 0 && y - offset >= 0; offset++)
            {
                if (state[x -  offset, y - offset] == '#')
                {
                    found.Add((x -  offset, y - offset));
                    adj++;
                    break;
                }

                if (state[x -  offset, y - offset] == 'L')
                    break;
            }

            // up-right
            for (var offset = 1; x + offset <= width && y - offset >= 0; offset++)
            {
                if (state[x + offset, y - offset] == '#')
                {
                    found.Add((x + offset, y - offset));
                    adj++;
                    break;
                }

                if (state[x + offset, y - offset] == 'L')
                    break;
            }

            // down-left
            for (var offset = 1; x - offset >= 0 && y + offset <= height; offset++)
            {
                if (state[x -  offset, y + offset] == '#')
                {
                    found.Add((x -  offset, y + offset));
                    adj++;
                    break;
                }

                if (state[x -  offset, y + offset] == 'L')
                    break;
            }

            // down-right
            for (var offset = 1; x + offset <= width && y + offset <= height; offset++)
            {
                if (state[x + offset, y + offset] == '#')
                {
                    found.Add((x + offset, y + offset));
                    adj++;
                    break;
                }

                if (state[x + offset, y + offset] == 'L')
                    break;
            }

            return adj;
        }

        private void PrintMatch(List<(int X, int Y)> found, char[,] state)
        {
            for (var y = 0; y < state.GetLength(0); y++)
            {
                var line = new StringBuilder(state.GetLength(0));
                for (var x = 0; x < state.GetLength(1); x++)
                {
                    if (found.Contains((y, x)))
                        line.Append('O');
                    else
                        line.Append(state[y, x]);
                }

                Console.WriteLine(line);
            }

            Console.WriteLine();
        }

        private static string[] Debug =
        {
            "L.LL.LL.LL",
            "LLLLLLL.LL",
            "L.L.L..L..",
            "LLLL.LL.LL",
            "L.LL.LL.LL",
            "L.LLLLL.LL",
            "..L.L.....",
            "LLLLLLLLLL",
            "L.LLLLLL.L",
            "L.LLLLL.LL",
        };

        private static string[] Input =
        {
            "LLLLLLLLL.LLLLLLL.LLLLLLLLLL.LLLLL.LLLLLL.LLLLLLLL.L.LLLLLL..L.LLLLL.LLLLLLLLLL.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLL..LLLLLLLLLL.LLLLL.LLLLLLLL.LLL.LL.LLLLLLLLLLLLLLLLL.LLLLLLLLLL.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLLLLLLL.LLLLL.LLLLLLLLL.LLLLLLL..LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLL.LLLLLLL",
            "LLLLLLLLLLLLLLLLL.LLLL.LLLLL.LLLLL.LLLLLL.LLLL.LLL.LLLLLLLLL.LLLLLLL.LLLL.LLLLL.LLLLLLLLLLL",
            "L.LLLLLLL.LLLLLLL.LLLL.LL.LLLLLLL..LLLLLL.LLLLLLLL.LLLLLLLLL.LLLLLLLLLLLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLLLLLLLLLL.LLLL.LLLLL.LLLLL.LLLLLL.LLL.LLLL.LLLLLLLLL.LLLLLLLLLLL.L.L.LLLLLLLLLLLLLL",
            "LLLL.LLLLLLLLLLLLLLLL.LL.LLLLLLLLL.LL..LL.LLLLLLLL..LLLLLL.L..LLL.LL.LLLLLLLLLL.LLLLLLLLLLL",
            "LLLLLLL.LLLLLLLLL.LLLL.LLLLL..LLLL.LLLLLL.LLLLLLLLLLLLLLLLLL.LLLLLLL.LLLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLLL.LLL..LLLL.LLLLLLLLLLLL.LLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLL.LLLL.LLLLLLLLLLL",
            ".L.....L......L.L.L...L...LL..........L....L..LL.....L.L....L.....L......L.......L...L..L.L",
            "LLLLLL.LL.LLLLLLLLLLLLLLLLLL.LLLLL.LLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLLLLLLLLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLLLLLLL.LLLLL.LLLLLLLLLLLL.LLLLLLLL.LLLLLLLLL.LLL.LLLLLLLLL.LLLLLLLLL.LLLLLL",
            "LLLLLLLLLLLLLLLLLLLLLL.LLLLLLLL.LLL.LLLLL.LLLLLLL.LLLLLL.LLLLLLLLLLL.LLLLLLLLLL.LLLLLLL.LLL",
            "LLLLLLLLL.LLLLLLLLLLLL.LLLLL.LL.LL.LLLLLL.LLLLLLLLLLLL.L.LLL.L.LLLLLLLLLLL.LLLLLLLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLL.LLLL.LLLLL.LLLLL.LLLLLLLLLLL.LLL..LLLLLLLL.LLLLLLL.LLLLL.LLLLLLLLLLLLLLLL",
            "LLLLLLLLL.LLLL.LLLLLLL.LLLLL.L.LLL.LLLLLL.LLLLLLLL.LLLLL.LLL.LLL.LLL.LLLLLLLLLL..LLL.LLLLLL",
            "LLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLL..LLL.LLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLL.LLLLLLLLLLL",
            "..LLL.....L...L.L....L.L.L.L.LL...L.LL.L..LLL......L.....L.L...L..LL.LL...LL..L....L.......",
            "LLLLLLLLL.LLLLLLL.LLLL.LLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLL.LLLLL.LLL..LLLLLLLLLL.",
            "LLLLLLLLL.LLLL.LLLLLLL..LLLLLL.LLL.LLLLLL.LLLLLLLL.LLLLLLLLL.LLLLLLLLLL.LLLLLLLLLLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLLLLLLL.LLLLL.LLLLL.LLLLLL.LLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLL",
            "LL.LLLLLL.LLLLLLL.LLLL.LLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLLL.LL.LLLLL.L.LLLLLLLLLL.LLL",
            "LLLLLLLLL.LLLLLLLLLLLL.LLLLLLLLLLL.LLLLLL.LLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLLLLL.LLLLLLLLLLL",
            "......L...LL.L.LL.....LL...L.L.L..L.........L..LLL.L..L.L.LLL..L..L...L...L..L....L.LL.L...",
            "LLLLLLLLL.LLLLLLLLLLLL.LLL.LLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLL.LLLLLLLLLL.LLLLLLLLLLL",
            "LLLL.LLLLLLLLLLLL.LLLL.LLLLLLLLL.L.LLLLLL.LLLLLLLL.L.LLLLL.LLLLLLLLL.LLLLL.LLL..LLLLLLLLLLL",
            ".LLLLLLLL.LLLLLLL.LL.LLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL..LLLLLL.LLL.L.LLL..LLLL.LL.LLL",
            "LLLLLLLL..LLLLLLLLLLLLLLLLLLLLLLLL.LLLLLL.LLLLLLL..LLLLL.LLL.LLLLLLL.LLLLL.LLLLLLLLLLLLLLLL",
            "LLL.LLLLL.LLLLLLLLLLLLLLLLLL.LLLLL.LLLLLL.LLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLL.L.LLLLLLLLLLLL.L",
            "LLLLLLLLL.LLLLLLL.L.LL.LLLLLLLLLLL.LLLLLL.LLLLLLLL.LLLLLLLLLLLLLLLL..LLLLL.LLLL.LLLLLLLLLLL",
            "LLLL.LLLL.L.LLLLL.LLLL.LLLLL.LLLLL.L.LLLL.LLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLL.LLLLLLLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLLLLLLL.LLLLL.LLLLLLLLLLLLLLLLLL.LL.LLLLLLLLL.L.LLLLL.LLLLLL.LLLLLLLLLLLL.LL",
            ".LLLLLLLL.L.LLLLL..LLL.LLLLL.LLLLL.LLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLLLLLL.LLLLLL.LLLLLLLLLLL",
            ".LL...LL.L..LL.LL....L..LL....L..L..L......L.....LLL....L.LLL..L..LLLLL..L.......L.....L..L",
            "LLLLLLLLL.LLLLLLL..LLL.LL.L.LLLLLL.LLLLLL.L.LLLLLLL.LL.LLLLLLLLLLLLL.LLLLL.LLLL.LLLLLLLLLLL",
            "L..LLLLLLLLLLLLLL.LLLL.LLLLLLLLLLL.LLLLLL.LLLLLLLLLLLLLLLLL.LLLLLLLLL.LLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLL..LLLLLLLLLLL.LLLLL.LLLLLLLLLLLL.LLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLLLLLLL.LLLLL.LLLLL.LLLLLL.LL.LLLL..LLLLLLLLL.LLLLLLL.LLLLL.LLLL.LLLLLL.LLLL",
            ".LLLLLLLLLL.LLLLL.LLLL.LLLLL.LLLLL.LLLLLL.LLLLLLLL.LLLLL.LLL.LLLLLLLLLLLLL.LLLLLLLL..LLLLLL",
            "LLLLLLLLLLLLLLLLL.LL.LLLLLLL.LLLLL.LLLLL..LLLLLLLLLLLLLL.LLL.LLLLLLL.LLLLL.LLLL.LLLLLLLLLLL",
            ".....LL...L..L.LL........L.......L.LLLLL..L.LLL...L..L....L.L..L.....L.L.........L..L.L...L",
            "LLLLLLLLLLLLLLLLL.LLLL.LL.LL.LLLLL.LLLL.L.LLLLLLLL.LLLLLLL.L.LLLLLLL.LLLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLLLLLLL.LLLLL.LL.LLLLLLLLLLLLLLLLLL.LLL.LLLLL.LLLLLLL.LLLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLLLLLLL.LL.LL.LLLLL.LLLLLL.LLLLLLLL.LLLLLLLLL.L.LLLLL.LLLLLLLLLLLLLLLLLLLLLL",
            "LLLLLLLLLLL.LLLLL.LLLL.LLLLLLLLLLLLLLLLLL.LLLLLLLL.LLLLLL.LL.LLLLL.L.LLLLL.LLLLLLLLLLL.LLLL",
            "LLLLLLLLLLLLLLLLLLLLLL.LLLL.LLLLLLL.LLLLLLLLL.LLLL.LLLLLLLLL.LLLLLLL.LLLLLLLL.L.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLL.LLLL..LLLL.LLLLL.LLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLL.LLLL.LLLLLLLLL.L",
            "LLLL.LLLL.LL.LLLL.LLLL.LLLLL.LLLLL.LLLLLLLLLLLL.LL.LLLLLLLLL.LLLLLLL.LL.LLLLLL.LLLLLLLLLLLL",
            "LLLLLLLLL.LLL.LLL.LLLL.LLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLL.LLL.L.LLLLLLLLLL.LLLLLLLLLLL",
            "L.L.L.L..LLLL.....LL.LL...L......LL..LLL.L.L.LL.LL....L.L....L..LL.L.......LLLLLLL.L....LL.",
            "LLLLLLLLL.LLLLLLLL.LLL.LLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLLLLLLLLLL.LLLL.LLLLL.LLLLL.LL.LLL.LLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLL",
            "L.LLLLLLLLLLLLLLL..LLLL.LLLLLLL..L.LLLLLL.LLL.LLLL.LLLLLLLLL.LLLLLLL.LLLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLLLLLLL.LLLLL.LLLLL.LLLLLL.LLLLLLLL.LLL.LLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLLLLL",
            "LLLLLLLLL.L.LLLLL.LLLL.LLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLL",
            "LLLLLLLLLLLLLLLLL.LLLL.LLLLL.LL.LL.LLLLLLLLLLLLLLL.LLL.LL.LLLLLLLLLL.LLLLLLLLLL.LLLLLLLLLLL",
            "LLLLLLLLLLLLLLLLLLLLLLLLLL.L.LLLLL.LLLLLL.LLLLLLLL.LLLLLLLLLLLLLLLL..LLLLL.LLLLLLLLLLLLLLLL",
            "LLLLLLLLLLLLLLLLL..LLL..LLLLLLLLLL.LLLLLLLLLLLLLLL.LLLLL..LL.LLLLLLL.LLLLL.LLLL.LLLLLLLLLLL",
            "...LL........L..L......L.LL.....L........L.L.L.L.L.L....L.L...L......LL.......L..L..LL.L...",
            "LLLLLLLLLLLLLLLLL.LLLL.LLLLL.LLLLL.LLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLL.LLL.LLLLL.LLLLLLL.LLLLLLLLLL.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLL..LLLLLLLLL.LLLLLLLLLLLL.LLLLLLLL.LLLLLLLLL.LLL.LLLLLLLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLL.LLLLLLLLL.LLLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLL.LLLLLLLL.LLLLLLLLLLLLLLL.L.LLLLL.LLLL.LLLLLLLLLLL",
            "LL.......L...LLL.....L...L........L...LL....L....L.L...L..LL.LL...LL.L.LL...L......LLL.LLL.",
            "LLLL.L.LL.LLLLLLL.LLLL.LLLLLLLLL.L.LLLLLLLLLLLLLLL.LLLL.LLLL.LLLLLLL.LLLLL.LLL..LLLLLLL.LLL",
            "LLLLLLLLL.LLLLLLL.LLLL.LLLLL.LLLLL.LLLLLL.LLLLLL.L.L.LLLLLLLLLL.LLLLLL.LLL.LLLLLLLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLL.LLLL.LLLLL.LLLLL.LLLLLL.LLLLLLLL.LLLLL.LLL.LLLLLLLLLLLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLL.LLLL.LLL.L.LLLLLLLLLLLL.LLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLLLLL.L.LLLLL..LLLL.LLLLLL.LLLLLLL..LLLLLL.LLLLLLLLLL.LLLLLLLLLL.LLLLLLLLLLL",
            "L..L.LLLL.LLL..L...LLL.LL.LLL...L.L.......L.L.....L...LL.LL..L.LL..LL....L......LLLL.......",
            "L..LLLLL.LLLLLLLL.L.LLLLL.LL.LLLLL.LLLLLL.LLLLLLLL..LLLLLLLLLLLLL.LL.LLLLL.LLLLLLLL..LLLLLL",
            "LLLLLLLLL.LLLLLLLLLLLLLLLLLL.LLLL.LLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLL.LLLLLLLLLLL",
            "LLLLLLLLL.L.LLLLL.LLLLLLLLLL.LLLLL.LLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLL.LLL.L.LL.L.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLL.LLLL.LLLLL.LLLLLLLLLLLL.LLLLLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLLL.LLLLLLLLLL.",
            "LLLLLLLLL.LLLLLLLLLLLLLLLLLL.LLLL.LLL.LLL.LLLLLLLL.LLLLLLLLLLLLLLLLL.LLLLL..LLL.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLL..LLLL.LLLLL.LLLLL.LLLL.L.LLLLLLLL.LLLLLLLLL.LLLLLLLLLLLLL.LLLL.LLLLLL..LLL",
            "LLLLLLL.L.L.LLLLLLLL.LLLLLLL.LLLLL.LLLLLL.LLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLL.L..L.LLLLLLL.LLL",
            "LLLLLLLLL.LLLLLLL.LLLL.LLLLLLLLLLLLLLLLLL.LL..LLLLLLLLLLLLLL.LLLLLLLLLLLLL..LLLLLL.LLLLLLLL",
            "......L...L..L..L.....LLL.L.L..L..LL..............L....L.LL.......L..L....L..........LLL...",
            "LLLLLLLLL.LLLLLLL.LLLL.LLLLLLLLLL..LLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLL.L.LL.LLLL..LLLLLLLLL.",
            "LLLLLLLLL.LLLL.LL..LLL.LLLLL..LLLLL.LLLLLLLL.LLL.L.LLLLLLLLLLLLL.LLL.LLLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLL.LLLL.LLLLLLLLLLL.LLLL.LL.LLLLLLLLLLLLLLL.LLLL.LLLL.LLLLL.LLLL.LLLLLLLLLLL",
            "LLLLLLLLLLLLLLLLLLLLLL.LLLLLL.LLLL.LLLLLLLLLLLLLLL.LLLLLLLLL.LLLLL.LLLL.LLLLLLL.LLLLLLLLLLL",
            "LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLL.LLLLLL.L.LLLLLLLLLL.LLLLLLLLLL.L.LLLL.LLLL.LLLLLL",
            "LLLLLLLLL..LLLLLL.LLLL.LLLLL.LLLLLLLLLLL..LLLLLLLLLLLLLLLLLL.LLLLLLL.LL.LL.LLLLLLLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLLLLLLL.LLLLLLLLLLLLL.LLLLLLLLLLLLL.LLLLLLLLL.LLLLLLLLLLLLL..LLL.LLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLL.LLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLLLLLLLLLLLLLLLLL",
            "LLLLLLLLL.LLLLLLL.LLLL.LLLLL.LLLLL.LLLLLL.LLLLLLLLLLLLLLLLL..LLLLL.L.LLLLL.LLLLLLLLLLLLLLLL",
            "LLLLLLLLLLLLLLLLL.LLLL.LLLLL.LLL.L.LLLLLL.LLLLLLLL...LLLLLLLLLLLLLLL.LLLLL.LLL..LLLL.LLLLLL",
        };
    }
}
