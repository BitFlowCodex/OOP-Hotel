class HotelBooking
{
  public string GuestName { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public double PricePerNight { get; set; }
  public int LengthOfStayInDays { get; set; }

  public HotelBooking(string guestName, DateTime startDate, int lengthOfStayInDays)
  {
    GuestName = guestName;
    StartDate = startDate;
    EndDate = startDate.AddDays(lengthOfStayInDays);
    LengthOfStayInDays = lengthOfStayInDays;
    PricePerNight = 100;
  }

  public void UpdateLengthOfStay()
  {
    Console.Write("Hur många dagar vill du lägga till? ");
    string? newLengthOfStayInput = Console.ReadLine();
    if (int.TryParse(newLengthOfStayInput, out int newLengthOfStay))
    {
      if (newLengthOfStay <= 0)
      {
        Console.WriteLine("Talet måste vara över 0");
      }
      else
      {
        EndDate = StartDate.AddDays(newLengthOfStay);
        LengthOfStayInDays = newLengthOfStay;
        Console.WriteLine($"Ny SlutDatum: {EndDate}, {LengthOfStayInDays} ");
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
    if (totalPrice <= 0) { return 0; }
    else { return totalPrice; }
  }

  public void DisplayBookingInfo()
  {
    Console.WriteLine($"Guest name: {GuestName}");
    Console.WriteLine($"Booking date: {StartDate} to {EndDate}");
    Console.WriteLine($"Length of stay in day: {LengthOfStayInDays}");
    Console.WriteLine($"Total price: {CalculateTotalPrice()}");

  }
}