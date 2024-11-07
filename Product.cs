using System.Collections;
using System.Collections.Generic;
namespace e_commerce_system
{

    public enum Category
    {
        kids = 1,
        electricity,
        office,
        clothes
    }

    class Product
    {

        protected string product_name;
        protected double price;
        protected string original_seller;
        protected Category product_category;

        protected static int last_id = -1;
        protected int id;
        protected static int max_id = 0;


        public Product(string product_name, double price, string original_seller, int category_choice)
        {
            SetProductName(product_name);
            SetPrice(price);
            SetOriginalSeller(original_seller);
            this.original_seller = original_seller;
            SetCategory(category_choice);
            last_id++;
            id = last_id;
            max_id = last_id;
        }

        public Product(string product_name, double price, string original_seller, int category_choice, int id)
        {
            SetProductName(product_name);
            SetPrice(price);
            SetOriginalSeller(original_seller);
            this.original_seller = original_seller;
            SetCategory(category_choice);
            this.id = id;
            last_id++;
            if (max_id == id)
            {
                last_id = max_id;
            }
        }



        public Product(Product other)
        {
            product_name = other.product_name;
            price = other.price;
            original_seller = other.original_seller;
            product_category = other.product_category;
            id = other.GetId();
        }


        //  SET

        public bool SetCategory(int category_choice)
        {
            switch (category_choice)
            {
                case 1:
                    this.product_category = Category.kids;
                    return true;
                case 2:
                    product_category = Category.electricity;
                    return true;
                case 3:
                    product_category = Category.office;
                    return true;
                case 4:
                    product_category = Category.clothes;
                    return true;
                default:
                    System.Console.WriteLine("Selected category does not exist");
                    return false;
            }
        }


        public bool SetProductName(string product_name)
        {
            if (product_name == null || product_name == " ")
                return false;

            this.product_name = product_name;
            return true;
        }

        public bool SetPrice(double price)
        {
            if (price <= 0)
                return false;
            this.price = price;
            return true;

        }



        public bool SetOriginalSeller(string seller_name)
        {
            if (seller_name == null || seller_name == "")
            {
                System.Console.WriteLine("cannot be a space or an empty field");
                return false;
            }
            return true;
        }



        //  GET

        public string GetProductName() { return product_name; }
        public double GetPrice() { return price; }
        public string GetOriginalSeller() { return original_seller; }

        public int GetId() { return id; }
        public Category GetCategory() { return product_category; }




        public virtual new string ToString()
        {
            string res = "Category: " + product_category + "\t Product ID: " + id + "\t product name: " + product_name + "  \t price: " + price + "\nseller-" + original_seller + "\n";
            return res;
        }

        public virtual bool Equals(object other)
        {
            Product temp = other as Product;
            if (temp == null)
                return false;

            return id.Equals(temp.id);
        }

    }
}