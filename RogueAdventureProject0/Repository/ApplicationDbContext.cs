using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace RevatureP0TimStDennis.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

    public ApplicationDbContext(){}

    public DbSet<RogueAccount> RogueAccount {get;set;}
    public DbSet<RogueCharacter> RogueCharacter {get;set;}
    public DbSet<RogueEquipment> RogueEquipment {get;set;}
    public DbSet<RogueInventory> RogueInventory {get;set;}
    public DbSet<RogueItems> rogueItems {get;set;}
    //public DbSet<RogueMonsters> RogueMonsters{get;set;}
    //public DbSet<RoguePerks> RoguePerks{get;set;}
    //public DbSet<RogueTechs> RogueTechs {get;set;}
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json")
                                            .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {    

    }
}