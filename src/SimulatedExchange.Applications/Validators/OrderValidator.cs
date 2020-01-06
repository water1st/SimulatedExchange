using SimulatedExchange.Applications.DTO;
using SimulatedExchange.Domain.Exceptions;
using System;
using System.ComponentModel;

namespace SimulatedExchange.Applications.Validators
{
    public class OrderValidator : IOrderValidator
    {
        public void VerifyId(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
        }

        public void VerifyOrderInfo(OrderInfo orderInfo)
        {
            if (orderInfo is null)
            {
                throw new ArgumentNullException(nameof(orderInfo));
            }
            if (orderInfo.Type < 0)
            {
                throw new InvalidEnumArgumentException("无效订单类型");
            }
            if (orderInfo.Exchange < 0)
            {
                throw new InvalidEnumArgumentException("无效交易所类型");
            }
            if (orderInfo.Amount <= 0)
            {
                throw new ArgumentOutOfRangeException("数量不大于0");
            }
            if (orderInfo.Price <= 0)
            {
                throw new ArgumentOutOfRangeException("价格不大于0");
            }
            VerifyPairSymbols(orderInfo.PairSymbols);
        }

        public void VerifyPairSymbols(string pairSymbols)
        {
            if (string.IsNullOrWhiteSpace(pairSymbols) || string.IsNullOrEmpty(pairSymbols))
            {
                throw new ArgumentNullException(nameof(pairSymbols));
            }
            if (!pairSymbols.Contains("-"))
            {
                throw new InvalidPairSymbolsExcption("币对必须使用'-'进行分割");
            }
            var temp = pairSymbols.Split('-');
            if (temp.Length != 2)
            {
                throw new InvalidPairSymbolsExcption("无效币对");
            }
        }
    }
}
