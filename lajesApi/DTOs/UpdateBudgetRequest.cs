public record UpdateBudgetRequest(
    int costumerId,
    string costumerName,
    double footage,
    double value,
    string city,
    string state,
    int? freightId,
    double freightPrice,
    double administration,
    double profit,
    double taxes,
    double extra,
    List<UpdateBudgetSlabeRequest> slabs
);
