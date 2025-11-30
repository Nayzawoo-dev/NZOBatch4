using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ConsoleApp;

public class LibraryServices
{
    List<Book> book = new List<Book>();
    public void EditBook()
    {
    before:
        Console.WriteLine("1. Insert Book");
        Console.WriteLine("2. Edit Book");
        Console.WriteLine("3. Delete Book");
        Console.WriteLine("4. Exit");

        Console.Write("Choose Your Option : ");
        int opt = Convert.ToInt32(Console.ReadLine());

        switch (opt)
        {
            case 1: InsertBook(); goto before;
            case 2: UpdateBook(); goto before;
            case 3: DeleteBook(); goto before;
            case 4: default: break;
        }

        void InsertBook()
        {
            int id;
            switch (book.Count)
            {
                case 0: id = 1; break;
                default: id = book.Max(x => x.Id) + 1; break;
            }
            Console.Write("Enter Book Name : ");
            string bookTitle = Console.ReadLine()!;

            Console.Write("Enter Book Author : ");
            string bookAuthor = Console.ReadLine()!;

            string status = "available";
            book.Add(new Book(id, bookTitle, bookAuthor, status));
            Console.WriteLine("Insert Book Successfully!");
        }

        void DeleteBook()
        {
            Console.Write("Enter Book Code : ");
            int id = Convert.ToInt32(Console.ReadLine());

            var res = book.Where(x => x.Id == id).FirstOrDefault();

            if (res is null)
            {
                Console.WriteLine("Book Not Found");
                return;
            }

            Console.WriteLine($"Code : {res.Id} ; Name : {res.Title} ; Author : {res.Author}");

            Console.Write("Are You Sure Want To Dele(Y/N) : ");
            char confirm = Convert.ToChar(Console.ReadLine()!);

            if (char.ToUpper(confirm) is not 'Y')
            {
                return;
            }

            book.Remove(res);
            Console.WriteLine("Delete Book Successfully");


        }

        void UpdateBook()
        {
            Console.Write("Enter Book Code : ");
            int code = Convert.ToInt32(Console.ReadLine());

            var res = book.Where(x => x.Id == code).FirstOrDefault()!;

            if (res is null)
            {
                Console.WriteLine("Book Not Found");
                return;
            }

            Console.WriteLine($"Code : {res.Id} ; Name : {res.Title} ; Author : {res.Author}");

            Console.Write("Edit Book Title : ");
            string title = Console.ReadLine()!;


            if (string.IsNullOrEmpty(title)) title = res.Title;

            Console.Write("Edit Book Author : ");
            string author = Console.ReadLine()!;
            if (string.IsNullOrEmpty(author)) author = res.Author;

            int lst = book.FindIndex(x => x.Id == code);
            book[lst].Title = title;
            book[lst].Author = author;

            Console.WriteLine("Update Book Successfully");

        }

    }





    public void BookList()
    {
        Console.WriteLine("All Book List");

        List<Book> lst = book.ToList();

        Console.WriteLine("Code----------Title----------Author------------Status");

        if (lst.Count is 0)
        {
            Console.WriteLine("Book Not Found");
        }

        foreach (Book item in lst)
        {
            Console.WriteLine($"{item.Id}-----------{item.Title}-----------{item.Author}-----------{item.Status}");
        }

        Console.Write("Enter Book Code You Want To Loan : ");
        string loanid = Console.ReadLine()!;
        if (string.IsNullOrEmpty(loanid))
        {
            return;
        }

        int id = Convert.ToInt32(loanid);

        Book? res = book.Where(x => x.Id == id).FirstOrDefault();

        if (res is null || res.Status is not "available")
        {
            Console.WriteLine("This Book Not Found or Not Available Now");
            return;
        }

        res.Status = "Not Available";
        Console.WriteLine("Book Loan Successfully");

    }

    public void Auth(int opt)
    {
        switch (opt)
        {
            case 1:
            before:
                Console.WriteLine("1. Book Management");
                Console.WriteLine("2. Look Book Loan");
                Console.WriteLine("3. Exit");
                Console.Write("Choose Your Option : ");
                int option1 = Convert.ToInt32(Console.ReadLine());
                switch (option1)
                {
                    case 1: EditBook(); goto before;
                    case 2: BookList(); goto before;
                    case 3: default: break;
                }
                break;
            case 2:
            reverse:
                Console.WriteLine("1. Look Book Loan");
                Console.WriteLine("2. Exit");
                Console.Write("Choose Your Option : ");
                int option2 = Convert.ToInt32(Console.ReadLine());
                switch (option2)
                {
                    case 1: BookList(); goto reverse;
                    case 2: default: break;
                }
                break;
            case 3: default: break;

        }

    }
}