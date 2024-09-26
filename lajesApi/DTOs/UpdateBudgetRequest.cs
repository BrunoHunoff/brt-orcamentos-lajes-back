public record UpdateBudgetRequest(
    int costumerId,
    string costumerName,
    double footage, 
    double value, 
    string city, 
    string state,
    Freight freight,
    List<BudgetSlab> budgetSlabs
);