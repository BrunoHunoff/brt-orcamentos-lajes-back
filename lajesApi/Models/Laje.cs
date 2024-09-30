public class Laje {
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public double Price { get; private set; }
    public double Weight { get; private set; }

    public Laje(string name, double price, double weight) {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Weight = weight;
    }

    public void UpdateLaje(String name, double price, double weight) {
        Name = name;
        Price = price;
        Weight = weight;
    }

    
}