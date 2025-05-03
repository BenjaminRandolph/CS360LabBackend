namespace Lab_E_Commerce_Website_API.Models
{
    // The model for an item listing
    // This is how we access the data in the database about the items currently listed or previously listed on the website
    public class Transaction
    {
        public int id { get; set; }

        public int posterid { get; set; }

        public string? productname { get; set; }

        public string? productdescription { get; set; }

        public double amountpaid { get; set; }

        public int amountofproduct { get; set; }

        public string? category { get; set; }

        public int purchaserid { get; set; }

        public DateTime dateofpurchase { get; set; }

        public DateTime dateofposting { get; set; }
    }
}
