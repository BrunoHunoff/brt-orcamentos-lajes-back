public record UpdateBudgetRequest(
    int costumerId,
    string costumerName,
    double footage, 
    double value, 
    string city, 
    string state,
    int freightId,
    List<BudgetSlab> budgetSlabs
);