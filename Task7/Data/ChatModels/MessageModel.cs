namespace Task7.Data.ChatModels
{
    public class MessageModel
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
        public string SenderColor { get; set; }    
        public MessageModel()
        {
            Id=Guid.NewGuid().ToString();
        }
    }
}
