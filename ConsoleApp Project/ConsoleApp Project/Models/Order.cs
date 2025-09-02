using ConsoleApp_Project.Models.Base;
using ConsoleApp_Project.Utilits.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace ConsoleApp_Project.Models
{
    internal class Order: BaseAuditable
    {

     
        
        public List<OrderItem> Items { get; set;} = new List<OrderItem>();
        public decimal Total { get { return CalculateTotal(); } }
        public string Email { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public Order(List<OrderItem> items, string email)
        {
            Items = items;
            Email = email;
        }
        private decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.SubTotal;
            }
            return total;
        }
        public void PrintInfo()
        {
            Console.WriteLine($"Id: {Id} Total: {Total} Email: {Email} Status: {Status} CreateDate: {CreateDate}");
            foreach (var item in Items)
            {
                item.Product.PrintInfo();
            }
        }
    }
}
