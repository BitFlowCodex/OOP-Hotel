namespace OOP_Hotel
{
    public class HotelBooking
    {
        public Person PrimaryGuest { get; private set; }
        public List<Person> Guests { get; private set; }
        public List<Person> NonPrimaryGuests => Guests.Where(guest => guest != PrimaryGuest).ToList();
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public double Price { get; private set; } = 0.0;
        public const double BasePerNightPrice = 150.0;
        public const double ExtraGuestFee = 50.0;
        public const int GuestsWithoutExtraFee = 2;
        public const double BasePrice = 200.0;
        public int LengthInDaysOfStay => (EndDate - StartDate).Days;

        public HotelBooking(Person primaryGuest, DateTime startDate, int lengthInDayslengthOfStayInDays)
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
            PrimaryGuest = primaryGuest;
            Guests = [primaryGuest];
            StartDate = startDate;
            EndDate = StartDate.AddDays(lengthInDayslengthOfStayInDays);
            //Price = lengthInDayslengthOfStayInDays * BasePerDayPrice + BasePrice;
            RecalculateTotalPrice();
        }
        private double CalculateNightPrice(int night, double nightPrice)
        {
            if (night <= 3)
                return nightPrice;

            if (night <= 7)
                return nightPrice * 0.75;

            return nightPrice * 0.5;
        }
        private void RecalculateTotalPrice()
        {
            double extraFee = Math.Max(0, Guests.Count - GuestsWithoutExtraFee) * ExtraGuestFee;

            double nightPrice = BasePerNightPrice + extraFee;

            double total = BasePrice;

            for (int night = 1; night <= LengthInDaysOfStay; night++)
            {
                total += CalculateNightPrice(night, nightPrice);
            }

            Price = total;
        }

        public void DisplayBookingInfo()
        {
            Console.WriteLine("Gästinfo:");
            Console.WriteLine($"Email: {PrimaryGuest.Email}");
            Console.WriteLine($"Telefon: {PrimaryGuest.Phone}");
            Console.WriteLine();

            if (NonPrimaryGuests.Count >= 1)
            {
                Console.WriteLine("Yttligare gäster:");
                foreach (Person guest in NonPrimaryGuests)
                {
                    Console.WriteLine($"Namn: {guest.Name}");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Bokingsinfo:");
            Console.WriteLine($"Startdatum: {StartDate.ToShortDateString()}");
            Console.WriteLine($"Slutdatum: {EndDate.ToShortDateString()}");
            Console.WriteLine();
            Console.WriteLine($"Totala priset: {Price}kr");
        }

        public bool AddDays(int days)
        {
            if (days < 1 || days > 365 || EndDate < DateTime.Today) return false;

            //if (EndDate > DateTime.MaxValue.AddDays(-days)) return false;

            try
            {
                EndDate = EndDate.AddDays(days);
                RecalculateTotalPrice();
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }

        }

        public bool AddGuest(Person person)
        {
            if (Guests.Contains(person))
                return false;

            Guests.Add(person);
            RecalculateTotalPrice();
            return true;
        }
    }
}