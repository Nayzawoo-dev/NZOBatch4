using System.ComponentModel.DataAnnotations;

namespace Library.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LibraryServices libraryServices = new LibraryServices();
        start:
            Console.WriteLine("Mini Library Management System!");
            Console.WriteLine("1. Admin Type");
            Console.WriteLine("2. User Type");
            Console.WriteLine("3. Exit");

            Console.Write("Choose Your User Type : ");

            int opt = Convert.ToInt32(Console.ReadLine());

            switch (opt)
            {
                case 1: libraryServices.Auth(opt) ; goto start;
                case 2: libraryServices.Auth(opt); goto start;
                case 3:
                default: break;

            }

            Console.ReadLine();
        }

    }
}
