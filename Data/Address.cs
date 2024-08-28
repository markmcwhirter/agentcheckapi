namespace AgentCheckApi.Data;
public class Address
{
    public Int64 Id { get; set; }
    public Int64 AddressTypeId { get; set; }
    public Int64 AgencyId { get; set; }
    public Int64 ProducerId { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string LastUpdateUser { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }

    public Address(Int64 Id_, Int64 AddressTypeId_, Int64 AgencyId_, Int64 ProducerId_, string AddressLine1_, string AddressLine2_, string City_, string Region_, string PostalCode_, string Country_, string LastUpdateUser_, DateTime DateCreated_, DateTime DateUpdated_)
    {
        this.Id = Id_;
        this.AddressTypeId = AddressTypeId_;
        this.AgencyId = AgencyId_;
        this.ProducerId = ProducerId_;
        this.AddressLine1 = AddressLine1_;
        this.AddressLine2 = AddressLine2_;
        this.City = City_;
        this.Region = Region_;
        this.PostalCode = PostalCode_;
        this.Country = Country_;
        this.LastUpdateUser = LastUpdateUser_;
        this.DateCreated = DateCreated_;
        this.DateUpdated = DateUpdated_;
    }
}