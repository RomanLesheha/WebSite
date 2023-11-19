namespace WebSite.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public int Discount { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

        public string FullName
        {
            get { return FName + " " + LName; }
        }

    }
}
