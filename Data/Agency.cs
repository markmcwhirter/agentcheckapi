namespace AgentCheckApi.Data; 
public class Agency
{
    public Int64 Id { get; set; }
    public string Name { get; set; }
    public bool TestAgency { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }

    public Agency(Int64 Id_, string Name_, bool TestAgency_, DateTime DateCreated_, DateTime DateUpdated_)
    {
        this.Id = Id_;
        this.Name = Name_;
        this.TestAgency = TestAgency_;
        this.DateCreated = DateCreated_;
        this.DateUpdated = DateUpdated_;
    }
}