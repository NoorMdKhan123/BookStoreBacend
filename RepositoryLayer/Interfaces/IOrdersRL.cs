using CommonLayer.Model;

using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IOrdersRL
    {
        OrderResModel AddingOrderDetails(OrderModel model, long userId);

        List<GetAllOrderData> GetAllCartData(long userId);
    }
}
