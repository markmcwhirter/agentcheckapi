namespace AgentCheckApi.Data; 
public class AddressType
{
    public Int64 Id { get; set; }
    public string AddressTypeName { get; set; }
    public string AddressTypeDescription { get; set; }
    public bool AddressTypeActive { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }

    public AddressType(Int64 Id_, string AddressTypeName_, string AddressTypeDescription_, bool AddressTypeActive_, DateTime DateCreated_, DateTime DateUpdated_)
    {
        this.Id = Id_;
        this.AddressTypeName = AddressTypeName_;
        this.AddressTypeDescription = AddressTypeDescription_;
        this.AddressTypeActive = AddressTypeActive_;
        this.DateCreated = DateCreated_;
        this.DateUpdated = DateUpdated_;
    }
}