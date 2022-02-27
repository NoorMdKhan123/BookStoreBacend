using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrdersBL :IOrdersBL
    {
        IOrdersRL _ordersRL;
        public  OrdersBL(IOrdersRL ordersRL)
        {
            _ordersRL = ordersRL;
        }
        public OrderResModel AddingOrderDetails(OrderModel model, long userId)
        {
            return this._ordersRL.AddingOrderDetails(model, userId);
        }

        public List<GetAllOrderData> GetAllCartData(long userId)
        {
            return _ordersRL.GetAllCartData(userId);
        }
    }
}
