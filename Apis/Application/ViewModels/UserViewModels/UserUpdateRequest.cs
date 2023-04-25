namespace Application.ViewModels.UserViewModels
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
}
