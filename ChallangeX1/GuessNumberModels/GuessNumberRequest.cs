using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallangeX1.GuessNumberModels
{
    public class GuessNumberRequest
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int GuessedNumber { get; set; }
    }
}
