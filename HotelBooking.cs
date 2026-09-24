namespace OOP_Hotel
{
    public class HotelBooking
    {
        public Person Person { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public double Price { get; private set; } = 0.0;
        public double BaseDayPrice { get; private set; } = 150.0;
        public double BasePrice { get; private set; } = 200.0;

        public HotelBooking(Person person, DateTime startDate, int lengthInDayslengthOfStayInDays)
        {
            if (startDate < DateTime.Today)
            {
                throw new ArgumentOutOfRangeException(nameof(startDate), "Bokningsdatumet kan inte vara bakåt i tiden.");
            }
            if (lengthInDayslengthOfStayInDays < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lengthInDayslengthOfStayInDays), "Bokningen måste vara för mer än 1 dag");
            }

            DateTime maxAllowedDate = DateTime.Today.AddYears(5);

            if (startDate > maxAllowedDate)
            {
                throw new ArgumentOutOfRangeException(nameof(startDate), "Start datumet för långt fram i tiden.");
            }
            if (startDate > maxAllowedDate.AddDays(-lengthInDayslengthOfStayInDays))
            {
                throw new ArgumentOutOfRangeException(nameof(lengthInDayslengthOfStayInDays), "Slutdatumet sträcker sig för långt fram i tiden.");
            }
            Person = person;
            StartDate = startDate;
            EndDate = StartDate.AddDays(lengthInDayslengthOfStayInDays);
            Price = lengthInDayslengthOfStayInDays * BaseDayPrice + BasePrice;
        }

        public void DisplayBookingInfo()
        {
            Console.WriteLine("Gästinfo:");
            Console.WriteLine($"Namn: {Person.Name}");
            Console.WriteLine($"Email: {Person.Email}");
            Console.WriteLine($"Telefon: {Person.Phone}");
            Console.WriteLine($"Startdatum: {StartDate}");
            Console.WriteLine($"Slutdatum: {EndDate}");
            Console.WriteLine($"Totala priset: {Price}kr");
        }

        public bool AddDays(int days)
        {
            if (days < 1 || days > 365 || EndDate < DateTime.Today) return false;

            //if (EndDate > DateTime.MaxValue.AddDays(-days)) return false;

            try
            {
                EndDate = EndDate.AddDays(days);
                Price += BaseDayPrice * days;
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }

        }
    }
}