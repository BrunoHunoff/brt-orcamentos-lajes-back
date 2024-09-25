public class Budget {
    public int Id { get; init; }
    public int CostumerId { get; private set; }
    public double Footage { get; private set; }
    public double Value { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public List<BudgetSlab> Slabs { get; private set; } = new List<BudgetSlab>();

    public Budget(int costumerId, double footage, double value, string city, string state) {
        CostumerId = costumerId;
        Footage = footage;
        Value = value;
        City = city;
        State = state;
    }
}