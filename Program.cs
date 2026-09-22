using System;

namespace OOP_Hotel
{
  class Program
  {
    static void Main(string[] args)
    {

      Console.Write("Enter guest name: ");
      string guestName = Console.ReadLine();
      if (guestName != "")
      {
        Console.WriteLine($"Guest name is: {guestName}");
      } 
      else
      {
        Console.WriteLine("Guest name is required");
      }

      Console.Write("Enter start date (yyyy-mm-dd): ");
      string dateString = Console.ReadLine();

      DateTime parsedDate;
      bool success = DateTime.TryParseExact(dateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out parsedDate);

      if (success)
      {
        Console.WriteLine($"Converted date: {parsedDate.ToShortDateString()}");
      }
      else
      {
        Console.WriteLine("Invalid date format");
      }

      Console.WriteLine("Enter length of stay in days: ");
      string stayDays = Console.ReadLine();

      int lengthOfStayInDays = int.Parse(stayDays);

      HotelBooking hotelboking = new HotelBooking(guestName, parsedDate, lengthOfStayInDays);

      hotelboking.test();
      // hotelboking.UpdateLengthOfStay();
      Console.WriteLine($"Total Price: {hotelboking.CalculateTotalPrice()}");
    }
  }
}