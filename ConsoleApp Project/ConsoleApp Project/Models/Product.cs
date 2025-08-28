using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace ConsoleApp_Project.Models
{
    internal class Product
    {
        private string _name;
        public int Id { get; set; }

        private static int s_count = 0;


        private decimal _price;

        private int _stock;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length >= 1)
                {
                    if (!value.Equals(Name))
                    {
                        _name = value;

                    }
                }
            }
        }

        public decimal Price
        {
            get
            {
                return _price;

            }
            set
            {
                if (value > 0)
                {
                    _price = value;
                }
            }
        }

        public int Stock
        {
            get
            {
                return _stock;
            }
            set
            {
                if (value >= 0)
                {
                    _stock = value;
                }
            }
        }

        public Product(string name, decimal price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;

            Id = ++s_count;
        }


        public void PrintInfo()
        {
            Console.WriteLine($"Id: {Id} Name: {Name} Price: {Price} Stock: {Stock}");
        }
        


    }
}
