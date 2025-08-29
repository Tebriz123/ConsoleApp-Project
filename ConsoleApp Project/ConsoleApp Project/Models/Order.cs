using ConsoleApp_Project.Utilits.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp_Project.Models
{
    internal class Order:BaseEntity
    {

     
        
        List<OrderItem> Items { get; set;} 
        public int Total { get; set; }
        public string Email { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime MyProperty { get; set; }


    
    }
}
