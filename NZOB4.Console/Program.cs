// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography.X509Certificates;
int Id = 0;
List<Product> products = new List<Product>();
start:
Console.WriteLine("------ Mini POS -----");
Console.WriteLine("1. Add Product");
Console.WriteLine("2. Product List");
Console.WriteLine("3. Exit");
Console.Write("Choose Option : ");
int opt = Convert.ToInt32(Console.ReadLine());

switch (opt)
{
    case 1: AddProduct(); goto start;
    case 2: ProductList(); goto start;
    case 3:
    default: break;
}

void ProductList()
{
    Console.WriteLine($"Total Product : {products.Count}");
    foreach (var item in products)
    {
        Console.WriteLine($"Name : {item.Name}");
        Console.WriteLine($"Price : {item.Price}");
        Console.WriteLine($"Quantity : {item.Quantity}");
    }
    Console.ReadLine();
}

void AddProduct()
{
    Console.Write("Enter Product Name : ");
    string productName = Console.ReadLine()!;
    Console.Write("Enter Quentity : ");
    int productQuentity = Convert.ToInt32(Console.ReadLine());
    Console.Write("Enter Price : ");
    decimal productPrice = Convert.ToDecimal(Console.ReadLine());
    Console.WriteLine("Product Save Successfully");
    products.Add(new Product(productName, productQuentity, productPrice));
}

public class Product
{
    public Product(string name, int quantity, decimal price)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public int Quantity { get; set; }

}