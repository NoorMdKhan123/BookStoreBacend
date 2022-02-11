using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class OrdersRL : IOrdersRL
    {
        IConfiguration _config;
        public OrdersRL(IConfiguration config)
        {
            _config = config;
        }

    }
}
