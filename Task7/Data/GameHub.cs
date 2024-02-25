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
            await Console.Out.WriteLineAsync(Context.ConnectionId);
            await Console.Out.WriteLineAsync("con");
            //return base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            UserHandler.ConnectedIds.Remove(this.Context.ConnectionId);
            await this.Clients.All.SendAsync("GetCurrentOnline", UserHandler.ConnectedIds.Count);
            //return base.OnDisconnectedAsync(exception);
            await Console.Out.WriteLineAsync(Context.ConnectionId);
            await Console.Out.WriteLineAsync("dis");

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


        //По запросу всех лобби, прошу юзеров дать мне их личные лобби
        public async Task GetLobbies()
        {
            await Clients.All.SendAsync("GetOwnLobby",Context.ConnectionId);
            await Clients.All.SendAsync("GetCurrentOnline", UserHandler.ConnectedIds.Count);
        }

        //Когда пользователи присылают мне их личные лобби, передаю их запросившему пользователю
        public async Task GiveHimLobby(GameLobbyModel lobbyModel,string destination)
        {
            await Clients.Client(destination).SendAsync("NewLobbyCreated", lobbyModel);
        }

        //создание и удаление лобби
        public async Task CreateNewLobby(GameLobbyModel lobbyModel)
        {
            await this.Groups.AddToGroupAsync(Context.ConnectionId, lobbyModel.Id);
            await Clients.All.SendAsync("NewLobbyCreated", lobbyModel);
        }

        public async Task CloseLobby(GameLobbyModel lobbyModel)
        {
            await Clients.Others.SendAsync("LobbyJustClosed", lobbyModel);
            await Clients.All.SendAsync("LobbyClosed", lobbyModel);
            Console.WriteLine("closed");
        }

        //общение между 2 пользователями комнаты
        public async Task ConnectToLobby(string Id,UserConnection user)
        {
            await Clients.Group(Id).SendAsync("OpponentAskForLobbyData", user,Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, Id);
        }
        public async Task SendLobbyDataResponse(string dest,GameLobbyModel lobbyModel)
        {
            await Clients.Group(lobbyModel.Id).SendAsync("RecieveLobbyDataResponse", lobbyModel);
        }
    }
}
