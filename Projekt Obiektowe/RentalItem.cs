using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Obiektowe
{
    class RentalItem
    {


        public string Name { get; set; }
        public double Price { get; set; }
        [Ignore] public int RentalDays { get; set; }

        public double TotalCost
        {
            get { return Price * RentalDays; }
        }

        public RentalItem(string Name, double Price)
        {
            this.Name = Name;
            this.Price = Price;
            RentalDays = 0;
        }
    }
}
