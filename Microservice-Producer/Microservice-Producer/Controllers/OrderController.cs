using ItemModel_Nugget;
using Microservice_Producer.Metrics;
using Microservice_Producer.Producer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservice_Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<OrderController> _logger;
        private readonly IMetricsRegistry _metricsRegistry;
        private readonly Serilog.Core.Logger _receivedMessageounter;
        public OrderController(IMessageProducer messageProducer, ILogger<OrderController> logger, IMetricsRegistry metricsRegistry)
        {
            _messageProducer = messageProducer;
            _logger = logger;
            _metricsRegistry = metricsRegistry;
            _receivedMessageounter = _metricsRegistry._receivedMessage();
        }
        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Item item)
        {
            var counter = _receivedMessageounter.CountOperation("counter", "operation(s)", true, LogEventLevel.Information);
            counter.Increment();
            var response = _messageProducer.PublishMessage(item);
            _logger.LogInformation("Item Post Successfull at: {time}", DateTimeOffset.Now);
            return Ok(response);


            

        }
    }
}
