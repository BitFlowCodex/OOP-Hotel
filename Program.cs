using System;

namespace OOP_Hotel
{
    class Program
  {
    static void Main(string[] args)
    {
      HotelBooking hotelboking = new HotelBooking("jack minayerdji", DateTime.Now, 5);

      hotelboking.test();
    }
  }
}