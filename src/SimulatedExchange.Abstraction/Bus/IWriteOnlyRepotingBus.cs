using System.Threading.Tasks;

namespace SimulatedExchange.Bus
{
    public interface IWriteOnlyRepotingBus
    {
        Task Write<TWriterParameter>(TWriterParameter parameter) where TWriterParameter : class;
    }
}
