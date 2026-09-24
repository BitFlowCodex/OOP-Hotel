using System;
using System.Globalization;
using System.Net.Http.Headers;

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


            HotelBooking? finalBooking = Book(guestPerson);
            finalBooking?.DisplayBookingInfo();

            while (true)
            {
                finalBooking = Options(guestPerson, finalBooking);
            }
        }

        private static HotelBooking? Book(Person guestPerson)
        {
            HotelBooking? booking = null;

            do
            {
                try
                {
                    DateTime startDate = GetUserDateInput("Välj ett startdatum för din bokning. (YYYY-MM-DD)");
                    int lengthInDayslengthOfStayInDays = GetUserLengthInDaysInput();
                    HotelBooking tempBooking = new HotelBooking(guestPerson, startDate, lengthInDayslengthOfStayInDays);
                    if (GetUserWantsToBook(tempBooking, lengthInDayslengthOfStayInDays))
                    {
                        Console.WriteLine("Systemet har hanterat din bokning!");
                        booking = tempBooking;
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
            } while (booking == null);

            return booking;
        }

        private static HotelBooking? Options(Person guestPerson, HotelBooking? finalBooking)
        {
            string noBookingInSystem = "Du har ingen bokning i systemet.";
            int userPick;
            while (true)
            {
                Console.WriteLine("1. Avsluta");
                Console.WriteLine("2. Lägg till dagar");
                Console.WriteLine("3. Boka");
                Console.WriteLine("4. Avboka");
                Console.WriteLine("5. Visa Bokning");

                if (int.TryParse(Console.ReadLine() ?? "", out userPick) && userPick >= 1 && userPick <= 5)
                {
                    break;
                }

                Console.WriteLine(Messages.InvalidInput);
            }

            switch (userPick)
            {
                case 1:
                    Console.WriteLine("Hej då!");
                    Environment.Exit(0);
                    break;

                case 2:
                    while (true)
                    {
                        Console.WriteLine("Hur många dagar vill du lägga till? (1-365 dagar)");
                        if (int.TryParse(Console.ReadLine(), out int days) && days >= 1 && days <= 365)
                        {
                            if (finalBooking != null && finalBooking.AddDays(days))
                            {
                                Console.WriteLine($"Du lade till {days} dagar till din bokning! Det nya priset är {finalBooking.Price}kr.");
                            }
                            else
                            {
                                Console.WriteLine("Något blev fel med ökningen.");
                            }
                            break;
                        }
                        Console.WriteLine(Messages.InvalidInput);
                    }
                    break;

                case 3:
                    if (finalBooking != null)
                    {
                        Console.WriteLine("Du måste avboka innan du kan göra en ny bokning.");
                    }
                    else
                    {
                        finalBooking = Book(guestPerson);
                        finalBooking?.DisplayBookingInfo();
                    }
                    break;

                case 4:
                    if (finalBooking != null)
                    {
                        finalBooking = null;
                        Console.WriteLine("Din bokning har tagits bort från systemet.");
                    }
                    else
                    {
                        Console.WriteLine(noBookingInSystem);
                    }
                    break;
                case 5:
                    if (finalBooking == null)
                        Console.WriteLine(noBookingInSystem);
                    else
                    {
                        finalBooking.DisplayBookingInfo();
                    }
                    break;
            }
            return finalBooking;
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
                    Console.WriteLine(Messages.InvalidInput);
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
                    Console.WriteLine(Messages.InvalidInput);
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
                        Console.WriteLine("Bokningen måste vara minst 1 dag.");
                        continue;
                    }
                    return userInt;
                }
                else
                {
                    Console.WriteLine(Messages.InvalidInput);
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