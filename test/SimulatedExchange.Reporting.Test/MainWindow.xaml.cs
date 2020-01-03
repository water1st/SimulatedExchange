using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Text.Json;
using System.Windows;

namespace SimulatedExchange.Reporting.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection connection;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var address = addressTextBox.Text;
            if (string.IsNullOrWhiteSpace(address))
            {
                PrintMessage("服务器地址不能为空");
                return;
            }
            try
            {
                connection = new HubConnectionBuilder()
                .WithUrl(address)
                .Build();


                connection.On<OrderState>("New", WriteToConsole);
                connection.On<OrderState>("PartialDeal", WriteToConsole);
                connection.On<OrderState>("FullDeal", WriteToConsole);
                connection.On<OrderState>("PartialCancel", WriteToConsole);
                connection.On<OrderState>("FullCanceled", WriteToConsole);

                connection.StartAsync();
            }
            catch (Exception ex)
            {
                PrintMessage(ex.Message);
            }

        }

        protected override void OnClosed(EventArgs e)
        {
            connection.StopAsync();
            base.OnClosed(e);
        }

        private void WriteToConsole(OrderState state) => PrintMessage(JsonSerializer.Serialize(state));

        private void PrintMessage(string message)
        {
            printTextBox.Dispatcher.Invoke(() =>
            {
                printTextBox.Text += message + "\r\n";
            });
        }
    }
}
