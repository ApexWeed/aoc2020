using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public interface IDay
    {
        int Day { get; }
        void RunPart1(bool silent);
        void RunPart2(bool silent);

        void RunPart3(Action<Action> timer)
        { }
    }
}
