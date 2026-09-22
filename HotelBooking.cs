class HotelBooking
{
  // Egenskaper för hotelbokning
  public string GuestName { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }


  public HotelBooking(string guestName, DateTime startDate, int lengthOfStayInDays)
  {
    GuestName = guestName;
    StartDate = startDate;
    // Räknar nuvarande dag och lägger till vad användaren har
    EndDate = startDate.AddDays(lengthOfStayInDays);

  }

  public void UpdateLengthOfStay()
  {
    Console.Write("Hur många dagar vill du lägga till? ");
    string newDay = Console.ReadLine();
    if (int.TryParse(newDay, out int day))
    {
      EndDate = StartDate.AddDays(day);
      Console.WriteLine($"Ny SlutDatum: {EndDate}");
    }
    else
    {
      Console.WriteLine("Du måste ge en siffra");
    }
  }

  public void CalculateTotalPrice()
  {

  }

  public void DisplayBookingInfo()
  {

  }

  public void test()
  {
    Console.WriteLine($"Namn: {GuestName}, StartDate: {StartDate}, EndDate: {EndDate}");
  }
}