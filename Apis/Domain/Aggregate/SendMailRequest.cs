namespace Domain.Aggregate
{
    public class Body
    {
        public string contentType { get; set; } = "HTML";
        public string content { get; set; }
    }

    public class EmailAddress
    {
        public string address { get; set; } = "fptvttnhon2017@gmail.com";
    }

    public class Message
    {
        public string subject { get; set; } = "Message from Book Store";
        public Body body { get; set; } = new Body();
        public List<ToRecipient> toRecipients { get; set; } = new List<ToRecipient> { };
    }

    public class SendMailRequest
    {
        public Message message { get; set; } = new Message();
    }

    public class ToRecipient
    {
        public EmailAddress emailAddress { get; set; } = new EmailAddress();
    }

}
