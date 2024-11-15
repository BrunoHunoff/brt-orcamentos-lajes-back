public class Slab {
    public int Id { get; init; }
    public string Name { get; private set; }
    public double Price { get; private set; }
    public double Weight { get; private set; }

    public Slab(){}

    public Slab(string name, double price, double weight) {
        Name = name;
        Price = price;
        Weight = weight;
    }

    public void UpdateSlab(String name, double price, double weight) {
        Name = name;
        Price = price;
        Weight = weight;
    }   

    
}