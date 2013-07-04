﻿using System;
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
    public class PlaceOrder
    {

        public PlaceOrder(OrderRequestModel orderRequestModel)
        {
            /*
             * TODO:
             * - I'd need to take a reference to an outside boundary (i.e. a Repository)
             * which then saves it back to the DB
             * - What about the entities?? What kind of role do they play?
             */
        }

    }
}