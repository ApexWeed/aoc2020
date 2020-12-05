using AdventOfCode.Days;
using Xunit;

namespace AdventOfTest
{
    public class Day05Tests
    {
        [Fact]
        public void Part1()
        {
            var valid = Day05.Part1();

            Assert.Equal(994, valid);
        }

        [Fact]
        public void Part2()
        {
            var valid = Day05.Part2();

            Assert.Equal(741, valid);
        }
    }
}
