using System;
using System.Collections.Generic;

namespace BooksUdemyCourse.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
