using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Task7.Data.Enums;


namespace Task7.Data
{
    
    public class GameLobbyModel
    {
        public UserConnection Owner { get; set; }
        public UserConnection Opponent { get; set; }
        public string Id { get; set; }
        public GameType Game { get; set; }

        public GameState GameState { get; set; }

        public GameLobbyModel()
        {
            Id = Guid.NewGuid().ToString();
            Opponent = new UserConnection();
        }
        public GameLobbyModel(string Id)
        {
            this.Id = Id;
            Opponent = new UserConnection();
        }
    }
}
