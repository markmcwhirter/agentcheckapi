namespace AgentCheckApi.Data; 
public class Producer
{
    public Int64 Id { get; set; }
    public Int64 AgencyId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string Prefix { get; set; }
    public string Suffix { get; set; }
    public string npm { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }

    public Producer(Int64 Id_, Int64 AgencyId_, string LastName_, string FirstName_, string MiddleName_, string Prefix_, string Suffix_, string npm_, DateTime DateCreated_, DateTime DateUpdated_)
    {
        this.Id = Id_;
        this.AgencyId = AgencyId_;
        this.LastName = LastName_;
        this.FirstName = FirstName_;
        this.MiddleName = MiddleName_;
        this.Prefix = Prefix_;
        this.Suffix = Suffix_;
        this.npm = npm_;
        this.DateCreated = DateCreated_;
        this.DateUpdated = DateUpdated_;
    }
}