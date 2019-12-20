using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedExchange.Applications.Services
{
    public interface ITestServices
    {
        Task TransactionAsync(string id, decimal amount, decimal price);
    }
}
