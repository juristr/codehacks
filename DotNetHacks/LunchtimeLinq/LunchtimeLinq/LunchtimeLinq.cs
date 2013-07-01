using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LunchtimeLinq
{
    [TestClass]
    public class LunchtimeLinq
    {
        /*
         *  Exercise 1
         *  -------------
         *  Take the following string "Davis, Clyne, Fonte, Hooiveld, Shaw, Davis, Schneiderlin,
         *  Cork, Lallana, Rodriguez, Lambert" and give each player a shirt number, 
         *  starting from 1, to create a string of the form: 
         *  "1. Davis, 2. Clyne, 3. Fonte" etc
         */
        [TestMethod]
        public void Ex1_ShouldNumberPlayers()
        {
            var result =
                String.Join(", ",
                    "Davis, Clyne, Fonte, Hooiveld, Shaw, Davis, Schneiderlin, Cork, Lallana, Rodriguez, Lambert"
                    .Split(',')
                    .Select((item, index) => index + 1 + "." + item.Trim())
                    .ToArray());


            Assert.AreEqual("1.Davis, 2.Clyne, 3.Fonte, 4.Hooiveld, 5.Shaw, 6.Davis, 7.Schneiderlin, 8.Cork, 9.Lallana, 10.Rodriguez, 11.Lambert", result);
        }

        /*
         *  Exercise 2
         *  -------------
         *  Take the following string 
         *  "Jason Puncheon, 26/06/1986; Jos Hooiveld, 22/04/1983; Kelvin Davis, 29/09/1976; Luke Shaw, 12/07/1995; Gaston Ramirez, 02/12/1990; Adam Lallana, 10/05/1988" 
         *  and turn it into an IEnumerable of players in order of age (bonus to show the age 
         *  in the output).
         */
        [TestMethod]
        public void Ex2_ShouldOrderThePlayersAccordingToTheirAge()
        {
            var result =
                "Jason Puncheon, 26/06/1986; Jos Hooiveld, 22/04/1983; Kelvin Davis, 29/09/1976; Luke Shaw, 12/07/1995; Gaston Ramirez, 02/12/1990; Adam Lallana, 10/05/1988"
                .Split(';')
                .Select(p => p.Split(','))
                .Select(p => new { Name = p[0].Trim(), DateOfBirth = DateTime.Parse(p[1].Trim()), Age = DateTime.Parse(p[1].Trim()).GetAge() })
                .OrderByDescending(p => p.Age);

            Assert.AreEqual("Kelvin Davis", result.ElementAt(0).Name);
            Assert.AreEqual("Luke Shaw", result.ElementAt(result.Count() - 1).Name);
        }

        [TestMethod]
        public void Ex2_ShouldCorrectlyCalculateTheAgeOfAPerson()
        {
            Assert.AreEqual(28, new DateTime(1985, 5, 15).GetAge());
            Assert.AreEqual(28, new DateTime(1985, 1, 1).GetAge());
        }

    }

    public static class AgeDateExtension
    {
        public static int GetAge(this DateTime dateTime)
        {
            var difference = DateTime.Today;
            return difference.Year - dateTime.Year;
        }
    }
}
