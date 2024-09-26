public record AddBudgetSlabRequest(
    int slabId, 
    int budgetId, 
    int slabsNumber,
    double length, 
    double width
);