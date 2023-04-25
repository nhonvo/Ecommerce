namespace Domain.Entities
{
    /// <summary>
    /// book 1 - n cart 
    /// book 1 - n wish list
    /// book 1 - n order detail
    /// book 1 - n review
    /// </summary>
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
        public string Genre { get; set; }
        public float AverageRating { get; set; }
        public int RatingCount { get; set; }
        public int TotalRating { get; set; }
        public DateTime PublicationDate { get; set; }
    
    }
}
