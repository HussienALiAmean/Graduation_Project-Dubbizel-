using Dubbizle.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dubbizle.Data
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context(DbContextOptions options) : base(options)
        { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-LKBV544\SQL19;Initial Catalog=DubbizleDB;Integrated Security=True;Encrypt=False");

          //  optionsBuilder.UseSqlServer(@"Data Source=.\SQL19;Initial Catalog=DubbizleDB;Integrated Security=True;Encrypt=False");

        //    //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-SH1SPK1\SQL2019;Initial Catalog=DubbizleDB;Integrated Security=True;Encrypt=False");

        //    base.OnConfiguring(optionsBuilder);


        //    //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-MFGHPAO\SQL19;Initial Catalog=DubbizleDB;Integrated Security=True;Encrypt=False");

        //    //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-SH1SPK1\SQL2019;Initial Catalog=DubbizleDB;Integrated Security=True;Encrypt=False");

        //    base.OnConfiguring(optionsBuilder);

        //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-LKBV544\SQL19;Initial Catalog=DubbizleDB;Integrated Security=True;Encrypt=False");


        //    //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-SH1SPK1\SQL2019;Initial Catalog=DubbizleDB;Integrated Security=True;Encrypt=False");

        //  //  base.OnConfiguring(optionsBuilder);

        //}

        public DbSet<Advertisment> Advertisments { get; set; }
        public DbSet<Advertisment_FiltrationValue> Advertisment_FiltrationValues { get; set; }
        public DbSet<Advertisment_RentOption> Advertisment_RentOptions { get; set; }
        public DbSet<AdvertismentImage> AdvertismentImages { get; set; }
        public DbSet<ApplicationUser_Package> ApplicationUser_Packages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<FiltrationValue> FiltrationValues { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<RentOption> RentOptions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SubCategory_Filter> SubCategory_Filters { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

    }
}