using ItemModel_Nugget;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workerservice_Consumer.Broker;
using Workerservice_Consumer.Consumer;
using Workerservice_Consumer.Metrics;

namespace Workerservice_Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly MessageConsumer _messageConsumer;
        private readonly MetricsRegistry _metricsRegistry;
        private readonly Serilog.Core.Logger _receivedMessageounter;
        private readonly RabbitMQOptions _rabbitMQOptions;
        private IConfiguration _configuration;
        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _rabbitMQOptions = new RabbitMQOptions(_configuration);
            _messageConsumer = new MessageConsumer(_rabbitMQOptions);
            _metricsRegistry = new MetricsRegistry(_rabbitMQOptions);
            _receivedMessageounter = _metricsRegistry._receivedMessage();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            try
            {
            

             while (!stoppingToken.IsCancellationRequested)
            {
               
                    Console.WriteLine("Started");

                    Item response = _messageConsumer.ReceiveMessage();
                    if (response != null)
                    {
                        Console.WriteLine("Item Name: " + response.Name);
                        _logger.LogInformation("Item received Successfully at: {time}", DateTimeOffset.Now);
                        var counter = _receivedMessageounter.CountOperation("counter", "operation(s)", true, LogEventLevel.Information);
                        counter.Increment();
                    }
                    await Task.Delay(1000, stoppingToken);
            }

            }
            catch (Exception ex)
            {

                Log.Fatal(ex, "An Error Occured.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
