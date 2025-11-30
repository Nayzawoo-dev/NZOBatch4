using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ConsoleApp;

public class Book
{
    public Book(int id,string title,string author,string status) 
    { 
        Id = id;
        Title = title;
        Author = author;
        Status = status;
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Status { get; set; }

}
