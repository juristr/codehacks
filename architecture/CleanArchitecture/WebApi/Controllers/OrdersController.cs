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
        private readonly IPlaceOrderInputPort placeOrderIntent;

        public OrdersController(IPlaceOrderInputPort placeOrderIntent)
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
        public OrderDto Post([FromBody]OrderDto order)
        {
            var orderRequest = new OrderRequestModel();
            var orderWrapper = new PlaceOrderDeliveryMechanism();
            placeOrderIntent.PlaceOrder(orderRequest, orderWrapper);

            if (orderWrapper.IsError)
            {//obviously use a more specific exception
                throw new ApplicationException(orderWrapper.ErrorMessage);
            }else{
                return new OrderDto(orderWrapper.ResultModel);
            }
        }

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }

    public class PlaceOrderDeliveryMechanism : IPlaceOrderOutputPort
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }

        public OrderResultModel ResultModel { get; set; }

        public void DisplayError(string errorMessage)
        {
            IsError = true;
            ErrorMessage = errorMessage;
        }

        public void DisplaySuccess(OrderResultModel orderRequestModel)
        {
            IsError = false;
        }
    }
}