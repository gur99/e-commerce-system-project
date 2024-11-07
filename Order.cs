//using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System;
namespace e_commerce_system
{

    class Order
    {
        private List<Product> shopping_cart;
        private double total_price = 0;
        private Buyer buyer_user;
        private int number_of_product;
        // protected static int last_order_id = 0;
        // protected int order_id;



        //C'tor
        public Order(Buyer buyer_user, List<Product> shopping_cart)
        {
            this.buyer_user = buyer_user;
            number_of_product = buyer_user.GetNumberOfProduct();
            if (number_of_product == 1)
            {
                throw new Exception("Cannot make a purchase with one product!");
            }
            this.shopping_cart = new List<Product>(shopping_cart);
            total_price = CalculateTotalPrice();
            // last_order_id++;
            // order_id = last_order_id;

        }
        public Order(Buyer buyer_user)
        {
            this.buyer_user = buyer_user;
            number_of_product = buyer_user.GetNumberOfProduct();
            // if (number_of_product == 1)
            // {
            //     throw new Exception("Cannot make a purchase with one product!");
            // }
            this.shopping_cart = new List<Product>(buyer_user.GetProductCart());
            total_price = CalculateTotalPrice();
        }


        //  Get
        public double GetTotalPrice() { return total_price; }
        public List<Product> GetShoppingCart() { return shopping_cart; }
        public Buyer Getbuyer() { return buyer_user; }
        public int GetNumberOfProduct() { return number_of_product; }
        // public int GetOrderId() { return order_id; }


        //Set
        public bool SetShoppingCart(List<Product> shopping_cart)
        {
            if (shopping_cart == null)
                return false;

            this.shopping_cart = shopping_cart;
            return true;
        }

        public bool SetTotalPrice(double total_price)
        {
            if (total_price < 0)
            {
                return false;
            }
            else
            {
                this.total_price = total_price;
                return true;
            }
        }
        public bool SetBuyerUser(Buyer buyer_user)
        {
            if (buyer_user == null)
                return false;

            this.buyer_user = buyer_user;
            return true;
        }

        public bool SetNumberOfProduct(int number_of_product)
        {
            if (number_of_product < 0)
                return false;

            return true;
        }

        public override string ToString()
        {
            string res;
            if (buyer_user.GetNumberOfProduct() == 0)
            {
                res = "\nNo product add to cart\n";
                return res;
            }
            res = "\nBuyer details:\n-----------------------------------------------------------\n" + buyer_user.ToString() + "\nTotal price: " + Math.Round(GetTotalPrice(), 2); //math -The use of the Class for elegant visibility only
            return res;
        }

        public double CalculateTotalPrice()
        {
            for (int i = 0; i < number_of_product; i++)
            {
                PackagedProduct temp = shopping_cart[i] as PackagedProduct;
                if (temp != null)
                {
                    total_price += temp.CalculateTotalPrice();
                }
                else
                    total_price += shopping_cart[i].GetPrice();
            }
            return total_price;
        }

        public override bool Equals(object other)
        {
            Order temp = other as Order;
            if (temp == null)
                return false;

            return buyer_user.Equals(temp.buyer_user);
        }
    }
}