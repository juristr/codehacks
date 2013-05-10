using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using AutofixtureUnitTests.Application;

namespace AutofixtureUnitTests
{
    [TestClass]
    public class AutoFixtureDrivenTests
    {

        [TestMethod]
        public void ShouldChangeThePersonsAge()
        {
            //arrange
            Fixture fixture = new Fixture();
            Person aPerson = fixture.Create<Person>();
            MyService service = new MyService(); //just a dummy service class

            //act
            service.SetPersonAge(aPerson, 28);

            //assert
            Assert.AreEqual(28, aPerson.Age);
        }

        [TestMethod]
        [ExpectedException(typeof(ChildNotPresentException))]
        public void ShouldThrowAChildNotPresentExceptionWhenNoChildIsPresent()
        {
            //arrange
            Fixture fixture = new Fixture();
            var aPerson = fixture.Build<Person>()
                            .Without(p => p.Children)
                            .Create();
            MyService service = new MyService(); //just a dummy service class

            //act
            service.SavePerson(aPerson);
        }

    }
}
