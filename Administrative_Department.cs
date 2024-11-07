using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.IO;
using System;
using System.Diagnostics;

namespace e_commerce_system { 

class Administrative_Department
{
    private string company_name;
    private List<User> users;
    private static int user_counter;
    private static int buyer_counter;
    private static int seller_counter;




    public Administrative_Department(string company_name)
    {
        SetCompanyName(company_name);
        user_counter = 0;
        buyer_counter = 0;
        seller_counter = 0;
        users = new List<User>();
    }

    public static Administrative_Department operator +(Administrative_Department a1, Buyer a2)
    {
        a1.AddBuyer(a2);
        return a1;

    }
    public static Administrative_Department operator +(Administrative_Department a1, Seller a2)
    {
        a1.AddSeller(a2);
        return a1;
    }

    // Get
    public string GetCompanyName() { return company_name; }
    public List<User> GetUsers() { return users; }
    public int GetUserCounter() { return seller_counter; }
    public int GetSellerCounter() { return seller_counter; }
    public int GetBuyerCounter() { return buyer_counter; }




    // Set
    public bool SetCompanyName(string company_name)
    {
        if (company_name == null || company_name == " " || company_name == "")
        {
            System.Console.WriteLine("Company name cannot be a space or an empty field");
            return false;
        }
        this.company_name = company_name;
        return true;
    }

    public bool SetUsers(List<User> users)
    {
        {
            if (users == null)
                return false;

            this.users = users;
            return true;
        }
    }


    //#1
    public void AddBuyer(Buyer buyer_user)
    {
        users.Add(buyer_user);
        user_counter++;
        buyer_counter++;
        System.Console.WriteLine("\nSuccessfully added Buyer!\n");
    }


    public int IsUserExist(string user_name)

    {
        for (int i = 0; i < user_counter; i++)
        {
            if (users[i].GetUsername().Equals(user_name))
                return i;
        }
        return -1;
    }

    public int IsBuyerExist(string user_name)

    {
        for (int i = 0; i < user_counter; i++)
        {
            User temp = users[i] as Buyer;
            if (temp != null && users[i].GetUsername().Equals(user_name))
                return i;
        }
        return -1;
    }
    public Buyer GetBuyerByName(string buyer_name)
    {
        foreach (var item in users)
        {
            Buyer temp = item as Buyer;
            if (temp != null)
            {
                if (item.GetUsername() == buyer_name)
                    return temp;
            }
        }
        return null;
    }

    public int IsSellerExist(string user_name)

    {
        for (int i = 0; i < user_counter; i++)
        {
            User temp = users[i] as Seller;
            if (temp != null && users[i].GetUsername().Equals(user_name))
                return i;
        }
        return -1;
    }

    //#2
    public void AddSeller(Seller seller_user) // check if isnt a buyer
    {
        users.Add(seller_user);
        user_counter++;
        seller_counter++;
        System.Console.WriteLine("\nSuccessfully added Seller!\n");


    }
    //#3
    public void AddProductToSeller(Product new_product, Seller seller_user)
    {
        string seller_name = seller_user.GetUsername();
        string product_name = new_product.GetProductName();
        if (seller_counter == 0)
        {
            System.Console.WriteLine("\nError- No sellers yet\n");
            return;
        }

        for (int i = 0; i < user_counter; i++)
        {

            if (users[i].GetNumberOfProduct() == 0 && users[i].GetUsername() == seller_name)
            {
                seller_user.AddProductToCart(new_product);
                return;
            }

            for (int j = 0; j < users[i].GetNumberOfProduct(); j++)
            {
                if (users[i].GetUsername() == seller_name && product_name != users[i].GetProductCart()[j].GetProductName())
                {
                    seller_user.AddProductToCart(new_product);
                    return;
                }
                else if (users[i].GetUsername() == seller_name && product_name == users[i].GetProductCart()[j].GetProductName())
                {
                    System.Console.WriteLine("\nA product already exists in your store\n");
                    return;
                }
            }
        }
    }



