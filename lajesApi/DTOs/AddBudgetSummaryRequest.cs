public record AddBudgetSummaryRequest(
    int budgetId, 
    double contribution, 
    double administration, 
    double taxes, 
    double extra, 
    double freightWeight
);