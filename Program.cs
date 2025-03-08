using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShoppingCart cart = new ShoppingCart();
            cart.OnCartChange += message => Console.WriteLine(message);

            IProduct phone = new ElectronicProduct(1, "Iphone15", 300000);
            IProduct shirt = new ClothingProduct(2, "Polo", 50000, "M");
            IProduct apple = new FoodProduct(3, "Banana", 400, 5);
            
            cart.AddProduct(phone);
            cart.AddProduct(shirt);
            cart.AddProduct(apple);
            Console.WriteLine();
            cart.ShowShopingCart();
            Console.WriteLine();

            cart.RemoveProduct(phone);
            Console.WriteLine();
            cart.ShowShopingCart();
        }
    }

    interface IProduct
    {
        int ProductId { get; set; }
        string Name { get; set; }
        int Price { get; set; }
        void GetProductInfo();
    }

    public abstract class Product : IProduct
    {
       private int _productId;
       public int ProductId 
        {
            get { return _productId; }
            set
            {
                if (value > 0)
                {
                    _productId = value;
                }
            }
        }
        public string Name {  get; set; }

        private int _price;
        public int Price
        {
            get { return _price; }
            set
            {
                if (value > 0)
                {
                    _price = value;
                }
            }
        }
        public Product(int id, string name, int price)
        {
            ProductId = id;
            Name = name;
            Price = price;
        }
        public abstract void GetProductInfo();
    }

    class ElectronicProduct : Product
    {
        public int Warenty { get; } = 1;
        public ElectronicProduct(int id, string name, int price) : base(id, name, price)
        {
        }

        public override void GetProductInfo() 
        {
            Console.WriteLine($"{ProductId} | {Name} | {Price} | {Warenty}");
        }
    }
    class ClothingProduct : Product
    {
        public string Syze {  get; set; }
        public ClothingProduct(int id, string name, int price, string syze) : base(id, name, price)
        {
            Syze = syze;
        }
        public override void GetProductInfo()
        {
            Console.WriteLine($"{ProductId} | {Name} | {Price} | {Syze}");
        }
    }
    class FoodProduct : Product
    {
        private int _dayOfExpire;
        public int DayOfExpire
        {
            get { return _dayOfExpire; }
            set
            {
                if (value>0)
                {
                    _dayOfExpire = value;
                }
            }
        }
        public FoodProduct(int id, string name, int price, int expireDay) : base(id, name, price)
        {
            DayOfExpire = expireDay;
        }
        public override void GetProductInfo()
        {
            Console.WriteLine($"{ProductId} | {Name} | {Price} | {DayOfExpire}");
        }
    }
    class ShoppingCart
    {
        List<IProduct> products = new List<IProduct>();

        public delegate void CartChangeHandler(string message);
        public event CartChangeHandler OnCartChange;

        public void AddProduct(IProduct product)
        {
            products.Add(product);
            OnCartChange?.Invoke($"Product Added : {product.Name}");
        }

        public void RemoveProduct(IProduct product)
        {
            products.Remove(product);
            OnCartChange?.Invoke($"Product Removed : {product.Name}");
        }

        public void ShowShopingCart()
        {
            foreach (var el in products)
            {
                el.GetProductInfo();
            }
        }
    }
}
