using AdventOfCode.Days;
using Xunit;

namespace AdventOfTest
{
    public class Day08Tests
    {
        [Fact]
        public void Part1()
        {
            var valid = Day08.Part1();

            Assert.Equal(1928, valid);
        }

        [Fact]
        public void Part2()
        {
            var valid = Day08.Part2();

            Assert.Equal(1319, valid);
        }
    }
}
