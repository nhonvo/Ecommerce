namespace Application.Interfaces
{
    public interface IClaimsService
    {
        /// Gets the current user identifier. Note that this does not check if the user is logged in
        public Guid CurrentUserId { get; }
    }
}
