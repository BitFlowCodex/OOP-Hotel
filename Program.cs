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
            Person primaryGuest = new Person(guestName, guestEmail, guestPhone);


            HotelBooking? finalBooking = Book(primaryGuest, AskAboutExtraGuests());
            finalBooking?.DisplayBookingInfo();

            bool exit;
            do
            {
                finalBooking = Options(primaryGuest, finalBooking, out exit);
            } while (!exit);
        }

        private static HotelBooking? Book(Person primaryGuest, List<Person>? extraGuests = null)
        {
            HotelBooking? booking = null;

            do
            {
                try
                {
                    DateTime startDate = GetUserDateInput("Välj ett startdatum för din bokning. (YYYY-MM-DD)");
                    int lengthInDayslengthOfStayInDays = GetUserLengthInDaysInput();
                    HotelBooking tempBooking = new HotelBooking(primaryGuest, startDate, lengthInDayslengthOfStayInDays);
                    
                    if (extraGuests != null)
                    {
                        foreach (Person extraGuest in extraGuests)
                        {
                            if (!tempBooking.AddGuest(extraGuest))
                            {
                                Console.WriteLine($"{extraGuest.Name} är redan en gäst!");
                            }
                        }
                    }

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

        private static HotelBooking? Options(Person guestPerson, HotelBooking? finalBooking, out bool exit)
        {
            exit = false;
            int userPick;
            // 8 options
            string[] options = [
                "Avsluta",
                    "Lägg till dagar",
                    "Boka",
                    "Avboka",
                    "Visa bokning",
                    "Visa priser",
                    "Ändra kontaktuppgifter",
                    "Lägg till extra gäster"
            ];
            while (true)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {options[i]}");
                }

                if (int.TryParse(Console.ReadLine() ?? "", out userPick) && userPick >= 1 && userPick <= options.Length)
                {
                    break;
                }

                Console.WriteLine(Messages.InvalidInput);
            }

            switch (userPick)
            {
                case 1:
                    Console.WriteLine("Hej då!");
                    exit = true;
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
                        break;
                    }
                    Console.WriteLine(Messages.NoBookingInSystem);
                    break;
                case 5:
                    if (finalBooking == null)
                    {
                        Console.WriteLine(Messages.NoBookingInSystem);
                        break;
                    }
                    finalBooking.DisplayBookingInfo();
                    break;
                case 6:
                    Console.WriteLine("Priser:");
                    Console.WriteLine($"Pris för bokning: {HotelBooking.BasePrice}kr");
                    Console.WriteLine($"Yttligare kostnad per natt: {HotelBooking.BasePerNightPrice}kr");
                    Console.WriteLine($"Yttligare kostnad per extra person över {HotelBooking.GuestsWithoutExtraFee}: {HotelBooking.ExtraGuestFee}kr per natt");
                    Console.WriteLine($"Efter 3 nätter gäller 25% rabatt och 50% rabatt efter 7 nätter");
                    break;
                case 7:
                    Console.WriteLine("Vill du ändra ditt namn?");
                    if (GetUserYesOrNoInput())
                        guestPerson.SetName(GetUserStringInput("Vad vill du ange som ditt nya namn?"));

                    Console.WriteLine("Vill du ändra din email?");
                    if (GetUserYesOrNoInput())
                        guestPerson.SetEmail(GetUserStringInput("Vad vill du ange som din nya email?"));

                    Console.WriteLine("Vill du ändra ditt telefonnummer?");
                    if (GetUserYesOrNoInput())
                        guestPerson.SetPhone(GetUserStringInput("Vad vill du ange som ditt nya telefonnummer?"));
                    break;
                case 8:
                    var extraGuests = AskAboutExtraGuests();
                    if (extraGuests != null)
                    {
                        foreach (var extraGuest in extraGuests)
                        {
                            if (finalBooking != null && !finalBooking.AddGuest(extraGuest))
                            {
                                Console.WriteLine($"{extraGuest.Name} är redan en gäst!");
                            }
                        }
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

        private static bool GetUserYesOrNoInput()
        {
            while (true)
            {
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
            }
        }
        private static List<Person>? AskAboutExtraGuests()
        {
            Console.WriteLine("Vill du ha med dig några extra gäster?");
            bool userWantsExtraGuests = GetUserYesOrNoInput();
            if (!userWantsExtraGuests) return null;

            var extraGuests = new List<Person>();
            do
            {
                string extraGuestName = GetUserStringInput("Ange namnet på gästen:");
                Person extraGuest = new Person(extraGuestName);
                extraGuests.Add(extraGuest);
                Console.WriteLine("Vill du ha yttligare gäster?");
                userWantsExtraGuests = GetUserYesOrNoInput();
            } while (userWantsExtraGuests);

            return extraGuests;
        }
        private static bool GetUserWantsToBook(HotelBooking booking, int lengthInDayslengthOfStayInDays)
        {
            Console.WriteLine($"Priset på bokningen i {lengthInDayslengthOfStayInDays} dagar blir {booking.Price}kr. Är du nöjd med bokningen? (JA/NEJ)");

            return GetUserYesOrNoInput();
        }
    }
}