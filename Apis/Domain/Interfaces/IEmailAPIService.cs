
using Domain.Aggregate;

namespace Domain.Interfaces
{
    public interface IEmailService
    {
        Task<HttpResponseMessage> SendEmailAsync(SendMailRequest request);
    }
}