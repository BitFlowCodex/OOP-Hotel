using System;
using System.Globalization;

namespace OOP_Hotel
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hej och välkommen till OOP Hotellet!");
            string guestName = GetUserStringInput("Vad är ditt namn?");
            string guestEmail = GetUserStringInput("Vad är din email?");
            string guestPhone = GetUserStringInput("Vad är ditt telefonnummer?");
            Person guestPerson = new Person(guestName, guestEmail, guestPhone);

            HotelBooking? finalBooking = null;
            do
            {
                try
                {
                    DateTime startDate = GetUserDateInput("Välj ett startdatum för din bokning. (YYYY-MM-DD)");
                    int lengthInDayslengthOfStayInDays = GetUserLengthInDaysInput();
                    HotelBooking booking = new HotelBooking(guestPerson, startDate, lengthInDayslengthOfStayInDays);
                    if (GetUserWantsToBook(booking, lengthInDayslengthOfStayInDays))
                    {
                        Console.WriteLine("Systemet har hanterat din bokning!");
                        finalBooking = booking;
                    }
                    else
                    {
                        Console.WriteLine("Systemet begär dig att göra om din bokning!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Det blev något fel i din bokning, vänligen försök igen.");
                    Console.WriteLine($"Fel: {ex.Message}");
                }
            } while (finalBooking == null);


            finalBooking.DisplayBookingInfo();
        }

        private static string GetUserStringInput(string question)
        {
            do
            {
                Console.WriteLine(question);
                string userInput = Console.ReadLine() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(userInput))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("Du gav en felaktig inmatning");
                }
            } while (true);
        }

        private static DateTime GetUserDateInput(string question)
        {
            do
            {
                Console.WriteLine(question);
                string userInput = Console.ReadLine() ?? string.Empty;

                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
                {
                    if (DateTime.Compare(startDate, DateTime.Today) < 0)
                    {
                        Console.WriteLine("Du kan inte välja ett datum bak i tiden.");
                        continue;
                    }
                    return startDate;
                }
                else
                {
                    Console.WriteLine("Du gav en felaktig inmatning.");
                }
            } while (true);
        }

        private static int GetUserLengthInDaysInput()
        {
            do
            {
                Console.WriteLine("Hur många dagar vill du stanna?");
                string userInput = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(userInput, out int userInt))
                {
                    if (userInt < 1)
                    {
                        Console.WriteLine("Bokningen måste vara minst 1 dag");
                        continue;
                    }
                    return userInt;
                }
                else
                {
                    Console.WriteLine("Du gav en felaktig inmatning");
                }
            } while (true);
        }

        private static bool GetUserWantsToBook(HotelBooking booking, int lengthInDayslengthOfStayInDays)
        {
            do
            {
                Console.WriteLine($"Priset på bokningen i {lengthInDayslengthOfStayInDays} dagar blir {booking.Price}kr. Är du nöjd med bokningen? (JA/NEJ)");

                string userInput = Console.ReadLine() ?? "";

                if (userInput.ToLower() == "ja")
                {
                    return true;
                }
                else if (userInput.ToLower() == "nej")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Du måste skriva JA eller NEJ");
                }
            } while (true);
        }
    }
}