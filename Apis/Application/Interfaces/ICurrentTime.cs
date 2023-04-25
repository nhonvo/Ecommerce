namespace Application.Interfaces
{
    public interface ICurrentTime
    {
        /// <summary>
        /// Gets the current time. This is used to determine when the user is informed
        /// </summary>
        public DateTime GetCurrentTime();
    }
}
