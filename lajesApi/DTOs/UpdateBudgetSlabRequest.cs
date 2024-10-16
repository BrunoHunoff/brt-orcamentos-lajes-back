public record UpdateBudgetSlabeRequest(
    int Id,
    int SlabId, 
    int SlabsNumber,
    double Overload,
    double Length, 
    double Width
);