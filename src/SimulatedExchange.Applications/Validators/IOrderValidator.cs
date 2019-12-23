using SimulatedExchange.Applications.DTO;

namespace SimulatedExchange.Applications.Validators
{
    public interface IOrderValidator
    {
        void VerifyOrderInfo(OrderInfo orderInfo);
        void VerifyPairSymbols(string pairSymbols);
        void VerifyId(string id);
    }
}
