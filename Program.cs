using System;

namespace OOP_Hotel
{
  class Program
  {
    static void Main(string[] args)
    {

      Console.Write("Enter guest name: ");
      string? guestName = Console.ReadLine();
      if (guestName != "")
      {
        Console.WriteLine($"Guest name is: {guestName}");
      }
      else
      {
        Console.WriteLine("Guest name is required");
      }

      Console.Write("Enter start date (yyyy-mm-dd): ");
      string? dateString = Console.ReadLine();

      DateTime parsedDate;
      bool isValidDate = DateTime.TryParseExact(dateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out parsedDate);

      if (isValidDate)
      {
        Console.WriteLine($"Converted date: {parsedDate.ToShortDateString()}");
      }
      else
      {
        Console.WriteLine("Invalid date format");
      }

      if (parsedDate.Date < DateTime.Now.Date)
      {
        Console.WriteLine("Start date cannot be in the past!");
      }


      Console.WriteLine("Enter length of stay in days: ");
      string? stayDaysInput = Console.ReadLine();

      if (int.TryParse(stayDaysInput, out int lengthOfStayInDays))
      {
        if (lengthOfStayInDays > 0)
        {
          Console.WriteLine($"Days: {lengthOfStayInDays}");
        }
        else
        {
          Console.WriteLine("Days must be over 0");
        }
      }
      else
      {
        Console.WriteLine("Please enter a valid number");
      }

      HotelBooking hotelBooking = new HotelBooking(guestName, parsedDate, lengthOfStayInDays);

      // hotelboking.UpdateLengthOfStay();
      hotelBooking.DisplayBookingInfo();
    }
  }
}