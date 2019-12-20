﻿using SimulatedExchange.Applications.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimulatedExchange.Applications.Services
{
    public interface IOrderServices
    {
        Task<OrderDetial> GetIdAsync(string id);
        Task<OrderList> GetList(int pageIndex, int PageSize);
        Task CreateNewOrder(OrderInfo orderInfo);
        Task CalcelOrderAsync(string id);
    }
}
