﻿namespace SimulatedExchange.Domain.Orders
{
    public enum OrderStatus
    {
        //订单打开
        Opened = 0,
        //部分成交
        PartialTransaction = 1,
        //全部成交
        FullTransaction = 2,
        //撤单中
        Canceling = 3,
        //已撤单
        FullCanceled = 4,
        //部分撤单
        PartialCanceled = 5
    }
}
