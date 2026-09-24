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
      DateTime currentDate = DateTime.Now;
      if (parsedDate.CompareTo(currentDate) > 0)
      {
        Console.WriteLine("Date is earlier");
      } else
      {
        Console.WriteLine("Date is older");
      }
      

      Console.WriteLine("Enter length of stay in days: ");
      string stayDays = Console.ReadLine();

      int lengthOfStayInDays = int.Parse(stayDays);
      if (lengthOfStayInDays <= 0)
      {
        Console.WriteLine("Days must be over 0");
      }
      else
      {
        Console.WriteLine($"Days: {lengthOfStayInDays}");
      }

      HotelBooking hotelboking = new HotelBooking(guestName, parsedDate, lengthOfStayInDays);

      // hotelboking.UpdateLengthOfStay();
      hotelboking.DisplayBookingInfo();
            Console.ReadLine();
        }
       
  }
}