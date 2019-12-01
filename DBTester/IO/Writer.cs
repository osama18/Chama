using System;
using System.Collections.Generic;
using System.Text;

namespace DBTester.IO
{
    public class Writer:IWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
