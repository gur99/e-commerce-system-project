using System.Collections;
using System.Collections.Generic;
namespace e_commerce_system { 
abstract class User
{
    protected string username;
    protected string password;
    protected Address address;
    protected List<Product> product_cart;
    protected int number_of_product;

    //C'tor
    public User(string username, string password, Address address)
    {
        SetUsername(username);
        SetPassword(password);
        this.address = new Address(address);
        number_of_product = 0;
        product_cart = new List<Product>();
    }

    //COPY C'tor
    public User(User other)
    {
        this.username = other.username;
        this.password = other.password;
        this.address = other.address;
        this.number_of_product = other.number_of_product;
        this.product_cart = other.product_cart;
    }


    //SET

    public bool SetUsername(string username)
    {
        if (username == " " || username == null || username == "")
        {
            System.Console.WriteLine("username cannot be a space or an empty field");
            return false;
        }
        this.username = username;
        return true;
    }
    public bool SetPassword(string password)
    {
        if (password == " " || password == null || password == "")
        {
            System.Console.WriteLine("Password cannot be a space or an empty field");
            return false;
        }

        this.password = password;
        return true;
    }
    public bool SetAddress(Address address)
    {
        if (address == null)
        {
            return false;
        }

        this.address = address;
        return true;
    }

    //GET
    public string GetUsername() { return username; }
    public string GetUserPassword() { return password; }
    public Address GetAddress() { return address; }
    public List<Product> GetProductCart() { return product_cart; }
    public int GetNumberOfProduct() { return number_of_product; }



    //ToString
    public virtual new string ToString() { return GetType().Name + "\nUsername: " + username + "\nAddress: \t" + address.ToString(); }


    public void AddProductToCart(Product new_product)
    {

        product_cart.Add(new_product);
        number_of_product++;
        System.Console.WriteLine("\nSuccessfully added product!\n");
    }

    public void ClearProductCart()
    {
        product_cart.Clear();
        number_of_product = 0;
    }


    public bool IsPasswordCorrect(string password)
    {
        if (this.password == password)
            return true;

        return false;
    }

    public override bool Equals(object other)
    {
        Seller temp = other as Seller;
        if (temp == null)
        {
            Buyer temp_two = other as Buyer;
            if (temp_two == null)
                return false;
        }
        return username.Equals(temp.username);
    }

}
}