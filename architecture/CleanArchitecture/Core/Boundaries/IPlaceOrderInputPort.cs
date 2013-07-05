using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transversal;

namespace Core.Boundaries
{
    public interface IPlaceOrderInputPort
    {

        void PlaceOrder(OrderRequestModel orderRequestModel, IPlaceOrderOutputPort placeOrderOutput);

    }
}
