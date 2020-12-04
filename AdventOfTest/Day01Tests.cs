using AdventOfCode.Days;
using Xunit;

namespace AdventOfTest
{
    public class Day01Tests
    {
        [Fact]
        public void Part1()
        {
            var (lower, upper) = Day01.Part1();

            Assert.Equal(2020, lower + upper);

            Assert.All(new [] {lower, upper}, i => Assert.Contains(Day01.Input, v => v == i));
        }

        [Fact]
        public void Part2()
        {
            var (lower, mid, upper) = Day01.Part2();

            Assert.Equal(2020, lower + mid + upper);

            Assert.All(new [] {lower, mid, upper}, i => Assert.Contains(Day01.Input, v => v == i));
        }
    }
}
