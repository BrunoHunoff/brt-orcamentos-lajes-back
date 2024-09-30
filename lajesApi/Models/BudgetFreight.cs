public class BudgetFreight{
    public int Id { get; init; }
    public int BudgetId { get; private set; }
    public int FreightId { get; private set; }

    public BudgetFreight(int budgetid, int freightid) {
        BudgetId = budgetid;
        FreightId = freightid;
    }
}