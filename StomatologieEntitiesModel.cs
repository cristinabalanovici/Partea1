using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace StomatologieModel
{
    public partial class StomatologieEntitiesModel : DbContext
    {
        public StomatologieEntitiesModel()
            : base("name=StomatologieEntitiesModel")
        {
        }

        public virtual DbSet<Concediu> Concedius { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Pontaj> Pontajs { get; set; }
        public virtual DbSet<Titlu> Titlus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pontaj>()
                .Property(e => e.OraStart)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Pontaj>()
                .Property(e => e.OraFinal)
                .HasPrecision(4, 2);
        }
    }
}
