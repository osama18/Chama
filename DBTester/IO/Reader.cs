using System;
using System.Collections.Generic;
using System.Text;

namespace DBTester.IO
{
    public class Reader : IReader
    {
        public string ReadMessage()
        {
            return Console.ReadLine();
        }
    }
}
