using AdventOfCode.Days;
using Xunit;

namespace AdventOfTest
{
    public class Day02Tests
    {
        [Fact]
        public void Part1()
        {
            var valid = Day02.Part1();

            Assert.Equal(393, valid);
        }

        [Fact]
        public void Part2()
        {
            var valid = Day02.Part2();

            Assert.Equal(690, valid);
        }
    }
}
