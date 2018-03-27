using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slutprojekt.Hubs
{
    public class Chat : Hub
    {
		public Task Send(string message)
		{ 
			return Clients.All.InvokeAsync("onSend", message);
		}

		public Task PrivateChat(string message, string receiverId)
		{
			return Clients.Group(receiverId).InvokeAsync("onSend", message);
		}

		public void method()
		{
			//var id = Clients.Client(Context.ConnectionId);
			var id = Context.ConnectionId;
		}
    }
}
