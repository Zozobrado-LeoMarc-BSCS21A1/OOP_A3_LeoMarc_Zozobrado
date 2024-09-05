using System;
using System.Collections.Generic;

namespace Coffee
{
    class Printer
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }


    class MenuManager
    {
        public List<(string itemName, double itemPrice)> MenuItem { get; private set; }

        public MenuManager()
        {
            MenuItem = new List<(string, double)>();
        }

        public void AddMenuItem(Printer printer)
        {
            printer.Print("Enter item name: ");
            string itemName = Console.ReadLine();
            printer.Print("Enter item price: ");
            double itemPrice = Convert.ToDouble(Console.ReadLine());

            MenuItem.Add((itemName, itemPrice));
            printer.Print("Item added successfully!");
        }

        public void ViewMenu(Printer printer)
        {
            if (MenuItem.Count == 0)
            {
                printer.Print("The menu is empty.");
            }
            else
            {
                printer.Print("Menu:");
                for (int i = 0; i < MenuItem.Count; i++)
                {
                    printer.Print($"{i + 1}. {MenuItem[i].itemName} - ${MenuItem[i].itemPrice}");
                }
            }
        }
    }

    class OrderManager
    {
        public List<(string itemName, double itemPrice)> Order { get; private set; }

        public OrderManager()
        {
            Order = new List<(string, double)>();
        }

        public void PlaceOrder(MenuManager menuManager, Printer printer)
        {
            if (menuManager.MenuItem.Count == 0)
            {
                printer.Print("The menu is empty.");
                return;
            }

            menuManager.ViewMenu(printer);
            printer.Print("Enter the item number to order: ");
            int itemNumber = Convert.ToInt32(Console.ReadLine());

            if (itemNumber > 0 && itemNumber <= menuManager.MenuItem.Count)
            {
                Order.Add(menuManager.MenuItem[itemNumber - 1]);
                printer.Print("Item added to order!");
            }
            else
            {
                printer.Print("Invalid item number.");
            }
        }

        public void ViewOrder(Printer printer)
        {
            if (Order.Count == 0)
            {
                printer.Print("Your Order is empty!");
                return;
            }

            printer.Print("Your order: ");
            foreach (var item in Order)
            {
                printer.Print($"{item.itemName} - ${item.itemPrice}");
            }
        }
    }

    class Calculator
    {
        public void CalculateTotal(OrderManager orderManager, Printer printer)
        {
            double totalAmount = 0;
            foreach (var item in orderManager.Order)
            {
                totalAmount += item.itemPrice;
            }
            printer.Print($"Total Amount Payable: ${totalAmount}");
        }
    }

    class Program
    {
        static void Main()
        {
            Printer printer = new Printer();
            MenuManager menuManager = new MenuManager();
            OrderManager orderManager = new OrderManager();
            Calculator calculator = new Calculator();

            printer.Print("Welcome to the Coffee Shop!");
            while (true)
            {
                printer.Print("1. Add Menu Item");
                printer.Print("2. View Menu");
                printer.Print("3. Place Order");
                printer.Print("4. View Order");
                printer.Print("5. Calculate Total");
                printer.Print("6. Exit");

                printer.Print("Select an option: ");
                int option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                {
                    menuManager.AddMenuItem(printer);
                }
                else if (option == 2)
                {
                    menuManager.ViewMenu(printer);
                }
                else if (option == 3)
                {
                    orderManager.PlaceOrder(menuManager, printer);
                }
                else if (option == 4)
                {
                    orderManager.ViewOrder(printer);
                }
                else if (option == 5)
                {
                    calculator.CalculateTotal(orderManager, printer);
                }
                else if (option == 6)
                {
                    break;
                }
                else
                {
                    printer.Print("Invalid option, please try again.");
                }

                printer.Print("");
            }
        }
    }
}