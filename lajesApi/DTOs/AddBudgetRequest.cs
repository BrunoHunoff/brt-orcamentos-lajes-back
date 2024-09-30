public record AddBudgetRequest(
    int costumerId,
    string costumerName,
    double footage, 
    double value, 
    string city, 
    string state
);