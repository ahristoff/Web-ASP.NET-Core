namespace SignalRDemo.Hub
{
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;

    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.InvokeAsync("Send", message);           //send to all clients

            //await this.Clients.User("1").InvokeAsync("Send", message);   //send to client with this id

            //this.Clients.Group("dghdndgjhmjf");                          //send to group with this name

        }
    }
}