    public Product GetProductOfSeller(string seller_name, string product_name)
    {
        for (int i = 0; i < user_counter; i++)
        {
            Seller temp = users[i] as Seller;
            Product temp_product;
            if (temp != null)
            {
                for (int j = 0; j < users[i].GetNumberOfProduct(); j++)
                {
                    temp_product = users[i].GetProductCart()[j];

                    if (users[i].GetProductCart()[j].GetProductName() == product_name && seller_name == users[i].GetUsername())
                    {
                        return temp_product;
                    }
                }

            }
        }
        return null;
    }


    public Product GetProductByNAme(string product_name)
    {
        for (int i = 0; i < user_counter; i++)
        {
            Seller temp = users[i] as Seller;
            Product temp_product;
            if (temp != null)
            {
                for (int j = 0; j < users[i].GetNumberOfProduct(); j++)
                {
                    temp_product = users[i].GetProductCart()[j];

                    if (users[i].GetProductCart()[j].GetProductName() == product_name)
                    {
                        return temp_product;
                    }
                }

            }
        }
        return null;
    }


    public int IsMultipleProduct(string product_name)
    {

        if (seller_counter == 0)
        {
            System.Console.WriteLine("\nNo sellers yet\n");
            return -1;
        }

        int product_counter = 0;
        Product temp;

        for (int i = 0; i < user_counter; i++)
        {
            Seller temp_a = users[i] as Seller;
            if (temp_a != null)
            {
                for (int j = 0; j < users[i].GetNumberOfProduct(); j++)
                {
                    temp = users[i].GetProductCart()[j];
                    if (temp.GetProductName() == product_name)
                    {
                        product_counter++;
                    }
                }
            }
        }
        if (product_counter != 0)
            return product_counter;

        return -1;

    }


    //#4
    public void AddProductToBuyer(Product new_product, Buyer buyer_user)
    {
        string buyer_name = buyer_user.GetUsername();

        if (user_counter == 0)
        {
            System.Console.WriteLine("\nError- No buyers yet\n");
            return;
        }

        else if (buyer_user.GetNumberOfProduct() == 0)
        {
            buyer_user.AddProductToCart(new_product);
            return;
        }

        for (int i = 0; i < user_counter; i++)
        {
            if (users[i].GetUsername() == buyer_name)
            {
                buyer_user.AddProductToCart(new_product);
            }
        }
    }

    public bool AddCartFromHistoryToCurrent(Buyer buyer_user, int index_of_cart)
    {
        List<Order> new_order_history = new List<Order>(buyer_user.GetOrderHistory());
        if (index_of_cart < 0 || index_of_cart > buyer_user.GetOrderCounter())
            return false;

        buyer_user.ClearProductCart();

        foreach (var item in new_order_history[index_of_cart - 1].GetShoppingCart())
        {
            buyer_user.AddProductToCart(item);
        }
        System.Console.WriteLine("Cart added");
        return true;
    }


    public string ShowAllProductByCategory(int category_choice)
    {
        Category eCategory = (Category)category_choice;
        int product_by_category_counter = 0;
        string res = "\nCategory: " + eCategory + "\n-----------------------------------------------------------";


        for (int i = 0; i < user_counter; i++)
        {
            Seller temp = users[i] as Seller;
            if (temp != null)
            {
                if (users[i].GetNumberOfProduct() != 0 && IsSellerHasCategoryProduct(category_choice, temp))
                {
                    res += "\nSeller name: " + users[i].GetUsername() + "\nProducts:\n";
                    product_by_category_counter++;
                }


                for (int j = 0; j < users[i].GetNumberOfProduct(); j++)
                {

                    if (users[i].GetProductCart()[j].GetCategory() == eCategory)
                    {

                        PackagedProduct temp_two = users[i].GetProductCart()[j] as PackagedProduct;
                        if (temp_two != null)
                        {
                            temp_two.SetIsSpecialPackaging(true);
                            res += temp_two.ToString() + "\n ";
                            temp_two.SetIsSpecialPackaging(false);
                        }
                        else
                            res += users[i].GetProductCart()[j].ToString();
                    }


                }
                res += "-----------------------------------------------------------";

            }
        }
        if (product_by_category_counter == 0)
        {
            res = null;
            return res;
        }
        return res;
    }

