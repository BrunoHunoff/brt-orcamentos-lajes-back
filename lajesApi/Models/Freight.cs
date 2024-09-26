public class Freight {
    public int Id { get; init; }
    public string City { get; private set; }
    public string State { get; private set; }
    public double Price { get; private set; }

    public Freight(){}

    public Freight(string city, string state, double price) {
        City = city;
        State = state;
        Price = price;
    }

    public void Updatefreight(string city, string state, double price) {
        City = city;
        State = state;
        Price = price;
    }
}