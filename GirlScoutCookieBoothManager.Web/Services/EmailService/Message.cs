using MimeKit;

namespace GirlScoutCookieBoothManager.Web.Services.EmailService
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFileCollection Attachments { get; set; }
        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments) {
            var emailsList = to.FirstOrDefault().Split(',').Select(l => l.Trim()).ToList();
            To = new List<MailboxAddress>();
            To.AddRange(emailsList.Select(email => new MailboxAddress(email,email)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}
