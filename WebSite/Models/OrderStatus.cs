using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required, MaxLength(20)]
        public string? StatusName { get; set; }
        public string? StatusDescription { get; set; }

        // Navigation property for the one-to-one relationship
        public Order Order { get; set; }
    }



    public enum Status
    {
        Pending,        // Order has been placed but not processed yet
        Processing,     // Order is currently being processed
        Shipped,        // Order has been shipped to the customer
        Delivered,      // Order has been successfully delivered
        Cancelled,      // Order has been canceled
        Returned,       // Order has been returned by the customer
        Refunded,       // Order has been refunded
        OnHold,         // Order is on hold for some reason
        Completed,      // Order is completed
        Failed,         // Order processing has failed
        AwaitingPayment // Order is waiting for payment
    }
}
