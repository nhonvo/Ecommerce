namespace Application.ViewModels.Book
{
    public class BookResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
        public float AverageRating { get; set; }
        public int RatingCount { get; set; }
        public int TotalRating { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Genre { get; set; }
    }
}