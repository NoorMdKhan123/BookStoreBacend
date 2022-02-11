using BusinessLayer.Interfaces;
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

    }
}
