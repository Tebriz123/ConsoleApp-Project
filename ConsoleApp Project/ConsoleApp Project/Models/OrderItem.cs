using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp_Project.Models
{
    internal class OrderItem
    {
        public static int s_count { get; set; } = 0;
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public int SubTotal { get; set; }


        public OrderItem()
        {
            Id = ++s_count;
        }
    }
}
