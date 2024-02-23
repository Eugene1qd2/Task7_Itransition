using Microsoft.AspNetCore.SignalR;
using Task7.Data.ChatModels;

namespace Task7.Data
{
    public static class UserHandler
    {
        public static HashSet<string> ConnectedIds = new HashSet<string>();
    }

    public sealed class GameHub : Hub
    {

        public async override Task OnConnectedAsync()
        {
            UserHandler.ConnectedIds.Add(this.Context.ConnectionId);
            await this.Clients.All.SendAsync("GetCurrentOnline", UserHandler.ConnectedIds.Count);
            //return base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            UserHandler.ConnectedIds.Remove(this.Context.ConnectionId);
            await this.Clients.All.SendAsync("GetCurrentOnline", UserHandler.ConnectedIds.Count);
            //return base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGameLobbyAsync(UserConnection userConnection)
        {
            await this.Groups.AddToGroupAsync(Context.ConnectionId, userConnection.CurrentLobby.Id);
            await Clients.Group(userConnection.CurrentLobby.Id).SendAsync("RecieveMessage", userConnection.Username, $"{userConnection.Username} connected to game!");
        }

        public async Task SendLobbyMessage(MessageModel message)
        {
            await Clients.All.SendAsync("GetLobbyMessage", message);
        }

        public async Task SendGroupMessage(string groupId, MessageModel message)
        {
            await Clients.Group(groupId).SendAsync("GetGroupMessage", message);
        }
    }
}
