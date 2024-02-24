using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Task7.Data.Enums;


namespace Task7.Data
{
    
    public class GameLobbyModel
    {
        public UserConnection Owner { get; set; }
        public string Id { get; set; }
        public GameType Game { get; set; }
        public List<string> memberNames { get; set; }

        public GameLobbyModel()
        {
            Id = Guid.NewGuid().ToString();
            memberNames = new List<string>();
        }
    }
}
