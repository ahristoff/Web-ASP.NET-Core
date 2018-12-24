using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Models.Chat;

namespace SignalRChat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        //1 var samo authorize users
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("NewMessage", new Message  //izvikva metoda NewMessage ot js
            {
                User = this.Context.User.Identity.Name,
                Text = message,
            });
        }

        //2 var - za vsjakakvi users(dori i anonimus)
        //public async Task Send(string user, string message)
        //{
        //    await this.Clients.All.SendAsync("NewMessage", new Message  //izvikva metoda NewMessage ot js
        //    {
        //        User = user,
        //        Text = message,
        //    });
        //}
    }
}
