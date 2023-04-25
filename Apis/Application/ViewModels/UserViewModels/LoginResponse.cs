namespace Application.ViewModels.UserViewModels
{
    public class LoginResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public DateTime? ExpireDay { get; set; }
    }
}
