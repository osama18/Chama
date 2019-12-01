using System;
using System.Collections.Generic;
using System.Text;

namespace DBTester
{
    public interface ICommad
    {
        string Name { get; }
        string Key { get;  }
        void Execute();
    }
}
