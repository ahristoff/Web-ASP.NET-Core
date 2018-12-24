
namespace Camera.Web.Hubs
{
    using Camera.Web.Models.Chat;
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;

    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.InvokeAsync("NewMessage", new Message
            {
                User = this.Context.User.Identity.Name,
                Text = message,
            });
        }
    }
}
