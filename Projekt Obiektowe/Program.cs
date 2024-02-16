using System;
using System.Collections.Generic;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Projekt_Obiektowe;

class Program
{
    static void Main()
    {
        string filePath = "sample.csv";

        RentalSystem rentalSystem = new RentalSystem(filePath);

        Console.WriteLine("Witaj w Wypożyczalni Sprzętu Zimowego!");

        Console.Write("Czy jesteś właścicielem (W) czy klientem (K)? ");
        char choice = Console.ReadKey().KeyChar;

        if (char.ToUpper(choice) == 'W')
        {
            Console.Write("\nPodaj hasło właściciela: ");
            string password = Console.ReadLine();

            if (password == "admin")
            {
                Console.WriteLine("\nZalogowano jako właściciel.");
                ShowOwnerMenu(rentalSystem);
            }
            else
            {
                Console.WriteLine("\nNieprawidłowe hasło. Zamykanie programu.");
            }
        }
        else if (char.ToUpper(choice) == 'K')
        {
            Console.WriteLine("\nLista dostępnych przedmiotów:");

            foreach (var item in rentalSystem.GetAvailableItems())
            {
                Console.WriteLine($"{item.Name} - Cena: {item.Price} zł");
            }

            Console.WriteLine("\nWybierz przedmiot, który chcesz wypożyczyć (wpisz nazwę): ");
            string itemName = Console.ReadLine();

            Console.Write($"Podaj ilość dni wypożyczenia dla {itemName}: ");
            int rentalDays = int.Parse(Console.ReadLine());

            rentalSystem.RentItem(itemName, rentalDays);
            Console.WriteLine("\nParagon:");
            rentalSystem.GenerateReceipt();
        }
        else
        {
            Console.WriteLine("\nNieprawidłowy wybór. Zamykanie programu.");
        }
    }

    static void ShowOwnerMenu(RentalSystem rentalSystem)
    {
        while (true)
        {
            Console.WriteLine("\nMenu właściciela:");
            Console.WriteLine("1. Dodaj nowy sprzęt");
            Console.WriteLine("2. Usuń sprzęt");
            Console.WriteLine("3. Zmień cenę sprzętu");
            Console.WriteLine("4. Wyświetl dostępne przedmioty");
            Console.WriteLine("5. Zakończ");

            Console.Write("Wybierz opcję (1-5): ");
            char option = Console.ReadKey().KeyChar;

            switch (option)
            {
                case '1':
                    Console.Write("\nPodaj nazwę nowego sprzętu: ");
                    string newItemName = Console.ReadLine();
                    Console.Write($"Podaj cenę dla {newItemName}: ");
                    double newItemPrice = double.Parse(Console.ReadLine());
                    rentalSystem.AddItem(newItemName, newItemPrice);
                    break;
                case '2':
                    Console.Write("\nPodaj nazwę sprzętu do usunięcia: ");
                    string itemToRemove = Console.ReadLine();
                    rentalSystem.RemoveItem(itemToRemove);
                    break;
                case '3':
                    Console.Write("\nPodaj nazwę sprzętu do zmiany ceny: ");
                    string itemToChangePrice = Console.ReadLine();
                    Console.Write($"Podaj nową cenę dla {itemToChangePrice}: ");
                    double newPrice = double.Parse(Console.ReadLine());
                    rentalSystem.ChangeItemPrice(itemToChangePrice, newPrice);
                    break;
                case '4':
                    Console.WriteLine("\nDostępne przedmioty:");
                    foreach (var item in rentalSystem.GetAvailableItems())
                    {
                        Console.WriteLine($"{item.Name} - Cena: {item.Price} zł");
                    }
                    break;
                case '5':
                    Console.WriteLine("\nZamykanie programu.");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nNieprawidłowa opcja.");
                    break;
            }
        }
    }
}