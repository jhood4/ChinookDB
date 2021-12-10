using ChinookDB.Data;
using ChinookDB.models;
using System.Collections.Generic;

namespace ChinookDB
{
    public class Account
    {
        public string Name { get; set; } // auto-implemented property
        public string UserName { get; set; } // added by Garcia

        public short Pin; // added by Garcia 

        // Account constructor that receives two parameters  
        public Account(string accountName, string accountUserName, short accountPin)
        {
            Name = accountName;
            UserName = accountUserName; // added by Garcia
            Pin = accountPin;
        }

        // Create a private menu for the admin to use

        public static void menu()
        {
            Console.Clear();
            LoginScreen.database();
            Console.WriteLine(" What do you wish to do? ");
            Console.WriteLine("---------------------------");
            Console.WriteLine(" (1) Edit Customer Tables");
            Console.WriteLine(" (2) Edit Invoice Tables");
            Console.WriteLine(" (3) Exit");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Choose an Option from the menu");
        }


        internal static void Show()
        {
            Console.WriteLine($"Welcome User"); // welcome string
            Console.WriteLine(" ");
            Console.WriteLine(" Please choose an option");
            Account.menu();
            var buttonChar = Console.ReadKey().KeyChar;

            if (buttonChar == '1') // Edits Customer table
            {
                editCtable();

            }
            else if (buttonChar == '2') // Edits Invoice Table
            {
                EditItable();

            }
            else if (buttonChar == '3') // Exit
            {
                System.Environment.Exit(-1);
            }


        }
        private static void EditItable()
        {
            throw new NotImplementedException();
        }

        private static void editCtable()
        {
            throw new NotImplementedException();
        }


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


        // used once for main screen
        internal static void database()
        {
            Console.WriteLine("         ---------------------------------------");
            Console.WriteLine("         | Welcome to Garcia's Database System |");
            Console.WriteLine("         ---------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine($"Welcome please choose an action"); // Adds Welcome to main Screen
            Console.WriteLine(" ");

        }
        public static void Database()
        {
            Console.WriteLine("         ---------------------------------------");
            Console.WriteLine("         | Welcome to Garcia's Database System |");
            Console.WriteLine("         ---------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine($"Welcome please follow the instructions Below"); // Adds Welcome to main Screen
            Console.WriteLine(" ");
        }

        internal void Show(object list)
        {
            throw new NotImplementedException();
        }
    }

    public class AdminScreen
    {
        public static void Show()
        {
            IList<Customer> customers = new List<Customer>();
            int ch;
            Console.Clear();
            Console.WriteLine("1. Update a customer");
            Console.WriteLine("2. Select a customer");
            Console.WriteLine("3. Select a customer with invoices");
            Console.WriteLine("4. Select all customers");
            Console.WriteLine("5. Create a new invoice");
            Console.WriteLine("6. Update a invoice");
            Console.WriteLine("7. Select an invoice");
            Console.WriteLine("8. Select a invoice with customer details");
            Console.WriteLine("9. Select all invoice");
            Console.WriteLine("M. Main Menu");
            Console.WriteLine("X. Exit");
            Console.WriteLine("Enter Your Choice:");
            ch = Convert.ToInt32(Console.ReadLine());
            switch (ch)
            {
                case 1:
                    UpdateCustomer();
                    break;
                case 4:
                    customers = CustomerDetails(); // To find the details of the Customer from the Sqlite database
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
        public static IList<Customer> CustomerDetails()
        {
            using (var _context = new AppDbContext())
            {
                IList<Customer> customers = _context.Customers.ToList();
                _context.Dispose(); //not sure if you need this or not, the using should dispose it automatically i would think but i'm not positive
                return customers;
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
                        Console.WriteLine($"Id: {cust.Id} | First name: {cust.FirstName} | Last name: {cust.LastName} | Country: {cust.Country} | Email: {cust.Email}");
                    }

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Please enter the id of the user you would like to update");
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
    }

    internal class Accountadmin
    {
        public object Name { get; private set; }

        public void Show(List<Account> list)
        {
            Console.Clear();
            LoginScreen.database();
            Console.WriteLine("Welcome admin"); // welcome string
            Console.WriteLine(" ");
            Account.menu(); // created private method for menu
            var buttonChar = Console.ReadKey().KeyChar;

            if (buttonChar == '1')
            {

                Console.WriteLine(" ");


            }
            else if (buttonChar == '2') // 

            {
                Console.WriteLine("");
            }
            else if (buttonChar == '3')

            {
                System.Environment.Exit(-1);
            }
        }
    }

    class AccountTest
    {


        static void loss()
        {
            // displays parameters of account name, user, and pin
            Account AdminAccount = new Account("Admin","adm1", 0001);
            Account account1 = new Account("User","Guest", 1234);

         // type parameter
            var list = new List<Account> { AdminAccount, account1};

            // creating variable use var to assign a value
            // var creates variable, name infront or var states the name of the variable which then the = assigns the right side into variable
            // new creates an instance of a type name infront of new grabs the func. of a class and applys it to the variable.
            var screen = new Database(); // Makes Variable Screen
            screen.Show(list); // Calls variable to show welcome Screen

        }

        private class Database
        {
            public Database()
            {
            }

            internal void Show(List<Account> list)
            {
                throw new NotImplementedException();
            }
        }
    }
}