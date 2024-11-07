using System.Collections;
using System.Collections.Generic;
namespace e_commerce_system
{

    class Address
    {
        //Properties
        private string street;
        private int building_number;
        private string city;
        private string state;

        //C'tor
        public Address(string street, int building_number, string city, string state)
        {
            SetStreet(street);

            SetBuildingNumber(building_number);

            SetCity(city);

            SetState(state);
        }
        //COPY C'tor
        public Address(Address other)
        {
            street = other.street;

            building_number = other.building_number;

            city = other.city;

            state = other.state;
        }

        //SET
        public bool SetStreet(string street)
        {
            if (street == " " || street == null || street == "")
                return false;

            this.street = street;
            return true;
        }
        public bool SetBuildingNumber(int building_number)
        {
            if (building_number < 1)
                return false;

            this.building_number = building_number;
            return true;
        }
        public bool SetCity(string city)
        {
            if (city == " " || city == null || city == "")
                return false;

            this.city = city;
            return true;
        }
        public bool SetState(string state)
        {
            if (state == null || state == " " || state == "")
                return false;

            this.state = state;
            return true;
        }

        //GET
        public string GetStreet() { return this.street; }
        public int GetBuildingNumber() { return this.building_number; }
        public string GetCity() { return this.city; }
        public string GetState() { return this.state; }

        //ToString
        public override string ToString() { return " street: " + street + "\t building number: " + building_number + "\t city: " + city + "\t state: " + state + "\n"; }


        public override bool Equals(object other)
        {
            Address temp = other as Address;
            if (temp == null)
                return false;

            return street.Equals(temp.street) && city.Equals(temp.city) && building_number.Equals(temp.building_number) && state.Equals(temp.state);
        }

    }
}
