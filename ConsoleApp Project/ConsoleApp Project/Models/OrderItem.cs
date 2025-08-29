using ConsoleApp_Project.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp_Project.Models
{
    internal class OrderItem:BaseEntity
    {
       
        
        public Product Product { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public int SubTotal { get; set; }



    }
}
