using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ClinicaVeterinariaApp.Models
{
    public partial class ModelDBContext : DbContext
    {
        public ModelDBContext()
            : base("name=ModelDBContext")
        {
        }

        public virtual DbSet<Animals> Animals { get; set; }
        public virtual DbSet<Exams> Exams { get; set; }
        public virtual DbSet<Species> Species { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animals>()
                .HasMany(e => e.Exams)
                .WithRequired(e => e.Animals)
                .WillCascadeOnDelete(false);

           

            modelBuilder.Entity<Species>()
                .HasMany(e => e.Animals)
                .WithRequired(e => e.Species)
                .WillCascadeOnDelete(false);
        }
    }
}
