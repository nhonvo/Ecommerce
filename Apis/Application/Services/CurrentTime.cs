using Application.Interfaces;

namespace Application.Services
{
    public class CurrentTime : ICurrentTime
    {
        /// <summary>
        /// Gets the current time. This is used to determine when the user is informed of an error and can be used to determine when the error is
        /// </summary>
        public DateTime GetCurrentTime() => DateTime.UtcNow;
    }
}

