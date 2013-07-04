using Core;
using Core.Boundaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transversal;

namespace Interactors
{
    /// <summary>
    /// Interactor
    /// This represets the action of placing a new order to the system
    /// </summary>
    public class PlaceOrder : IPlaceOrder
    {
        private readonly IOrderGateway orderGateway;
        private readonly OrderRequestModel orderRequestModel;

        public PlaceOrder(OrderRequestModel orderRequestModel, IOrderGateway orderGateway)
        {
            /*
             * TODO:
             * - I'd need to take a reference to an outside boundary (i.e. a Repository)
             * which then saves it back to the DB
             * - What about the entities?? What kind of role do they play?
             */

            this.orderGateway = orderGateway;
            this.orderRequestModel = orderRequestModel;
        }


        public OrderResultModel PlaceOrder(OrderRequestModel orderRequestModel)
        {
            throw new NotImplementedException();
        }
    }
}
