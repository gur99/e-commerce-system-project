using System.Collections;
using System.Collections.Generic;
namespace e_commerce_system
{
    class Seller : User
    {

        public Seller(string username, string password, Address address) : base(username, password, address) { }

        public Seller(Seller other) : base(other) { }



        public override string ToString()
        {
            string res = "Seller details:\n\n" + base.ToString() + "Password: " + password + "\n\nSelling Cart:\n";

            if (number_of_product != 0)
            {
                for (int i = 0; i < number_of_product; i++)
                {
                    PackagedProduct temp = GetProductCart()[i] as PackagedProduct;
                    if (temp != null)
                    {
                        temp.SetIsSpecialPackaging(true);
                        res += temp.ToString() + "\n";
                        temp.SetIsSpecialPackaging(false);
                    }
                    else
                        res += product_cart[i].ToString() + "\n\n";
                }

            }
            else
                res = res + "\n--is empty--\n\n";

            return res;
        }

        public override bool Equals(object other)
        {
            Seller temp = other as Seller;
            if (temp == null)
                return false;
            return username.Equals(temp.username);
        }

    }
}




