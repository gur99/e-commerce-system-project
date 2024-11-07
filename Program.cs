using System.ComponentModel;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using e_commerce_system;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System;
internal class Program
{

    // e_commerce_project P.4 :  E-KinGur  
    // Name:    Gur Yitzhaki
    // ID:      207888009     


    //MAIN
    private static void Main(string[] args)
    {

        string e_commerce_system_name = "E-KinGur";
        Administrative_Department e_kingur = new Administrative_Department(e_commerce_system_name);

        int user_choice = 0;
        string answer;

        string user_name, password, street, city, state, product_name;
        int building_number = 0;
        double product_price;
        bool is_special_packaging;
        int category_choice;

        Address address;
        Product product;

        Buyer buyer;
        Seller seller;

        int index_of_user, index_of_buyer, index_of_buyer2, index_of_seller;
        string seller_name, buyer_name, buyer_name2;
        bool ValidInput = false;

        string TXTFilePath = Directory.GetCurrentDirectory() + "\\e_kingur" + ".txt";
        e_kingur.LoadDataFromFile(TXTFilePath);
        Console.Clear();

        while (user_choice != 8)
        {
            System.Console.WriteLine("\n___________________________________________________________\n");
            Console.WriteLine($"Hi! welcome to {e_commerce_system_name} online store\n");
            Console.WriteLine("Menu:\n\n1.Add a buyer\n2.Add a seller\n3.Add a product for sale\n4.Add a product to shopping cart\n5.Order Payment(buyer)\n6.All buyers details\n7.All sellers details\n11.Compare 2 buyers\n8.Exit");
            System.Console.WriteLine("___________________________________________________________\n");
            user_choice = IntegerException();

            switch (user_choice)
            {

                //  Choice #1
                case 1:
                    Console.WriteLine("Please enter your username:");
                    user_name = StringException();
                    // Check if username already exist
                    index_of_buyer = e_kingur.IsUserExist(user_name);
                    if (index_of_buyer != -1)
                    {
                        System.Console.WriteLine("\nUsername already exist\n");
                        break;
                    }

                    Console.WriteLine("Please enter your password: ");
                    password = StringException();

                    Console.WriteLine("Address:\nPlease enter your Street:");
                    street = StringException();

                    Console.WriteLine("Please enter your building number: ");
                    building_number = IntegerException();

                    Console.WriteLine("Please enter your city:");
                    city = StringException();

                    Console.WriteLine("Please enter your state:");
                    state = StringException();

                    address = new Address(street, building_number, city, state);
                    buyer = new Buyer(user_name, password, address);
                    Console.Clear();

                    e_kingur += buyer;
                    // e_kingur.AddBuyer(buyer);

                    break;


                //  Choice #2
                case 2:

                    Console.WriteLine("Please enter your username:");
                    user_name = StringException();
                    // Check if username already exist
                    index_of_buyer = e_kingur.IsUserExist(user_name);
                    if (index_of_buyer != -1)
                    {
                        System.Console.WriteLine("\nUsername already exist\n");
                        break;
                    }

                    Console.WriteLine("Please enter your password: ");
                    password = StringException();

                    Console.WriteLine("Address:\nPlease enter your Street:");
                    street = StringException();

                    Console.WriteLine("Please enter your building number: ");
                    building_number = IntegerException();

                    Console.WriteLine("Please enter your city:");
                    city = StringException();

                    Console.WriteLine("Please enter your state:");
                    state = StringException();

                    address = new Address(street, building_number, city, state);
                    seller = new Seller(user_name, password, address);
                    Console.Clear();
                    e_kingur += seller;
                    // e_kingur.AddSeller(seller);
                    break;


                //  Choice #3
                case 3:

                    Console.WriteLine("Please enter your name:");
                    seller_name = StringException();

                    index_of_seller = e_kingur.IsSellerExist(seller_name);
                    if (index_of_seller == -1)
                    {
                        System.Console.WriteLine("\nSeller not found\n");
                        break;
                    }

                    System.Console.WriteLine("Please enter password: ");
                    password = StringException();


                    if (!e_kingur.GetUsers()[index_of_seller].IsPasswordCorrect(password))
                    {
                        System.Console.WriteLine("\nwrong password\n");
                        break;
                    }

                    Console.WriteLine("Please enter product name");
                    product_name = StringException();

                    if (e_kingur.GetProductOfSeller(seller_name, product_name) != null)
                    {
                        System.Console.WriteLine("\nProduct already exist in your cart\n");
                        break;
                    }

                    Console.WriteLine("What is your product price?");
                    product_price = DoubleException();

                    System.Console.WriteLine("\nPlease choose category for your product\n1) Kids\n2) Electricity\n3) office\n4) clothes\n");
                    category_choice = IntegerException();
                    if (category_choice != 1 && category_choice != 2 && category_choice != 3 && category_choice != 4)
                    {
                        System.Console.WriteLine("\nWrong input!\n");
                        break;
                    }

                    System.Console.WriteLine("would you like to add special packaging option?\n[Y/any other char]");
                    answer = StringException();
                    if (answer == "Y")
                    {
                        is_special_packaging = true;
                        product = new PackagedProduct(product_name, product_price, seller_name, category_choice, false);
                    }
                    else
                    {
                        is_special_packaging = false;
                        product = new Product(product_name, product_price, seller_name, category_choice);
                    }

                    seller = (Seller)e_kingur.GetUsers()[index_of_seller];
                    Console.Clear();
                    e_kingur.AddProductToSeller(product, seller);
                    break;


                // ADD Product To Buyer Cart
                case 4:

                    Console.WriteLine("Please enter your Username:");
                    buyer_name = StringException();

                    // Index of buyer
                    index_of_buyer = e_kingur.IsBuyerExist(buyer_name);
                    if (index_of_buyer == -1)
                    {
                        System.Console.WriteLine("\nBuyer not found\n");
                        break;
                    }

                    System.Console.WriteLine("Please enter password: ");
                    password = StringException();

                    // // Password test
                    if (!e_kingur.GetUsers()[index_of_buyer].IsPasswordCorrect(password))
                    {
                        System.Console.WriteLine("\nwrong password\n");
                        break;
                    }
                    System.Console.WriteLine("Select an option:(Enter 1 or otherwise)\n(1) Create a new cart from one of the carts in your shopping history\n(2) Continue to the store\n");
                    answer = StringException();
                    if (answer == "1")
                    {
                        buyer = e_kingur.GetBuyerByName(buyer_name);
                        Console.Clear();
                        System.Console.WriteLine(buyer.ShowOrderHistory());
                        if (buyer.GetOrderCounter() == 0)
                            break;
                        System.Console.WriteLine("Please choose your order number.\n");
                        user_choice = IntegerException();
                        if (e_kingur.AddCartFromHistoryToCurrent(buyer, user_choice) == false)
                        {
                            System.Console.WriteLine("choice was out of range");
                            break;
                        }
                        break;
                    }
                    else
                        Console.Clear();

                    //Shwo all products
                    System.Console.WriteLine("\nPlease choose a category number:\n1) Kids\n2) Electricity\n3) Office\n4) Clothes\n5) Show all categories");
                    category_choice = IntegerException();
                    if (category_choice == 5)
                    {
                        if (e_kingur.ShowAllSellersProduct() == false) break;
                    }
                    else if (category_choice == 1 || category_choice == 2 || category_choice == 3 || category_choice == 4)
                    {
                        if (e_kingur.ShowAllProductByCategory(category_choice) == null)
                        {
                            System.Console.WriteLine("\n\nNo product found for " + (Category)category_choice);
                            break;
                        }
                        System.Console.WriteLine(e_kingur.ShowAllProductByCategory(category_choice));
                    }
                    else
                    {
                        System.Console.WriteLine("\nWrong input\n");
                        break;
                    }

                    //Checking for product 
                    Console.WriteLine("Please enter a product name you would like to purchase:\n");
                    product_name = StringException();

                    if (e_kingur.IsMultipleProduct(product_name) > 1)
                    {
                        System.Console.WriteLine("There is a multiple sellers who sells: " + product_name + "\n" + "Which one would you prefer? -Enter seller name \n");
                        seller_name = StringException();
                        if (e_kingur.IsUserExist(seller_name) == -1)
                        {
                            System.Console.WriteLine("\nSeller doesnt Exist \n");
                            break;
                        }

                        product = e_kingur.GetProductOfSeller(seller_name, product_name);
                        if (product == null)
                        {
                            System.Console.WriteLine("\nThis seller doesnt sell: " + product_name + "\n");
                            break;
                        }
                    }

                    else if (e_kingur.IsMultipleProduct(product_name) == 1)
                    {
                        product = e_kingur.GetProductByNAme(product_name);
                    }

                    else
                    {
                        System.Console.WriteLine("\nProduct doesnt exist!\n");
                        break;
                    }


                    buyer = (Buyer)e_kingur.GetUsers()[index_of_buyer];
                    PackagedProduct temp = product as PackagedProduct;
                    if (temp != null)
                    {
                        temp = new PackagedProduct(temp);
                        System.Console.WriteLine("Would you like to add special packaging ? [Y/N]");
                        answer = StringException();

                        if (answer == "Y")
                        {
                            temp.SetIsSpecialPackaging(true);
                            product = temp;
                        }
                        else if (answer == "N")
                        {
                            PackagedProduct new_temp = new PackagedProduct(temp);
                            new_temp.SetIsSpecialPackaging(false);
                            product = new_temp;
                        }
                        else
                        {
                            System.Console.WriteLine("\nWrong input!\n");
                            break;
                        }

                    }
                    Console.Clear();
                    e_kingur.AddProductToBuyer(product, buyer);
                    break;

                case 5: //Order Payment (Buyer)

                    System.Console.WriteLine("Please enter your user name: ");
                    user_name = StringException();

                    index_of_buyer = e_kingur.IsBuyerExist(user_name);
                    if (index_of_buyer == -1)
                    {
                        System.Console.WriteLine("\nError username doesn't exist )\n");
                        break;
                    }

                    // Password test
                    System.Console.WriteLine("Please enter password: ");
                    password = StringException();

                    if (!e_kingur.GetUsers()[index_of_buyer].IsPasswordCorrect(password))
                    {
                        System.Console.WriteLine("\nwrong password\n");
                        break;
                    }
                    System.Console.WriteLine();

                    // Get Buyer 
                    buyer = (Buyer)e_kingur.GetUsers()[index_of_buyer];

                    if (e_kingur.GetUsers()[index_of_buyer].GetNumberOfProduct() == 0)
                    {
                        System.Console.WriteLine("\nYour Shopping cart is empty\n");
                        break;
                    }
                    try
                    {
                        e_kingur.OrderProcessForBuyer(buyer);
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.Message);
                        break;
                    }

                    answer = " ";
                    while (answer != "N" && answer != "Y")
                    {
                        System.Console.WriteLine("\nPay now? [Y/N]");
                        answer = StringException();
                        if (answer == "Y")
                        {
                            Console.Clear();
                            e_kingur.AddOrderToHistory(buyer);
                        }
                        else if (answer == "N")
                        {
                            Console.Clear();
                            System.Console.WriteLine("\nOK\n");
                        }
                        else
                        {
                            Console.Clear();
                            System.Console.WriteLine("\nWrong input\n");
                        }
                    }
                    break;

                case 6:
                    Console.Clear();
                    System.Console.WriteLine(e_kingur.DisplayAllBuyers());
                    break;

                case 7:
                    Console.Clear();
                    System.Console.WriteLine(e_kingur.DisplayAllSellers());
                    break;

                case 8:
                    Console.Clear();
                    System.Console.WriteLine("\nGoodbye :)\n");
                    e_kingur.SaveToFile(TXTFilePath);
                    break;

                case 11:
                    Console.Clear();
                    System.Console.WriteLine("\nPlease enter two buyer names you would like to compare.\nEnter first buyer:\n");
                    buyer_name = StringException();
                    index_of_buyer = e_kingur.IsBuyerExist(buyer_name);
                    if (index_of_buyer == -1)
                    {
                        System.Console.WriteLine("\nBuyer not found\n");
                        break;
                    }
                    System.Console.WriteLine("Enter second buyer:\n");
                    buyer_name2 = StringException();
                    index_of_buyer2 = e_kingur.IsBuyerExist(buyer_name2);
                    if (index_of_buyer2 == -1)
                    {
                        System.Console.WriteLine("\nBuyer not found\n");
                        break;
                    }

                    if (e_kingur.GetBuyerByName(buyer_name) > e_kingur.GetBuyerByName(buyer_name2))
                    {
                        System.Console.WriteLine(buyer_name + " has a shopping list of greater value than " + buyer_name2);
                    }
                    else
                        System.Console.WriteLine(buyer_name + " has a shopping list of lower or equal value than " + buyer_name2);

                    break;


                default:
                    Console.WriteLine("\nWrong input! Please try again\n");
                    break;
            }
        }
    }
    static int IntegerException()
    {
        int integer_input = 0;
        bool ValidInput = false;

        while (!ValidInput)
        {
            try
            {
                integer_input = int.Parse(Console.ReadLine());
                if (integer_input <= 0)
                {
                    throw new FormatException("Input must be a positive number.");
                }
                ValidInput = true;
            }

            catch (FormatException e)
            {
                System.Console.WriteLine("Input must be a number, try agian\n");
                Console.WriteLine("Please enter input again: ");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                Console.WriteLine("\nPlease enter input again: ");
            }
        }
        return integer_input;
    }
    static string StringException()
    {
        string string_input = null;
        bool ValidInput = false;

        while (!ValidInput)
        {
            try
            {
                string_input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(string_input))
                {
                    throw new FormatException("Input cannot be null or whitespace only.");
                }
                ValidInput = true;
            }

            catch (FormatException e)
            {
                System.Console.WriteLine(e.Message);
                Console.WriteLine("\nPlease enter input agian: ");
            }
        }
        return string_input;
    }
    static double DoubleException()
    {
        double double_input = 0.0;
        bool ValidInput = false;

        while (!ValidInput)
        {
            try
            {
                double_input = double.Parse(Console.ReadLine());

                if (double_input <= 0)
                {
                    throw new FormatException("Input must be a positive number.");
                }
                ValidInput = true;
            }


            catch (FormatException e)
            {
                System.Console.WriteLine("Input must be a number, try agian\n");
                Console.WriteLine("Please enter input again: ");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                Console.WriteLine("\nPlease enter input again: ");
            }
        }
        return double_input;
    }
}