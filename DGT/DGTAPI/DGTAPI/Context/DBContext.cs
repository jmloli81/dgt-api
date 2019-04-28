using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DGTAPI.Models;

namespace DGTAPI.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<Conductor> Conductor { get; set; }
        public DbSet<Vehiculos> Vehiculo { get; set; }
        public DbSet<TInfracciones> Infracciones { get; set; }
        public DbSet<RInfracciones> VehiculoInfraccion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conductor>(
                b=> {
                    b.HasKey(e => e.DNI);
                    b.Property(e => e.Nombre);
                    b.Property(e => e.Apellidos);
                    b.Property(e => e.Puntos);
                });

            modelBuilder.Entity<Vehiculos>(
                b => {
                    b.HasKey(e => e.Matricula);
                    b.Property(e => e.Marca);
                    b.Property(e => e.Modelo);
                });

            modelBuilder.Entity<TInfracciones>(
                b => {
                    b.HasKey(e => e.Id);
                    b.Property(e => e.Descripcion);
                    b.Property(e => e.PuntosDesc);
                });

            modelBuilder.Entity<RInfracciones>(
               b =>
               {
                   b.HasKey(e => e.Id);
                   b.Property(e => e.FechaHora);
                   b.Property(e => e.TipoInfraccion);
               });
        }
    }
}
