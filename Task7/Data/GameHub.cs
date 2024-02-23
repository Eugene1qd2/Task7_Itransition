using Microsoft.AspNetCore.SignalR;

namespace Task7.Data
{
    public sealed class GameHub:Hub
    {
        public async override Task OnConnectedAsync()
        {
            await this.Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            //return base.OnConnectedAsync();
        }

        public async Task JoinGameLobbyAsync(UserConnection userConnection)
        {
            await this.Groups.AddToGroupAsync(Context.ConnectionId, userConnection.CurrentLobby.Id);
            await Clients.Group(userConnection.CurrentLobby.Id).SendAsync("RecieveMessage", userConnection.Username, $"{userConnection.Username} connected to game!");
        }
    }
}
