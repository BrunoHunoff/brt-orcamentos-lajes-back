public record AddBudgetSlabRequest(
    int slabId, 
    int budgetId, 
    int slabsNumber,
    double overload,
    double length, 
    double width
);