    public bool IsSellerHasCategoryProduct(int category_choice, Seller seller)
    {
        Category eCategory = (Category)category_choice;
        if (seller.GetNumberOfProduct() == 0)
            return false;

        for (int j = 0; j < seller.GetNumberOfProduct(); j++)
        {
            if (seller.GetProductCart()[j].GetCategory() == eCategory)
            {
                return true;
            }
        }
        return false;
    }

    //Sorted by quantity of products per seller
    public bool ShowAllSellersProduct()
    {
        List<int> temp_list_of_quantitiy = new List<int>();
        List<Seller> sellers = new List<Seller>();
        for (int i = 0; i < user_counter; i++)
        {
            Seller temp = users[i] as Seller;
            if (temp != null)
            {
                sellers.Add(temp);
                if (!temp_list_of_quantitiy.Contains(temp.GetNumberOfProduct()))
                {
                    temp_list_of_quantitiy.Add(temp.GetNumberOfProduct());
                }
            }
        }


        temp_list_of_quantitiy.Sort();
        temp_list_of_quantitiy.Reverse();

        int is_products = 0;
        System.Console.WriteLine("\nAll Selers:\n-----------------------------------------------------------\n");
        for (int i = 0; i < temp_list_of_quantitiy.Count(); i++)
        {
            for (int j = 0; j < sellers.Count(); j++)
            {
                if (sellers[j].GetNumberOfProduct() == temp_list_of_quantitiy[i])
                {
                    System.Console.WriteLine("\nSeller name: " + sellers[j].GetUsername() + "\nProducts:\n");

                    if (sellers[j].GetNumberOfProduct() == 0)
                    {
                        System.Console.WriteLine("No product to show yet.");
                    }
                    for (int k = 0; k < sellers[j].GetNumberOfProduct(); k++)
                    {
                        PackagedProduct temp_two = sellers[j].GetProductCart()[k] as PackagedProduct;
                        if (temp_two != null)
                        {
                            temp_two.SetIsSpecialPackaging(true);
                            System.Console.WriteLine(temp_two.ToString() + "\n ");
                            temp_two.SetIsSpecialPackaging(false);
                        }
                        else
                            System.Console.WriteLine(sellers[j].GetProductCart()[k].ToString());


                        is_products++;
                    }
                    {

                    }
                    System.Console.WriteLine("-----------------------------------------------------------\n");
                }
            }
        }

        if (is_products == 0)
        {
            System.Console.WriteLine("\nNo products to show yet\n");
            return false;
        }
        return true;
    }



    //#5
    public void OrderProcessForBuyer(Buyer buyer_user)
    {
        Order new_order = new Order(buyer_user, buyer_user.GetProductCart());
        System.Console.WriteLine(new_order.ToString());
    }
    public void AddOrderToHistory(Buyer buyer_user)
    {
        Buyer new_buyer = new Buyer(buyer_user);
        Order new_order = new Order(buyer_user, new_buyer.GetProductCart());
        buyer_user.AddOrderToHistory(new_order);
    }
    //#6
    public string DisplayAllBuyers()
    {
        string res = "\nAll Buyers deatails:\n----------------------------------------------------------- \n"; //32
        if (buyer_counter == 0)
        {
            res = "\nNo buyers\n";
            return res;
        }

        for (int i = 0; i < user_counter; i++)
        {
            Buyer temp = users[i] as Buyer;
            if (temp != null)
            {
                res += temp.ToString();
                res += temp.ShowOrderHistory() + "\n";
                res += "-----------------------------------------------------------\n";
            }

        }
        return res;
    }


    //#7

    public string DisplayAllSellers()
    {
        string res = "\nAll Sellers deatails:\n-----------------------------------------------------------\n";
        if (seller_counter == 0)
        {
            res = "\nNo sellers\n";
            return res;
        }
        for (int i = 0; i < user_counter; i++)
        {
            Seller temp = users[i] as Seller;
            if (temp != null)
            {
                res += temp.ToString();
                res += "-----------------------------------------------------------\n";
            }
        }
        return res;

    }

