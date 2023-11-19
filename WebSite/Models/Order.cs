using System.ComponentModel.DataAnnotations;
using WebSite.Areas.Identity.Data;
using WebSite.Models;

namespace WebSite.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        // Foreign key property
        public int StatusId { get; set; }

        public int AdressDetailID { get; set; }

        // Navigation property for the one-to-one relationship
        public OrderStatus Status { get; set; }

        public CustomerAdressDetail adressDetail { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }

    public class CustomerAdressDetail
    {
        public int ID { get; set; }

        public string PhoneNumber {  get; set; }

        public string FullName { get; set; }

        public string DeliverySity { get; set; }

        public string PostDepartment { get; set; }


    }
}
