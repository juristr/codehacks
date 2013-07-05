using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transversal;

namespace Core.Boundaries
{
    public interface IPlaceOrderOutputPort
    {

        void DisplayError(string errorMessage);

        void DisplaySuccess(OrderResultModel orderRequestModel);

    }
}
