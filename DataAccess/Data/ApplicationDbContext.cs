using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductAttributes> Attributes { get; set; }
        public DbSet<ProductImage> productImages { get; set; }
        public DbSet<IPAddress> ipAddresses { get; set; }
        public DbSet<StoreImage> storeImages { get; set; }
        public DbSet<TagType> tagTypes { get; set; }
        public DbSet<Tag> tags { get; set; }
        public DbSet<StoreTags> storeTags { get; set; }
        public DbSet<CountDetails> countDetails { get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Store>().Property(i => i.ClickCount).HasDefaultValue(0);
        //    builder.Entity<Category>().Property(i => i.ClickCount).HasDefaultValue(0);
        //    builder.Entity<Product>().Property(i => i.ClickCount).HasDefaultValue(0);

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Database=Sukhdari; Trusted_Connection=True");
        //=> optionsBuilder.UseSqlServer("Server=66.165.248.146\\MSSQLSERVER2016;Database=SukhdariDB;User Id=Sukhdari;Password=SukhProj123@; TrustServerCertificate=True");
        //=> optionsBuilder.UseSqlServer("Server=66.165.248.146\\MSSQLSERVER2016;Database=AzamClothMarketDB;User Id=AzamClothMarket;Password=LocalMarketOnline123@;TrustServerCertificate=True");
    }

}
