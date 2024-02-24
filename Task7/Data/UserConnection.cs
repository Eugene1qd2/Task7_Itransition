namespace Task7.Data
{


    public class UserConnection
    {
        public string Id { get; set; }
        public string Username {  get; set; }
        public GameLobbyModel CurrentLobby {  get; set; }
        public string ChatColor {  get; set; }

        public UserConnection()
        {
            ColorGenerator colorGenerator = new ColorGenerator();
            Id= Guid.NewGuid().ToString(); 
            ChatColor = string.Empty;
            ChatColor = colorGenerator.GenerateColor();
        }

    }
}
