using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SimulatedExchange.Reporting.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/api/TRADE_REPORT_CHANNEL")
                .Build();

            connection.On<OrderState>("New", WriteToConsole);
            connection.On<OrderState>("PartialDeal", WriteToConsole);
            connection.On<OrderState>("FullDeal", WriteToConsole);
            connection.On<OrderState>("PartialCancel", WriteToConsole);
            connection.On<OrderState>("FullCanceled", WriteToConsole);

            await connection.StartAsync();
            Console.WriteLine("按任意键退出");
            Console.ReadKey();
        }

        private static void WriteToConsole(OrderState state) => Console.WriteLine(JsonSerializer.Serialize(state));
    }
}
