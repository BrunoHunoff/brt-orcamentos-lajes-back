public record UpdateBudgetSlabeRequest(
    int slabId, 
    int slabsNumber,
    double overload,
    double length, 
    double width
);