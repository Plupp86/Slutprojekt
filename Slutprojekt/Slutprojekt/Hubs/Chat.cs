using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Slutprojekt.Hubs
{

    public class Chat : Hub
    {
		private static readonly ConcurrentBag<ChatUser> chatters = new ConcurrentBag<ChatUser>();

		public Task Send(string message)
		{ 
			return Clients.All.InvokeAsync("onSend", message);
		}

		public Task PrivateChat(string message, string receiverId)
		{
			return Clients.Group(receiverId).InvokeAsync("onSend", message);
		}

		public override Task OnConnectedAsync()
		{

			return base.OnConnectedAsync();
		}

		public Task RegisterChatUser(string userName)
		{
			var chatUser = chatters?.FirstOrDefault(x => x.UserName == userName);
			if (chatUser == null)
			{
				chatUser = new ChatUser { ConnectionId = Context.ConnectionId, UserName = userName };
				if (!chatters.Any(j => j.UserName == userName))
				{
					chatters.Add(chatUser);
				}
			return Clients.All.InvokeAsync("newChatUser", userName);
			}

			return null;

			//this.OnRegisterationComplete(Context.ConnectionId);
		}

		public Task GetOnlineUsers()
		{
			return Clients.All.InvokeAsync("listOfChatUsers", chatters);
		}

	}
}
