using AdventOfCode.Days;
using Xunit;

namespace AdventOfTest
{
    public class Day07Tests
    {
        [Fact]
        public void Part1()
        {
            var valid = Day07.Part1();

            Assert.Equal(126, valid);
        }

        [Fact]
        public void Part2()
        {
            var valid = Day07.Part2();

            Assert.Equal(220149, valid);
        }
    }
}
