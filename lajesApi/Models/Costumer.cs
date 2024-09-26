public class Costumer {
    public int Id { get; init; }
    public string Name { get; private set; }
    public bool PJ { get; private set; }
    public string CnpjCpf { get; private set; }
    public string Cep { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Address { get; private set; }
    public int AddressNumber { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }

    public Costumer(){}

    public Costumer(string name, bool pj, string cnpjCpf, string cep, string city, string state, string address, int addressNumber, string email, string phoneNumber)
        {
            Name = name;
            PJ = pj;
            CnpjCpf = cnpjCpf;
            Cep = cep;
            City = city;
            State = state;
            Address = address;
            AddressNumber = addressNumber;
            Email = email;
            PhoneNumber = phoneNumber;
        }

    public void Updatecostumer(string name, bool pj, string cnpjCpf, string cep, string city, string state, string address, int addressNumber, string email, string phoneNumber) {
        Name = name;
            PJ = pj;
            CnpjCpf = cnpjCpf;
            Cep = cep;
            City = city;
            State = state;
            Address = address;
            AddressNumber = addressNumber;
            Email = email;
            PhoneNumber = phoneNumber;
    }
}