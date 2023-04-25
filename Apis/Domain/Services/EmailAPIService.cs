using System.Text;
using Domain.Aggregate;
using Domain.Interfaces;
using Newtonsoft.Json;

namespace Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient client;
        private readonly string _accessToken;

        public EmailService(IHttpClientFactory httpClientFactory, AppConfiguration configuration)
        {
            client = httpClientFactory.CreateClient("OutLook"); ;
            _accessToken = configuration.Token.AccessToken;
        }
        public async Task<HttpResponseMessage> SendEmailAsync(SendMailRequest request)
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("me/sendMail", httpContent);
            return response;
        }
    }
}