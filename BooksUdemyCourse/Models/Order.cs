using System;
using System.Collections.Generic;

namespace BooksUdemyCourse.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public int IdClient { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Client IdClientNavigation { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
