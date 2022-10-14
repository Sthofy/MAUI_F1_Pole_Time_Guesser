using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapTest
{
    public class DriverModel
    {
        public int DriverNumber { get; set; }
        public string? LongName { get; set; }
        public string? ShortName { get; set; }
        public string? Team { get; set; }
        public int Position { get; set; }
        public int Points { get; set; }

        public DriverModel()
        {
        }

    }
}
