public record AddCostumerRequest(
    string Name, 
    string Pj, 
    string CnpjCpf, 
    string Cep, 
    string City,
    string State, 
    string Address, 
    int AddressNumber, 
    string Email, 
    string PhoneNumber
);