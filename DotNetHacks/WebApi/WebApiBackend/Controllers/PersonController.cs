using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiBackend.Models;

namespace WebApiBackend.Controllers
{
    public class PersonController : ApiController
    {

        public IEnumerable<Person> GetAll()
        {
            return new List<Person>{
                new Person(){
                    Id = 1,
                    Firstname = "Juri",
                    Lastname = "Strumpflohner"
                },
                new Person(){
                    Id = 2,
                    Firstname = "Steffi",
                    Lastname = "Franchi"
                },
                new Person(){
                    Id = 2,
                    Firstname = "Steffi",
                    Lastname = "Franchi"
                }
            };
        }

        public Person GetById(int id)
        {
            return new Person()
            {
                Id = id,
                Firstname = "Juri"
            };
        }

        [HttpPost]
        public Person SavePerson(Person person)
        {
            person.Id = 123;
            return person;
        }

    }

}
