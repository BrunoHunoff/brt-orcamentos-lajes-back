public class Budget
{
    public int Id { get; init; }
    public int CostumerId { get; private set; }
    public string CostumerName { get; private set; }
    public double Footage { get; private set; }
    public double TotalWeight { get; private set; }
    public double Cost { get; private set; }
    public double SellPrice { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public double FreightWeight { get; private set; }
    public string FreightType { get; private set; }
    public double? FreightPrice { get; private set; }
    public double Administration { get; private set; }
    public double Profit { get; private set; }
    public double Taxes { get; private set; }
    public double Extra { get; private set; }

    public List<BudgetSlab> Slabs { get; private set; } = new List<BudgetSlab>();

    public Budget() { }

    public Budget(
        int costumerId,
        string costumerName,
        double footage,
        double totalWeight,
        double cost,
        double sellPrice,
        string city,
        string state,
        double freightWeight,
        string freightType,
        double? freightPrice,
        double administration,
        double profit,
        double taxes,
        double extra
        
    )
    {
        CostumerId = costumerId;
        CostumerName = costumerName;
        Footage = footage;
        TotalWeight = totalWeight;
        Cost = cost;
        SellPrice = sellPrice;
        City = city;
        State = state;
        FreightWeight = freightWeight;
        FreightType = freightType;
        FreightPrice = freightPrice;
        Administration = administration;
        Profit = profit;
        Taxes = taxes;
        Extra = extra;
    }

    public async void UpdateBudget(
        int costumerId,
        string costumerName,
        double footage,
        double totalWeight,
        double sellPrice,
        string city,
        string state,
        double freightWeight,
        string freightType,
        double? freightPrice,
        double administration,
        double profit,
        double taxes,
        double extra
    )
    {
        CostumerId = costumerId;
        CostumerName = costumerName;
        Footage = footage;
        TotalWeight = totalWeight;
        SellPrice = sellPrice;
        City = city;
        State = state;
        FreightWeight = freightWeight;
        FreightType = freightType;
        FreightPrice = freightPrice;
        Administration = administration;
        Profit = profit;
        Taxes = taxes;
        Extra = extra;
        
    }

    public override string ToString()
    {
        return $"CustomerId: {CostumerId}, "
            + $"CustomerName: {CostumerName}, "
            + $"Footage: {Footage}, "
            + $"Value: {SellPrice}, "
            + $"City: {City}, "
            + $"State: {State}";
    }
}
