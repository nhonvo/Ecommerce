namespace Application.ViewModels.Book
{
    public class SearchRequest
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public decimal? StartPrice { get; set; }
        public decimal? EndPrice { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string? Genre { get; set; }
    }
}