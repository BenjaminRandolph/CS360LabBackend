namespace Lab_E_Commerce_Website_API.Models
{
    // The model for an item listing
    // This is how we access the data in the database about the items currently listed or previously listed on the website
    public class ItemListing
    {
        public int id { get; set; }

        public int ownerid { get; set; }

        public string? name { get; set; }

        public string? description { get; set; }

        public double price { get; set; }

        public string? category { get; set; }

        public DateTime dateofposting { get; set; }
    }
}
