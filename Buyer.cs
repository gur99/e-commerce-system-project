using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using System;

namespace e_commerce_system
{

    class Buyer : User
    {
        private List<Order> order_history;
        private int order_counter;


        //   C'tor   -- Creating new Buyer User
        // public Buyer() { }
        public Buyer(string username, string password, Address address) : base(username, password, address)
        {
            order_counter = 0;
            order_history = new List<Order>();
            // order_history = new Order[size_of_order_history];
        }
        // Copy C'tor
        public Buyer(Buyer other) : base(other)
        {
            this.order_counter = other.order_counter;
            this.order_history = other.order_history;
        }

        // Operators

        public static bool operator <(Buyer a1, Buyer a2)
        {
            Buyer a = new Buyer(a1);
            Buyer b = new Buyer(a2);

            Order new_order1 = new Order(a);
            Order new_order2 = new Order(b);
            if (new_order1.CalculateTotalPrice() < new_order2.CalculateTotalPrice())
                return true;
            else
                return false;
        }

        public static bool operator >(Buyer a1, Buyer a2)
        {
            Buyer a = new Buyer(a1);
            Buyer b = new Buyer(a2);

            Order new_order1 = new Order(a);
            Order new_order2 = new Order(b);
            if (new_order1.CalculateTotalPrice() > new_order2.CalculateTotalPrice())
                return true;
            else
                return false;
        }


        public int GetOrderCounter() { return order_counter; }
        public List<Order> GetOrderHistory() { return order_history; }


        public bool SetNumberOfOrders(int order_counter)
        {
            if (order_counter < 0)
            {
                return false;
            }
            this.order_counter = order_counter;
            return true;
        }


        public bool SetOrderCounter(int order_counter)
        {
            if (order_counter < 0)
                return false;

            this.order_counter = order_counter;
            return true;
        }





        public void AddOrderToHistory(Order new_order)
        {
            order_history.Add(new_order);
            order_counter++;
            number_of_product = 0;
            System.Console.WriteLine("Shopping cart has been cleard\nOrder been added to history!\n");
        }



        //ToString
        public override string ToString()  //Buyer Details+ Cart details
        {
            string res = base.ToString() + "Password: " + password + "\n\nYour Cart:\n";

            if (number_of_product != 0)
            {
                for (int i = 0; i < number_of_product; i++)
                {
                    res += product_cart[i].ToString() + "\n\n";
                }
            }
            else
            {
                res = res + "\n--is empty--\n\n";
            }
            return res;
        }


        public string ShowOrderHistory()
        {
            string res = "Order Histroy:\n";
            if (order_counter == 0)
            {
                res += "\n--is empty\n";
                return res;
            }
            else
            {

                for (int i = 0; i < order_counter; i++)
                {
                    res += "\nOrder number: " + (i + 1) + "\nProducts:\n";
                    for (int j = 0; j < order_history[i].GetNumberOfProduct(); j++)
                        res += order_history[i].GetShoppingCart()[j].ToString() + "\n";
                    res += "Total price " + Math.Round(order_history[i].GetTotalPrice(), 2) + "\n\n";
                }
            }
            return res;
        }

        public override bool Equals(object other)
        {
            Buyer temp = other as Buyer;
            if (temp == null)
                return false;

            return username.Equals(temp.username);
        }



    }
}

