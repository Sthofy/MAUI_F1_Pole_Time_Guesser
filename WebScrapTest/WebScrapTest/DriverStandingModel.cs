using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapTest
{
    public class DriverStandingModel
    {
        public int Position { get; set; }
        public string? DriverName { get; set; }
        public string? Team { get; set; }
        public int Points { get; set; }

        public override string ToString()
        {
            return $"{Position} {DriverName} {Team} {Points}";
        }
    }
}
