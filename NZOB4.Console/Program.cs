// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography.X509Certificates;

List<Product> products = new List<Product>();
start:
Console.WriteLine("------ Mini POS -----");
Console.WriteLine("1. Add Product");
Console.WriteLine("2. Product List");
Console.WriteLine("3. Edit Product");
Console.WriteLine("4. Delete Product");
Console.WriteLine("5. Exit");
Console.Write("Choose Option : ");
int opt = Convert.ToInt32(Console.ReadLine());

switch (opt)
{
    case 1: AddProduct(); goto start;
    case 2: ProductList(); goto start;
    case 3: EditProduct(); goto start;
    case 4: DeleteProduct(); goto start;
    case 5:
    default: break;
}

void DeleteProduct()
{
rearound:
    Console.WriteLine("Enter Product Code : ");
    int id = Convert.ToInt32(Console.ReadLine());
    var res = products.Where(x => x.Id == id).FirstOrDefault();

    if (res is null)
    {
        Console.WriteLine("Product Not Found");
        goto rearound;
    }
    Console.WriteLine("Product Found");
    Console.WriteLine($"Name : {res.Name}, Price : {res.Price}, Quantity : {res.Quantity}");
    Console.Write("Are You Sure Want To Delete (Y/N) ");
    string confirm = Console.ReadLine()!;
    if(confirm.ToUpper() != "Y")
    {
        return;
    }

    products.Remove(res);
    Console.WriteLine("Product Delete Successful");
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
    var res = products.Where(x => x.Id == id).FirstOrDefault();
    if (res is null)
    {
        Console.WriteLine("Product Not Found");
        goto revice;
    }
    Console.WriteLine("Product Found");
    Console.WriteLine($"Name : {res.Name}, Price : {res.Price}, Quantity : {res.Quantity}");

    decimal price;
    int quantity;
    Console.Write("Edit Name : ");
    string name = Console.ReadLine()!;
    if (string.IsNullOrEmpty(name))
    {
        name = res.Name;
    }
    Console.Write($"Edit Price : ");
    string str = Console.ReadLine()!;

    if (string.IsNullOrEmpty(str))
    {
        price = res.Price;
    }
    else
    {
        price = Convert.ToDecimal(str);
    }


    Console.Write($"Edit Price : ");
    string qua = Console.ReadLine()!;

    if (string.IsNullOrEmpty(qua))
    {
        quantity = res.Quantity;
    }
    else
    {
        quantity = Convert.ToInt32(qua);
    }



        var index = products.FindIndex(x => x.Id == id);
    products[index].Name = name;
    products[index].Price = price;
    products[index].Quantity = quantity;

    Console.WriteLine("Edit Successful");
}

void AddProduct()
{
    int id = 0;
    Console.Write("Enter Product Name : ");
    string productName = Console.ReadLine()!;
    Console.Write("Enter Quentity : ");
    int productQuentity = Convert.ToInt32(Console.ReadLine());
    Console.Write("Enter Price : ");
    decimal productPrice = Convert.ToDecimal(Console.ReadLine());
    if (products.Count is 0)
    {
        id += 1;
    }
    else
    {
        id = products.Max(x => x.Id) + 1;
    }
    products.Add(new Product(id, productName, productQuentity, productPrice));
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