class HotelBooking
{
    // Egenskaper för hotelbokning
    public string GuestName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double PricePerNight { get; set; }
    public int LengthOfStayInDays { get; set; }
    public double RabbattPrice { get; set; }

    public HotelBooking(string guestName, DateTime startDate, int lengthOfStayInDays)
    {
        GuestName = guestName;
        StartDate = startDate;
        // Räknar nuvarande dag och lägger till vad användaren har
        EndDate = startDate.AddDays(lengthOfStayInDays);
        LengthOfStayInDays = lengthOfStayInDays;
        PricePerNight = 100;

    }

    public void UpdateLengthOfStay()
    {
        Console.Write("Hur många dagar vill du lägga till? ");
        string newDay = Console.ReadLine();
        if (int.TryParse(newDay, out int day))
        {
            if (day <= 0)
            {
                Console.WriteLine("Talet måste vara över 0");
            }
            else
            {
                EndDate = StartDate.AddDays(day);
                Console.WriteLine($"Ny SlutDatum: {EndDate}");
            }

        }
        else
        {
            Console.WriteLine("Du måste ge en siffra");
        }
    }

    public double CalculateTotalPrice()
    {
        double totalPrice = (EndDate - StartDate).Days * PricePerNight;
        double rabbatPrice = 0.10;

        if (LengthOfStayInDays >= 3)
        {
            Console.WriteLine($" Hej, {GuestName}! You got 10% rabbat");
            return totalPrice * (1-rabbatPrice);
        }
        else {
            Console.WriteLine("Du missade rabbat");
            return totalPrice;
        }

    }
   

  public void DisplayBookingInfo()
  {
    Console.WriteLine($"Guest name: {GuestName}");
    Console.WriteLine($"Booking date: {StartDate} to {EndDate}");
    Console.WriteLine($"Length of stay in day: {LengthOfStayInDays}");
    Console.WriteLine($"Total price: {CalculateTotalPrice()}");
    
  }
}