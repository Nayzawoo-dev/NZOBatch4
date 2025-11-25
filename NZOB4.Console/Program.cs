// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography.X509Certificates;
int Id = 0;
List<Product> products = new List<Product>();
start:
Console.WriteLine("------ Mini POS -----");
Console.WriteLine("1. Add Product");
Console.WriteLine("2. Product List");
Console.WriteLine("3. Edit Product");
Console.WriteLine("4. Exit");
Console.Write("Choose Option : ");
int opt = Convert.ToInt32(Console.ReadLine());

switch (opt)
{
    case 1: AddProduct(); goto start;
    case 2: ProductList(); goto start;
    case 3: EditProduct(); goto start;
    default: break;
}

void ProductList()
{
    Console.WriteLine($"Total Product : {products.Count}");
    foreach (var item in products)
    {
        Console.WriteLine($"Product Code : {item.Id}");
        Console.WriteLine($"Name : {item.Name}");
        Console.WriteLine($"Price : {item.Price}");
        Console.WriteLine($"Quantity : {item.Quantity}");
    }
    Console.ReadLine();
}

void EditProduct()
{
revice:
    Console.Write("Enter Product Code : ");
    int id = Convert.ToInt32(Console.ReadLine());
    var res = products.Where(x => x.Id == id);
    if(res.Count() is 0)
    {
        Console.WriteLine("Product Not Found");
        goto revice;
    }
    foreach (var item in res)
    {
        Console.WriteLine($"Name : {item.Name} ,Price : {item.Price} , Quantity : {item.Quantity}");
    }
    Console.WriteLine("Product Found");

    Console.Write("Edit Price : ");
    decimal price = Convert.ToDecimal(Console.ReadLine());
    foreach (var item in res)
    {
        item.Price = price;
    }
    Console.WriteLine("Edit Successful");
}

void AddProduct()
{
    Console.Write("Enter Product Name : ");
    string productName = Console.ReadLine()!;
    Console.Write("Enter Quentity : ");
    int productQuentity = Convert.ToInt32(Console.ReadLine());
    Console.Write("Enter Price : ");
    decimal productPrice = Convert.ToDecimal(Console.ReadLine());
    products.Add(new Product(++Id, productName, productQuentity, productPrice));
    Console.WriteLine("Product Save Successfully");
}

public class Product
{
    public Product(int id, string name = null!, int quantity = 0, decimal price = 0)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

}