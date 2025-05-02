namespace Lab_E_Commerce_Website_API.Models
{
    // The model for an item listing
    // This is how we access the data in the database about the items currently listed or previously listed on the website
    public class Transaction
    {
        public int ID { get; set; }

        public int PosterID { get; set; }

        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public double AmountPaid { get; set; }

        public int AmountOfProduct { get; set; }

        public string? Category { get; set; }

        public int PurchaserID { get; set; }

        public DateTime DateOfPurchase { get; set; }

        public DateTime DateOfPosting { get; set; }
    }
}
