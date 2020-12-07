using AdventOfCode.Days;
using Xunit;

namespace AdventOfTest
{
    public class Day06Tests
    {
        [Fact]
        public void Part1()
        {
            var valid = Day06.Part1();

            Assert.Equal(6633, valid);
        }

        [Fact]
        public void Part2()
        {
            var valid = Day06.Part2();

            Assert.Equal(3202, valid);
        }
    }
}
