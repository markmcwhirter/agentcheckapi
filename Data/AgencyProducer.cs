namespace AgentCheckApi.Data; 
public class AgencyProducer
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public int ProducerId { get; set; }
    public DateTime DateCreated { get; set; }

    public AgencyProducer(int Id_, int AgencyId_, int ProducerId_, DateTime DateCreated_)
    {
        this.Id = Id_;
        this.AgencyId = AgencyId_;
        this.ProducerId = ProducerId_;
        this.DateCreated = DateCreated_;
    }
}