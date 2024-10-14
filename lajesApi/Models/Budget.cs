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

    public List<BudgetSlab> Slabs { get; private set; } = new List<BudgetSlab>();

    public Budget() { }

    public Budget(int costumerId, string costumerName, double footage, double value, string city, string state, Freight freight)
    {
        CostumerId = costumerId;
        CostumerName = costumerName;
        Footage = footage;
        Value = value;
        City = city;
        State = state;
        Freight = freight;
        if (freight is not null) FreightId = freight.Id;
    }

    public Budget(int costumerId, string costumerName, double footage, double value, string city, string state, int? freightId)
    {
        CostumerId = costumerId;
        CostumerName = costumerName;
        Footage = footage;
        Value = value;
        City = city;
        State = state;
        FreightId = freightId;
    }

    public Budget(int costumerId, string costumerName, double footage, double value, string city, string state)
    {
        CostumerId = costumerId;
        CostumerName = costumerName;
        Footage = footage;
        Value = value;
        City = city;
        State = state;
        Freight = null;
        FreightId = null;
    }

    public async void UpdateBudget(int costumerId, string costumerName, double footage, double value, string city, string state, int? freightId)
    {
        CostumerId = costumerId;
        CostumerName = costumerName;
        Footage = footage;
        Value = value;
        City = city;
        State = state;
        FreightId = freightId;
    }

    public void SetFreight(Freight freight)
    {
        Freight = freight;
        FreightId = freight?.Id;
    }

    public override string ToString()
{
    return $"CustomerId: {CostumerId}, " +
           $"CustomerName: {CostumerName}, " +
           $"Footage: {Footage}, " +
           $"Value: {Value}, " +
           $"City: {City}, " +
           $"State: {State}, " +
           $"FreightId: {FreightId}, " +
           $"Freight: {Freight}";
}
}
