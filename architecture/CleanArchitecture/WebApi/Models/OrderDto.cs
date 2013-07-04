using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CleanArchitecture.Models
{
    public class OrderDto
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<int> OrderItemsId { get; set; }

    }
}