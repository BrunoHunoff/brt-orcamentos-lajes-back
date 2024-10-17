using Microsoft.AspNetCore.Mvc;

public class Budget
{
    public int Id { get; init; }
    public int CostumerId { get; private set; }
    public string CostumerName { get; private set; }
    public double Footage { get; private set; }
    public double Value { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public int? FreightId { get; private set; }
    public Freight? Freight { get; private set; }
    public double FreightPrice { get; private set; }
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
        double value,
        string city,
        string state,
        double freightPrice,
        double administration,
        double profit,
        double taxes,
        double extra,
        Freight freight
        
    )
    {
        CostumerId = costumerId;
        CostumerName = costumerName;
        Footage = footage;
        Value = value;
        City = city;
        State = state;
        Freight = freight;
        Administration = administration;
        Profit = profit;
        Taxes = taxes;
        Extra = extra;
        if (freight is not null)
            FreightId = freight.Id;
        FreightPrice = freightPrice;
    }

    public Budget(
        int costumerId,
        string costumerName,
        double footage,
        double value,
        string city,
        string state,
        double administration,
        double profit,
        double taxes,
        double extra,
        int? freightId,
        double freightPrice
    )
    {
        CostumerId = costumerId;
        CostumerName = costumerName;
        Footage = footage;
        Value = value;
        City = city;
        State = state;
        Administration = administration;
        Profit = profit;
        Taxes = taxes;
        Extra = extra;
        FreightId = freightId;
        FreightPrice = freightPrice;
    }

    public Budget(
        int costumerId,
        string costumerName,
        double footage,
        double value,
        string city,
        string state,
        double administration,
        double profit,
        double taxes,
        double extra,
        double freightPrice
    )
    {
        CostumerId = costumerId;
        CostumerName = costumerName;
        Footage = footage;
        Value = value;
        City = city;
        State = state;
        Administration = administration;
        Profit = profit;
        Taxes = taxes;
        Extra = extra;
        Freight = null;
        FreightId = null;
        FreightPrice = freightPrice;
    }

    public async void UpdateBudget(
        int costumerId,
        string costumerName,
        double footage,
        double value,
        string city,
        string state,
        double administration,
        double profit,
        double taxes,
        double extra,
        int? freightId,
        double freightPrice
    )
    {
        CostumerId = costumerId;
        CostumerName = costumerName;
        Footage = footage;
        Value = value;
        City = city;
        State = state;
        Administration = administration;
        Profit = profit;
        Taxes = taxes;
        Extra = extra;
        FreightId = freightId;
        FreightPrice = freightPrice;
    }

    public void SetFreight(Freight freight)
    {
        Freight = freight;
        FreightId = freight?.Id;
    }

    public override string ToString()
    {
        return $"CustomerId: {CostumerId}, "
            + $"CustomerName: {CostumerName}, "
            + $"Footage: {Footage}, "
            + $"Value: {Value}, "
            + $"City: {City}, "
            + $"State: {State}, "
            + $"FreightId: {FreightId}, "
            + $"Freight: {Freight}";
    }
}
