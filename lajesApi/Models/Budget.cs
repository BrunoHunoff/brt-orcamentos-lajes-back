public class Budget {
    public int Id { get; init; }
    public int ClientId { get; init; }
    public double Footage { get; private set; }
    public double Value { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }

    public Budget(int clientId, double footage, double value, string city, string state) {
        ClientId = clientId;
        Footage = footage;
        Value = value;
        City = city;
        State = state;
    }
}