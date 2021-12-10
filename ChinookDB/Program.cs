using ChinookDB.Data;
using ChinookDB.models;
using Microsoft.Data.Sqlite;
using System;


namespace ChinookDB
{
    class Program
    {
        private readonly AppDbContext _context;
        public Program(AppDbContext context)
        {
            _context = context;
        }
        static void Main(string[] args)
        {
            using (var _context = new AppDbContext())
            {
                var adminUser = _context.AppUsers.FirstOrDefault(b => b.Id == 1);
                if (adminUser == null)
                {
                    _context.AppUsers.Add(new AppUser { Username = "admin", Password = "password" });
                }

                _context.SaveChanges();
            }

            MainMenu();

        }

        public static void MainMenu()
        {
            Console.WriteLine("         --------------------------------------");
            Console.WriteLine("         | Welcome to Garcia's Banking System |");
            Console.WriteLine("         --------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine($"Welcome please choose an action"); // Adds Welcome to main Screen
            Console.WriteLine("(1) Login");
            Console.WriteLine("(2) Add customer");
            Console.WriteLine("(x) Exit");
            var buttonChar = Console.ReadKey().KeyChar;
            if (buttonChar == '1')
            {
                var screen = new LoginScreen(); // Takes to new Screen where Login will take place
                screen.Show();

            }
            else if (buttonChar == '2')
            {
                createnewcustomerentry();

            }
            else if (buttonChar == 'X')
            {
                System.Environment.Exit(-1);
            }
        }

        // Creates a new Entry
        private static void createnewcustomerentry()
        {
            Console.Clear();
            Customer customer = new Customer();
            Console.WriteLine("Enter your First Name");
            customer.FirstName = Console.ReadLine();
            Console.WriteLine("Enter your Last Name");
            customer.LastName = Console.ReadLine();
            Console.WriteLine("Enter your Country's Name");
            customer.Country = Console.ReadLine();
            Console.WriteLine("Enter your Email");
            customer.Email = Console.ReadLine();

            using (var _context = new AppDbContext())
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }

            MainMenu();
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

        // Creates a new Entry
        //private static void createnewentry()
        //{
        //    throw new NotImplementedException();
        //}

        //// Function to find the CustomerDetails from the SqliteDatabase
        //public static void CustomerDetails()
        //{
        //    int CustomerId;
        //    String FirstName, LastName, Country, Email;
        //    // Connection String for Sqlite Database
        //    string conStr = @"Data Source=D:\SCHOOL\New folder\chinook\Chinook_Sqlite_AutoIncrementPKs.sqlite; datetimeformat=CurrentCulture;";

        //    SqliteConnection con = new SqliteConnection(conStr);

        //    // To check for the connection
        //    con.Open();

        //    // To write sql query for selecting records from the Customer table
        //    String query = "SELECT CustomerId, FirstName, LastName, Country, Email FROM Customer";

        //    // Creating cmd for executing queries
        //    SqliteCommand cmd = new SqliteCommand(query, con);

        //    //Executing and Reading records
        //    SqliteDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        CustomerId = Convert.ToInt32(dr[0].ToString());
        //        FirstName = dr[1].ToString();
        //        LastName = dr[2].ToString();
        //        Country = dr[3].ToString();
        //        Email = dr[4].ToString();

        //        Console.Write("CustomerId:" + CustomerId + " FirstName:" + FirstName + " LastName:" + LastName + " Country:" + Country + " Email:" + Email + "\n");
        //    }
        //    con.Close();
        //}

        //// Function to find the InvoiceDetails from the SqliteDatabase
        //public static void InvoiceDetails()
        //{
        //    int InvoiceId, CustomerId;
        //    DateTime InvoiceDate;
        //    decimal Total;
        //    // Connection String for Sqlite Database
        //    string conStr = @"Data Source=D:\SCHOOL\New folder\chinook\Chinook_Sqlite_AutoIncrementPKs.sqlite; datetimeformat=CurrentCulture;";
        //    SqliteConnection con = new SqliteConnection(conStr);

        //    // To check for the connection
        //    con.Open();

        //    // To write sql query for selecting records from the Invoice table
        //    String query = "SELECT InvoiceId, CustomerId, InvoiceDate, Total FROM Invoice";

        //    // Creating cmd for executing queries
        //    SqliteCommand cmd = new SqliteCommand(query, con);

        //    //Executing and Reading records
        //    SqliteDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        InvoiceId = Convert.ToInt32(dr[0].ToString());
        //        CustomerId = Convert.ToInt32(dr[1].ToString());
        //        InvoiceDate = Convert.ToDateTime(dr[2].ToString());
        //        Total = Convert.ToDecimal(dr[3].ToString());

        //        Console.Write("InvoiceId:" + InvoiceId + " CustomerId:" + CustomerId + " InvoiceDate:" + InvoiceDate + " Total:" + Total + "\n");
        //    }
        //    con.Close();
        //}
    }
}