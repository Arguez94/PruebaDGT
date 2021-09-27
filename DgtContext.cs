namespace PruebaInnovation
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DgtContext : DbContext
    {
        public DgtContext()
            : base("name=DgtContext")
        {
        }

        public virtual DbSet<Conductor> Conductor { get; set; }
        public virtual DbSet<Infraccion> Infraccion { get; set; }
        public virtual DbSet<TipoInfraccion> TipoInfraccion { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conductor>()
                .HasMany(e => e.Infraccion)
                .WithRequired(e => e.Conductor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Conductor>()
                .HasMany(e => e.Vehiculo)
                .WithMany(e => e.Conductor)
                .Map(m => m.ToTable("ConductorVehiculo"));

            modelBuilder.Entity<TipoInfraccion>()
                .HasMany(e => e.Infraccion)
                .WithRequired(e => e.TipoInfraccion)
                //.HasForeignKey(e => e.TipoInfraccion_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehiculo>()
                .HasMany(e => e.Infraccion)
                .WithRequired(e => e.Vehiculo)
                .WillCascadeOnDelete(false);
        }
    }
}
