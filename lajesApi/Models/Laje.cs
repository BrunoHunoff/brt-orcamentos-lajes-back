using Microsoft.OpenApi.Any;

public class Laje {
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public double Price { get; private set; }

    public Laje(Guid id, string name, double price) {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
    }
}