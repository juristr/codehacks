using CleanArchitecture.Models;
using Core.Boundaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Transversal;

namespace CleanArchitecture.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IPlaceOrder placeOrderIntent;


        public OrdersController(IPlaceOrder placeOrderIntent)
        {
            this.placeOrderIntent = placeOrderIntent;
        }

        //// GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        public void Post([FromBody]OrderDto order)
        {
            var orderRequest = new OrderRequestModel();
            var orderResult = placeOrderIntent.PlaceOrder(orderRequest);

            //return orderResult;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}