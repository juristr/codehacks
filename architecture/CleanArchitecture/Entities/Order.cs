using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Person Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
