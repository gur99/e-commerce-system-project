//using System.Reflection.Metadata.Ecma335;
using System.Collections;
using System.Collections.Generic;
namespace e_commerce_system
{

    class PackagedProduct : Product
    {
        private const double extra_price_for_packaging = 10.90;
        private bool is_special_packaging;
        private double total_price;

        public PackagedProduct(string product_name, double price, string original_seller, int category_choice, bool option_for_special_packaging) : base(product_name, price, original_seller, category_choice)
        {
            is_special_packaging = false;
            total_price = price;
        }
         public PackagedProduct(string product_name, double price, string original_seller, int category_choice, bool option_for_special_packaging, int id) : base(product_name, price, original_seller, category_choice, id)
         {
            is_special_packaging = false;
             total_price = price;
         }


        public PackagedProduct(PackagedProduct other) : base(other)
        {
            is_special_packaging = false;
            total_price = price;

        }




        public double CalculateTotalPrice()
        {
            double total_price = price;
            if (is_special_packaging)
            {
                total_price += extra_price_for_packaging;
                return total_price;
            }
            else
                return total_price;
        }

        //Get
        public double GetExtraPriceForPacking() { return extra_price_for_packaging; }
        public bool GetIsSpecialPackaging() { return is_special_packaging; }
        // Set
        public void SetIsSpecialPackaging(bool choice)
        {
            if (choice)
            {
                is_special_packaging = true;
            }
            else
                is_special_packaging = false;

        }


        //To String 
        public override string ToString()
        {
            string res;
            if (is_special_packaging)
            {
                res = "Category: " + product_category + "\t Product ID: " + id + "\t product name: " + product_name + " \t price: " + price + "\t +extra price for special packaging: " + extra_price_for_packaging + "\nseller-" + original_seller + "\n";
            }
            // CalculateTotalPrice()
            else
                res = base.ToString();

            return res;
        }
        public override bool Equals(object other)
        {
            PackagedProduct temp = other as PackagedProduct;
            if (temp == null)
                return false;

            return id.Equals(temp.id);
        }

    }
}
