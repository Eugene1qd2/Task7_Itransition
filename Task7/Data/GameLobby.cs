namespace Task7.Data
{
    public enum GameType
    {
        TicTacToe,
        BiggerTicTacToe,
        CowBull,
    }

    public class GameLobby
    {
        public UserConnection Owner { get; private set; }
        public string Id { get; set; }
        public GameType Game { get; set; }
        public List<string> memberNames { get; set; }
    }
}
