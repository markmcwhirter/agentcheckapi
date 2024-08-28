using Microsoft.EntityFrameworkCore;

namespace AgentCheckApi.Data;


public partial class AgentCheckContext : DbContext
{
    public AgentCheckContext(DbContextOptions<AgentCheckContext> options)
            : base(options)
    {
    }

    public DbSet<Agency> Agencies { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<Address> Addresses { get; set; }       
    public DbSet<AddressType> AddressTypes { get; set; }
    public DbSet<AgencyProducer> AgencyProducers { get; set; }
    public DbSet<Users> Userss { get; set; }




}

