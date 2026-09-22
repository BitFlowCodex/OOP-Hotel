namespace OOP_Hotel
{
    public class Person
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        public Person(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }
    }

}