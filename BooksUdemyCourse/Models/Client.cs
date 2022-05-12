using System;
using System.Collections.Generic;

namespace BooksUdemyCourse.Models
{
    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public DateTime RegisterDate { get; set; }
        public DateTime? UnregisterDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
