namespace AgentCheckApi.Data; 
public class Users
{
    public int Id { get; set; }
    public string ProducerId { get; set; }
    public string Username { get; set; }
    public bool IsActive { get; set; }
    public string Password { get; set; }
    public TimeSpan DateCreated { get; set; }
    public TimeSpan DateUpdated { get; set; }
    public TimeSpan DateLastLogin { get; set; }
    public string Notes { get; set; }

    public Users(int Id_, string ProducerId_, string Username_, bool IsActive_, string Password_, TimeSpan DateCreated_, TimeSpan DateUpdated_, TimeSpan DateLastLogin_, string Notes_)
    {
        this.Id = Id_;
        this.ProducerId = ProducerId_;
        this.Username = Username_;
        this.IsActive = IsActive_;
        this.Password = Password_;
        this.DateCreated = DateCreated_;
        this.DateUpdated = DateUpdated_;
        this.DateLastLogin = DateLastLogin_;
        this.Notes = Notes_;
    }
}