    public void SaveToFile(string file_path)
    {
        string user_name, Password, street, city, state, product_name;
        bool is_special_packaging = false;
        int building_number, product_ID;
        double price;
        Category category;
        StreamWriter sw = new StreamWriter(file_path);
        foreach (var item in users)
        {
            if (null != item as Seller)
            {
                user_name = item.GetUsername();
                Password = item.GetUserPassword();
                street = item.GetAddress().GetStreet();
                building_number = item.GetAddress().GetBuildingNumber();
                city = item.GetAddress().GetCity();
                state = item.GetAddress().GetState();
                sw.WriteLine(user_name + "\n" + Password + "\n" + street + "\n" + building_number + "\n" + city + "\n" + state + "\n");
                foreach (var product in item.GetProductCart())
                {
                    category = product.GetCategory();
                    product_ID = product.GetId();
                    product_name = product.GetProductName();
                    price = product.GetPrice();
                    sw.WriteLine(category + "\n" + product_ID + "\n" + product_name + "\n" + price);

                    PackagedProduct temp = product as PackagedProduct;
                    if (temp != null)
                    {
                        is_special_packaging = temp.GetIsSpecialPackaging();
                        sw.WriteLine(is_special_packaging);
                    }
                    sw.WriteLine();
                }
                sw.WriteLine();
            }
        }
        sw.Close();
    }

    public void LoadDataFromFile(string file_path)
    {

        // try
        // {

        string user_name, Password, street, city, state, product_name;
        bool is_special_packaging = false;
        int building_number, product_ID;
        double price;
        string category_choice;
        Address address;
        Seller seller;
        Product product;
        PackagedProduct packaged_product;
        // List<User> user;
        // List<Product> product_list = new List<Product>();

        int counter = 0;


        try
        {
            StreamReader reader = new StreamReader(file_path);

            string line = reader.ReadLine();
            while (line != null)
            {

                user_name = line;
                Password = reader.ReadLine();
                street = reader.ReadLine();
                building_number = int.Parse(reader.ReadLine());
                city = reader.ReadLine();
                state = reader.ReadLine();

                address = new Address(street, building_number, city, state);
                seller = new Seller(user_name, Password, address);
                AddSeller(seller);
                reader.ReadLine();
                line = reader.ReadLine();

                if (String.IsNullOrWhiteSpace(line))
                {
                    line = reader.ReadLine();
                    if (String.IsNullOrWhiteSpace(line))
                        break;
                    else
                        continue;
                }
                bool stop = false;
                counter = 0;
                while (stop != true)
                {
                    category_choice = line;

                    product_ID = int.Parse(reader.ReadLine());
                    product_name = reader.ReadLine();
                    price = double.Parse(reader.ReadLine());

                    line = reader.ReadLine();
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        is_special_packaging = false;
                        packaged_product = new PackagedProduct(product_name, price, user_name, CategoryToInt(category_choice), is_special_packaging, product_ID);
                        AddProductToSeller(packaged_product, seller);
                        counter++;
                        line = reader.ReadLine();
                        if (String.IsNullOrWhiteSpace(line))
                        {
                            line = reader.ReadLine();
                            if (String.IsNullOrWhiteSpace(line))

                                stop = true;
                        }
                    }
                    else
                    {
                        product = new Product(product_name, price, user_name, CategoryToInt(category_choice), product_ID);
                        AddProductToSeller(product, seller);
                        counter++;
                        line = reader.ReadLine();
                        if (String.IsNullOrWhiteSpace(line))
                        {
                            line = reader.ReadLine();
                            stop = true;
                        }
                    }
                }

            }

        }
        catch (Exception e)
        {
            System.Console.WriteLine("An error occurred: " + e.Message);
        }
    }

        public static int CategoryToInt(string category)
        {
            switch (category)
            {
                case "kids":
                    return 1;
                case "electricity":
                    return 2;
                case "office":
                    return 3;
                case "clothes":
                    return 4;
                default:
                    System.Console.WriteLine("Selected category does not exist");
                    return -1;
            }
        }
    }
}