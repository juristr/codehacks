using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transversal
{
    /// <summary>
    /// Created by the Delivery mechanism, passed through the boundaries
    /// over to the Interactors which manipulate it using the Entities and then
    /// create a <paramref name="OrderResultModel"/> which is again passed back through
    /// the boundaries to the delivery mechanism
    /// </summary>
    public class OrderRequestModel
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<int> OrderItemIds { get; set; }

    }
}
