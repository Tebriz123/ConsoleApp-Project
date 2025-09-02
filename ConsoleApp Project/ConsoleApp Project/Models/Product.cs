using ConsoleApp_Project.Models;
using ConsoleApp_Project.Models.Base;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace ConsoleApp_Project.Models
{
    internal class Product:BaseAuditable
    {
        public string  Name { get; set; }

     

        public decimal Price { get; set; }

        public int Stock { get; set; }

      

        public Product(string name, decimal price, int stock)
        { 
       
            Name = name;
            Price = price;
            Stock = stock;
        }


        public void PrintInfo()
        {
            Console.WriteLine($"Id: {Id} Name: {Name} Price: {Price} Stock: {Stock}");
        }
        


    }
}
