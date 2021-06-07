using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RstateAPI.Entities;
using RstateAPI.Entities.CityData;
using RstateAPI.Entities.ContactDetails;
using RstateAPI.Entities.PropertyConfiguration;

namespace RstateAPI.Data {
  public class StoreContext : IdentityDbContext {
    public StoreContext (DbContextOptions<StoreContext> options) : base (options) { }
    public DbSet<SaveModel> PostPropertyModel { get; set; }
    public DbSet<BasicDetails> basicDetails { get; set; }
    public DbSet<Locations> Locations { get; set; }
    public DbSet<Images1> images { get; set; }
    public DbSet<City> city { get; set; }
    public DbSet<Sector> sector { get; set; }
    public DbSet<Pocket> Pockets { get; set; }
    public DbSet<AddressLocality> AddressLocality { get; set; }
    public DbSet<PropertyConfig> PropertyConfig { get; set; }
    public DbSet<AdminAccessProp> adminconfigProp { get; set; }
    public DbSet<UserContact> userContact { get; set; }
    public DbSet<ApplicationUser> appuser { get; set; }
    public DbSet<SearchAddresData> addressSearchData { get; set; }
    protected override void OnModelCreating (ModelBuilder builder) {
      base.OnModelCreating (builder);
    }
  }

}