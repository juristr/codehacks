using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutofixtureUnitTests.Application
{
    public class Person
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }

        public IEnumerable<Child> Children { get; set; }
    }
}
