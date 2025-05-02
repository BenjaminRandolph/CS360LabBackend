namespace Lab_E_Commerce_Website_API.Models
{
    // The model for an item listing
    // This is how we access the data in the database about the items currently listed or previously listed on the website
    public class ItemListing
    {
        public int ID { get; set; }

        public int OwnerID { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public string? Category { get; set; }

        public DateTime DateOfPosting { get; set; }
    }
}
