using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutofixtureUnitTests.Application
{
    public class MyService
    {

        public void SetPersonAge(Person person, int age)
        {
            person.Age = age;   
        }

        public void SavePerson(Person person)
        {
            if (person.Children == null)
            {
                throw new ChildNotPresentException();
            }
        }

    }
}
