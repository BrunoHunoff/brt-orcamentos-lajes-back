public record AddBudgetRequest(
    int costumerId,
    string costumerName,
    double footage,
    double value,
    string city,
    string state,
    int? freightId,
    double administration,
    double profit,
    double taxes,
    double extra,
    List<AddBudgetSlabRequest> slabs
);
