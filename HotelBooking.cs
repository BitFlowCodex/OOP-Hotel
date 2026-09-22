class HotelBooking
{
  public string GuestName { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }

  public HotelBooking(string guestName, DateTime startDate, int lengthOfStayInDays)
  {
    GuestName = guestName;
    StartDate = startDate;
    EndDate = startDate.AddDays(lengthOfStayInDays);
  }

  

  public void test()
  {
    Console.WriteLine($"Namn: {GuestName}, StartDate: {StartDate}, EndDate: {EndDate}");
  }
}