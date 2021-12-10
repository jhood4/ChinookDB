using ChinookDB.Data;
using ChinookDB.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ChinookDB
{
    public class Account
    {
      
    }
    // Login Screen?
    internal class LoginScreen
    {
        public void Show()
        {
            Console.Clear();
            Console.WriteLine("Welcome! Please enter your username.");
            var username = Console.ReadLine();
            Console.WriteLine("Please enter your password");
            var password = Console.ReadLine();

            using(var _context = new AppDbContext())
            {
                var userDetails = _context.AppUsers.FirstOrDefault(x => x.Username == username);
                if(userDetails is null)
                {
                    Console.Clear();
                    Program.MainMenu();
                }

                if(userDetails.Password != password)
                {
                    Console.Clear();
                    Program.MainMenu();
                }

                AdminScreen.Show();
            }
        }
    }

    public class AdminScreen
    {
        public static void Show()
        {
            IList<Customer> customers = new List<Customer>();
            int ch;
            Console.Clear();
            Console.WriteLine("1. Select all customers");
            Console.WriteLine("2. Select a customer");
            Console.WriteLine("3. Select a customer with invoices");
            Console.WriteLine("4. Update a customer");
            Console.WriteLine("5. Create a new invoice");
            Console.WriteLine("6. Select an invoice");
            Console.WriteLine("7. Select a invoice with customer details");
            Console.WriteLine("8. Select all invoice");
            Console.WriteLine("9. Update a invoice");
            Console.WriteLine("10. Main Menu");
            Console.WriteLine("11. Exit");
            Console.WriteLine("Enter Your Choice:");
            ch = Convert.ToInt32(Console.ReadLine());
            switch (ch)
            {
                case 1:
                    CustomerDetails();
                    break;
                case 2:
                    SelectCustomerById();
                    break;
                case 3:
                    SelectCustomerWithInvoices();
                    break;
                case 4:
                    UpdateCustomer();
                    break;
                case 5:
                    CreateInvoice();
                    break;
                case 6:
                    SelectInvoice();
                    break;
                case 7:
                    SelectInvoiceWithCustomer();
                    break;
                case 8:
                    SelectAllInvoices();
                    break;
                case 9:
                    UpdateInvoice();
                    break;
                case 10:
                    Program.MainMenu();
                    break;
                case 11:
                    System.Environment.Exit(-1);
                    break;
                //case 2:
                //    InvoiceDetails(); // To call the InvoiceDetails function to find the Invoice Details from the sqlite database
                //    break;
                default:
                    break;
            }
            Console.WriteLine(customers);
        }

        //// Function to find the CustomerDetails from the Database
        public static void CustomerDetails()
        {
            using (var _context = new AppDbContext())
            {
                Console.Clear();
                IList<Customer> customers = _context.Customers.ToList();
                _context.Dispose(); //not sure if you need this or not, the using should dispose it automatically i would think but i'm not positive
                foreach(var customer in customers)
                {
                    Console.WriteLine($"Id: {customer.Id}");
                    Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                    Console.WriteLine($"Country: {customer.Country}");
                    Console.WriteLine($"Email: {customer.Email}");
                    Console.WriteLine();
                    Console.WriteLine();
                }
                Console.WriteLine("Press any key to return to the menu");
                var ch = Console.ReadKey().KeyChar;

                AdminScreen.Show();
            }
        }

        public static void SelectCustomerById()
        {
            Console.Clear();
            using (var _context = new AppDbContext())
            {
                var customers = _context.Customers.ToList();
                if (customers.Count > 0)
                {
                    foreach(var cust in customers)
                    {
                        Console.WriteLine($"Id: {cust.Id} | First name: {cust.FirstName} | Last name: {cust.LastName}");
                    }


                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Please enter the id of the customer you would like to view");
                    var userId = Convert.ToInt32(Console.ReadLine());
                    Customer customer = _context.Customers.FirstOrDefault(x => x.Id == userId);
                    Console.WriteLine($"First name: {customer.FirstName}");
                    Console.WriteLine($"Last name: {customer.LastName}");
                    Console.WriteLine($"Country: {customer.Country}");
                    Console.WriteLine($"Email: {customer.Email}");

                    Console.WriteLine();
                    Console.WriteLine();

                    Console.WriteLine("Press any key to return to the menu");
                    var ch = Console.ReadKey().KeyChar;

                    AdminScreen.Show();
                }
            }
        }

        public static void SelectCustomerWithInvoices()
        {
            Console.Clear();
            using (var _context = new AppDbContext())
            {
                var customers = _context.Customers.ToList();
                if (customers.Count > 0)
                {
                    foreach (var cust in customers)
                    {
                        Console.WriteLine($"Id: {cust.Id} | First name: {cust.FirstName} | Last name: {cust.LastName} | Country: {cust.Country} | Email: {cust.Email}");
                    }

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Please enter the id of the customer you would like to view");
                    var userId = Convert.ToInt32(Console.ReadLine());
                    var customer = _context.Customers.Where(x => x.Id == userId).Include("Invoices").FirstOrDefault();
                    Console.WriteLine("Customer Details");
                    Console.WriteLine($"Customer name: {customer.FirstName} {customer.LastName}");
                    Console.WriteLine($"Country: {customer.Country}");
                    Console.WriteLine($"Email: {customer.Email}");
                    Console.WriteLine();
                    Console.WriteLine("Invoice Details");
                    foreach(var invoice in customer.Invoices)
                    {
                        Console.WriteLine($"Invoice Id: {invoice.InvoiceId}");
                        Console.WriteLine($"Invoice Date: {invoice.InvoiceDate}");
                        Console.WriteLine($"Amount: {invoice.Amount}");
                    }

                    Console.WriteLine();
                    Console.WriteLine();

                    Console.WriteLine("Press any key to return to the menu");
                    var ch = Console.ReadKey().KeyChar;

                    AdminScreen.Show();
                }
            }
        }

        public static void UpdateCustomer()
        {
            Console.Clear();
            using(var _context = new AppDbContext())
            {
                var customers = _context.Customers.ToList();
                if(customers.Count > 0)
                {
                    foreach(var cust in customers)
                    {
                        Console.WriteLine($"Id: {cust.Id} | First name: {cust.FirstName} | Last name: {cust.LastName}");
                    }

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Please enter the id of the customer you would like to update");
                    var userId = Convert.ToInt32(Console.ReadLine());
                    Customer customer = _context.Customers.FirstOrDefault(x => x.Id == userId);
                    Console.WriteLine("Enter the customer First Name");
                    customer.FirstName = Console.ReadLine();
                    Console.WriteLine("Enter the customer Last Name");
                    customer.LastName = Console.ReadLine();
                    Console.WriteLine("Enter your Country's Name");
                    customer.Country = Console.ReadLine();
                    Console.WriteLine("Enter your Email");
                    customer.Email = Console.ReadLine();

                    _context.Customers.Update(customer);
                    _context.SaveChanges();

                    AdminScreen.Show();
                }
                
            }
        }

        public static void CreateInvoice()
        {
            using(var _context = new AppDbContext())
            {
                var customers = _context.Customers.ToList();
                if (customers.Count > 0)
                {
                    foreach (var cust in customers)
                    {
                        Console.WriteLine($"Id: {cust.Id} | First name: {cust.FirstName} | Last name: {cust.LastName}");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Please enter the id of the customer you would like create an invoice for");
                    Invoice invoice = new Invoice();
                    var userId = Convert.ToInt32(Console.ReadLine());
                    invoice.CustomerId = userId;
                    Console.WriteLine("Enter the invoice amount");
                    invoice.Amount = Convert.ToDecimal(Console.ReadLine());
                    invoice.InvoiceDate = DateTime.Now;

                    _context.Invoices.Add(invoice);
                    _context.SaveChanges();

                    AdminScreen.Show();
                }
            }
        }

        public static void SelectInvoice()
        {
            Console.Clear();
            using (var _context = new AppDbContext())
            {
                var invoices = _context.Invoices.ToList();
                if (invoices.Count > 0)
                {
                    foreach (var inv in invoices)
                    {
                        Console.WriteLine($"Id: {inv.InvoiceId}");
                    }

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Please enter the id of the invoice you would like to view");

                    var invoiceId = Convert.ToInt32(Console.ReadLine());
                    Invoice invoice = _context.Invoices.FirstOrDefault(x => x.InvoiceId == invoiceId);
                    Console.WriteLine($"Invoice Id: {invoice.InvoiceId}");
                    Console.WriteLine($"Invoice Date: {invoice.InvoiceDate}");
                    Console.WriteLine($"Amount: ${invoice.Amount}");

                    Console.WriteLine();
                    Console.WriteLine();

                    Console.WriteLine("Press any key to return to the menu");
                    var ch = Console.ReadKey().KeyChar;

                    AdminScreen.Show();
                }
            }
        }

        public static void SelectInvoiceWithCustomer()
        {
            Console.Clear();
            using (var _context = new AppDbContext())
            {
                var invoices = _context.Invoices.ToList();
                if (invoices.Count > 0)
                {
                    foreach (var inv in invoices)
                    {
                        Console.WriteLine($"Id: {inv.InvoiceId}");
                    }

                    Console.WriteLine();
                    Console.WriteLine();

                    Console.WriteLine("Please enter the id of the invoice to see what customer it was issued for.");

                    var invoiceId = Convert.ToInt32(Console.ReadLine());
                    Invoice invoice = _context.Invoices.Where(x=> x.InvoiceId == invoiceId).Include("Customers").FirstOrDefault();
                    Console.WriteLine("Invoice Details");
                    Console.WriteLine($"Invoice Id: {invoice.InvoiceId}");
                    Console.WriteLine($"Invoice Date: {invoice.InvoiceDate}");
                    Console.WriteLine($"Amount: ${invoice.Amount}");

                    Console.WriteLine();
                    Console.WriteLine();

                    Console.WriteLine("Customer Details");
                    Console.WriteLine($"Customer name: {invoice.Customers.FirstName} {invoice.Customers.LastName}");
                    Console.WriteLine($"Country: {invoice.Customers.Country}");
                    Console.WriteLine($"Email: {invoice.Customers.Email}");

                    Console.WriteLine();
                    Console.WriteLine();

                    Console.WriteLine("Press any key to return to the menu");
                    var ch = Console.ReadKey().KeyChar;

                    AdminScreen.Show();
                }
            }
        }

        public static void SelectAllInvoices()
        {
            using(var _context = new AppDbContext())
            {
                Console.Clear();
                IList<Invoice> invoices = _context.Invoices.ToList();
                _context.Dispose(); //not sure if you need this or not, the using should dispose it automatically i would think but i'm not positive
                foreach (var invoice in invoices)
                {
                    Console.WriteLine($"Id: {invoice.InvoiceId}");
                    Console.WriteLine($"Invoice Date {invoice.InvoiceDate}");
                    Console.WriteLine($"Amount: ${invoice.Amount}");

                    Console.WriteLine();
                    Console.WriteLine();
                }
                Console.WriteLine("Press any key to return to the menu");
                var ch = Console.ReadKey().KeyChar;

                AdminScreen.Show();
            }
        }

        public static void UpdateInvoice()
        {
            Console.Clear();
            using (var _context = new AppDbContext())
            {
                var invoices = _context.Invoices.ToList();
                if (invoices.Count > 0)
                {
                    foreach (var inv in invoices)
                    {
                        Console.WriteLine($"Id: {inv.InvoiceId} | Invoice Date: {inv.InvoiceDate} | Amount: ${inv.Amount}");
                    }

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Please enter the id of the invoice you would like to update");
                    var invoiceId = Convert.ToInt32(Console.ReadLine());
                    Invoice invoice = _context.Invoices.FirstOrDefault(x => x.InvoiceId == invoiceId);
                    Console.WriteLine("Enter the Amount");
                    invoice.Amount = Convert.ToDecimal(Console.ReadLine());
                    

                    _context.Invoices.Update(invoice);
                    _context.SaveChanges();

                    AdminScreen.Show();
                }

            }
        }
    }
}