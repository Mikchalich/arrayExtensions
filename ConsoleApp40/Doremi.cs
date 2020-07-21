using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp40
{
    class Doremi
    {
        public int Count { get; set; }
        private Random random = new Random();
        public Doremi()
        {
            Count = random.Next(0, 12);
        }

    }
}
