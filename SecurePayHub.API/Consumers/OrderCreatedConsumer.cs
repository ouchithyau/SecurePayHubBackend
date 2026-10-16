using MassTransit;
using SecurePayHub.Application.Contracts;
using System.Threading.Tasks;

namespace SecurePayHub.API.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger;

        public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;

           
            _logger.LogInformation("🔔 [RabbitMQ] Order Received! ID: {OrderId}, Total: {Price}", message.OrderId, message.TotalPrice);

            await Task.CompletedTask;
        }
    }
}