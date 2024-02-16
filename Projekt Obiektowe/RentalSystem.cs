using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Projekt_Obiektowe
{
    class RentalSystem
    {
        private List<RentalItem> availableItems;
        private List<RentalItem> rentedItems;
        private string filePath;

        public RentalSystem(string filePath)
        {
            this.filePath = filePath;
            availableItems = new List<RentalItem>();
            rentedItems = new List<RentalItem>();
            LoadItemsFromCsv(filePath);
        }

        public List<RentalItem> GetAvailableItems()
        {
            return availableItems;
        }

        public void AddItem(string name, double price)
        {
            RentalItem newItem = new RentalItem(name, price);
            availableItems.Add(newItem);
            Console.WriteLine($"Dodano nowy sprzęt: {name} - Cena: {price} zł/dzień");
            SaveItemsToCsv(filePath);
        }

        public void RemoveItem(string name)
        {
            RentalItem itemToRemove = null;

            foreach (var item in availableItems)
            {
                if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    itemToRemove = item;
                    break;
                    SaveItemsToCsv(filePath);
                }
            }

            if (itemToRemove != null)
            {
                availableItems.Remove(itemToRemove);
                Console.WriteLine($"Usunięto sprzęt: {name}");
                SaveItemsToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Nie znaleziono sprzętu o nazwie: {name}");
            }
        }

        public void ChangeItemPrice(string name, double newPrice)
        {
            RentalItem itemToChangePrice = null;

            foreach (var item in availableItems)
            {
                if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    itemToChangePrice = item;
                    break;
                }
            }

            if (itemToChangePrice != null)
            {
                itemToChangePrice.Price = newPrice;
                Console.WriteLine($"Zmieniono cenę sprzętu {name} na {newPrice} zł/dzień");
                SaveItemsToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Nie znaleziono sprzętu o nazwie: {name}");
            }
        }

        public void RentItem(string name, int rentalDays)
        {
            RentalItem itemToRent = null;

            foreach (var item in availableItems)
            {
                if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    itemToRent = item;
                    break;
                }
            }

            if (itemToRent != null)
            {
                availableItems.Remove(itemToRent);
                itemToRent.RentalDays = rentalDays;
                rentedItems.Add(itemToRent);
                Console.WriteLine($"Wypożyczono {name} na {rentalDays} dni.");
            }
            else
            {
                Console.WriteLine($"Nie znaleziono sprzętu o nazwie: {name}");
            }
        }

        public void GenerateReceipt()
        {
            double totalCost = 0.0;

            foreach (var item in rentedItems)
            {
                Console.WriteLine($"{item.Name} - Cena: {item.Price} zł - Ilość dni: {item.RentalDays} - Koszt: {item.TotalCost} zł");
                totalCost += item.TotalCost;
            }

            Console.WriteLine($"Łączny koszt: {totalCost} zł");
        }

        private void LoadItemsFromCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                availableItems = csv.GetRecords<RentalItem>().ToList();
            }
        }

        private void SaveItemsToCsv(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(availableItems);
                
            }
        }
    }
}
