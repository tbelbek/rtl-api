#region usings

using System;
using System.Data.Entity;
using RtlAPI.Data.Entity;

#endregion

namespace RtlAPI.Data
{
    public class CustomContext : DbContext
    {
        public CustomContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<TvShow> TvShow { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Network> Network { get; set; }
        public DbSet<Country2> Country2 { get; set; }
        public DbSet<WebChannel> WebChannel { get; set; }
        public DbSet<Externals> Externals { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Self> Self { get; set; }
        public DbSet<Previousepisode> Previousepisode { get; set; }
        public DbSet<Nextepisode> Nextepisode { get; set; }
        public DbSet<Links> Links { get; set; }
        public DbSet<CastPerson> CastPerson { get; set; }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default(int);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            modelBuilder.Entity<CastPerson>()
                .HasRequired(m => m.Person )
                .WithMany()
                .HasForeignKey(c => c.PersonId);

            base.OnModelCreating(modelBuilder);
        }
    }


}