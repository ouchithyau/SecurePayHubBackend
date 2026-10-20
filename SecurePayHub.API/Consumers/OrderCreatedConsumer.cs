using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecurePayHub.API.Hubs;
using SecurePayHub.Application.Contracts;
using System.Threading.Tasks;

namespace SecurePayHub.API.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger;
        private readonly IHubContext<NotificationHub> _hubContext;

        public OrderCreatedConsumer(
            ILogger<OrderCreatedConsumer> logger,
            IHubContext<NotificationHub> hubContext)

        {
            _logger = logger;
            _hubContext = hubContext;
        }
        

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;

           
            _logger.LogInformation("🔔 [RabbitMQ] Order Received! ID: {OrderId}, Total: {Price}", message.OrderId, message.TotalPrice);

            await _hubContext.Clients.All.SendAsync("ReceiveOrderNotification", new
            {
                OrderId = message.OrderId,
                UserId = message.UserId,
                TotalPrice = message.TotalPrice,
                Message = $"Order #{message.OrderId} සාර්ථකව සැකසුණා! මුළු මුදල: Rs. {message.TotalPrice}",
                CreatedAt = message.CreatedAt
            });
            await Task.CompletedTask;
        }
    }   
}