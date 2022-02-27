using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IOrdersBL
    {
        OrderResModel AddingOrderDetails(OrderModel model, long userId);

        List<GetAllOrderData> GetAllCartData(long userId);
    }
}
