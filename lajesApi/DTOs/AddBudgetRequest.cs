public record AddBudgetRequest(
    int costumerId,
    string costumerName,
    double footage,
    double totalWeight,
    double cost,
    double sellPrice,
    string city,
    string state,
    double freightWeight,
    string freightType,
    double? freightPrice,
    double administration,
    double profit,
    double taxes,
    double extra,
    List<AddBudgetSlabRequest> slabs
);
