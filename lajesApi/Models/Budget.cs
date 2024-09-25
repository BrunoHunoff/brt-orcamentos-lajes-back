public class Budget {
    public int Id { get; init; }
    public int CostumerId { get; private set; }
    public double Footage { get; private set; }
    public double Value { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }

    public int FreightId { get; private set; }
    
    public Freight Freight { get; private set; }

    public List<BudgetSlab> Slabs { get; private set; } = new List<BudgetSlab>();

    public Budget(int costumerId, double footage, double value, string city, string state, Freight freight) {
        CostumerId = costumerId;
        Footage = footage;
        Value = value;
        City = city;
        State = state;
        Freight = freight;
        FreightId = freight.Id;
    }

    public void UpdateBudget(int costumerId, double footage, double value, string city, string state, Freight freight, List<BudgetSlab> budgetSlabs) {
        CostumerId = costumerId;
        Footage = footage;
        Value = value;
        City = city;
        State = state;
        Freight = freight;
        FreightId = freight.Id;
        Slabs = budgetSlabs;
    }
}
