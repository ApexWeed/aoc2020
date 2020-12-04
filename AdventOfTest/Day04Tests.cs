using AdventOfCode.Days;
using Xunit;

namespace AdventOfTest
{
    public class Day04Tests
    {
        [Fact]
        public void Part1()
        {
            var valid = Day04.Part1();

            Assert.Equal(202, valid);
        }

        [Fact]
        public void Part2()
        {
            var valid = Day04.Part2();

            Assert.Equal(137, valid);
        }
    }
}
