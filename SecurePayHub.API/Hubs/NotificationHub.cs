using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SecurePayHub.API.Hubs
{
    public class NotificationHub : Hub
    {
       
        public async Task JoinUserGroup(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId}");
        }
    }
}