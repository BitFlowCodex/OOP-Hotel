using System;

namespace OOP_Hotel
{
  class Program
  {
    static void Main(string[] args)
    {

      Console.WriteLine("Guest name: ");
      string guestName = Console.ReadLine();

      Console.WriteLine("Enter start date (yyyy-mm-dd): ");
      string date = Console.ReadLine();

      Console.WriteLine("Enter length of stay in days: ");
      string stayDays = Console.ReadLine();

      int lengthOfStayInDays = int.Parse(stayDays);

      DateTime startDate = DateTime.Parse(date);

      HotelBooking hotelboking = new HotelBooking(guestName, startDate, lengthOfStayInDays);

      hotelboking.test();
      hotelboking.UpdateLengthOfStay();
    }
  }
}