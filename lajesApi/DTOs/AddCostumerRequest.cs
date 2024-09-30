public record AddCostumerRequest(
    string Name, 
    bool Pj, 
    string CnpjCpf, 
    string Cep, 
    string City,
    string State, 
    string Address, 
    int AddressNumber, 
    string Email, 
    string PhoneNumber
);