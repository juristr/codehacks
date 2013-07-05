using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transversal;

namespace CleanArchitecture.Models
{
    public class OrderDto
    {
        public OrderDto(OrderResultModel resultModel = null)
        {
            //TODO: if resultModel != null, then map it to the properties
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<int> OrderItemsId { get; set; }

    }
}