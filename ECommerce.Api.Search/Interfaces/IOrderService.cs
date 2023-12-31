﻿using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IOrderService
    {
        Task<(bool isSuccess, IEnumerable<Order>, string ErrorMessgae)> GetOrderAsync(int customerId);
    }
}
