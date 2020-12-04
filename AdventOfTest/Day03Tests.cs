using AdventOfCode.Days;
using Xunit;

namespace AdventOfTest
{
    public class Day03Tests
    {
        [Fact]
        public void Part1()
        {
            var valid = Day03.Part1();

            Assert.Equal(164, valid);
        }

        [Fact]
        public void Part2()
        {
            var valid = Day03.Part2();

            Assert.Equal(5007658656L, valid);
        }
    }
}
