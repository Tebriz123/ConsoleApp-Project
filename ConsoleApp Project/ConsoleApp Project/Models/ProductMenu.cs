using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp_Project.Models
{
    internal class ProductMenu
    {
        public void Run()
        {
            int num = 0;
            string str = null;
            bool result = false;

            while (!(num == 0 && result))
            {
                Console.WriteLine("1.Create Product\n2.Delete Product\n3.Get Product By Id\n4.Refill Product\n5.Order Product\n6.Shaw All Orders\n7.Change Order Status\n\n0.Exit");
                str = Console.ReadLine();
                Console.Clear();
                result = int.TryParse(str, out num);
                switch (num)
                {
                    case 1:










                        Console.WriteLine("Product created");
                        break;
                    case 2:
                        Console.WriteLine("Product deleted");
                        break;
                    case 3:
                        Console.WriteLine("Product");
                        break;
                    case 4:
                        Console.WriteLine("Products");
                        break;
                    case 5:
                        Console.WriteLine("Products REFIL");
                        break;
                    case 6:
                        Console.WriteLine("PRODECT ");
                        break;
                    case 7:
                        Console.WriteLine("ORDERS");
                        break;
                    case 8:
                        Console.WriteLine("ORDERS STATUS");
                        break;
                    case 0:
                        Console.WriteLine("EXITED");
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        break ;





                }


            }

        }
    }
}
