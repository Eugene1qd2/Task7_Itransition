namespace Task7.Data.ChatModels
{
    public class ChatModel
    {
        public List<MessageModel> Messages { get; set; }
        public string CurrentMessage { get; set; }
        public int CurrentOnline { get; set; }
        public ChatModel() 
        {
            Messages = new List<MessageModel>();
            CurrentMessage= string.Empty;
        }
        public void AppendMessage(string Sender,string Message,string SenderColor="#000000")
        {
            Messages.Add(new MessageModel {  Sender = Sender, Message = Message, SenderColor = SenderColor });
        }
        public void AppendMessage(MessageModel message)
        {
            Messages.Add(message);
        }
        public void Clear()
        {
            Messages.Clear();
        }
    }
}
