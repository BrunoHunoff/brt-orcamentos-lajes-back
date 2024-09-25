public class BudgetSummary {
    public int Id { get; init; }
    public int BudgetId { get; private set; }
    public double Contribution { get; private set; }
    public double Administration { get; private set; }
    public double Taxes { get; private set;}
    public double Extra { get; private set;} 
    public double FreightWeight { get; private set; }

    public BudgetSummary(int budgetId, double contribution, double administration, double taxes, double extra, double freightWeight) {
        BudgetId = budgetId;
        Contribution = contribution;
        Administration = administration;
        Taxes = taxes;
        Extra = extra;
        FreightWeight = freightWeight;
    }
}