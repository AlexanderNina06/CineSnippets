using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options) 
        {

        }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Productora> Productoras { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<SerieGenero> SeriesGeneros { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region tables
            modelBuilder.Entity<Serie>().ToTable("Series");
            modelBuilder.Entity<Productora>().ToTable("Productoras");
            modelBuilder.Entity<Genero>().ToTable("Generos");
            modelBuilder.Entity<SerieGenero>().ToTable("SeriesGeneros");
            #endregion

            #region "primary keys"
            modelBuilder.Entity<Serie>().HasKey(serie => serie.Id);
            modelBuilder.Entity<Productora>().HasKey(p => p.Id);
            modelBuilder.Entity<Genero>().HasKey(g => g.Id);
            modelBuilder.Entity<SerieGenero>().HasKey(sg => new {sg.SerieId, sg.GeneroId});
            #endregion#

            //Relacion de Productora con Serie, uno a muchos.
            #region relationships
            modelBuilder.Entity<Productora>()
                .HasMany<Serie>(p => p.Series)
                .WithOne(s => s.Productora)
                .HasForeignKey(s=>s.ProductoraId)
                .OnDelete(DeleteBehavior.Cascade);

            //Relacion de serie con genero, Muchos a Muchos.
            modelBuilder.Entity<SerieGenero>()
                .HasOne(sg => sg.Serie)
                .WithMany(s => s.SerieGeneros)
                .HasForeignKey(sg => sg.SerieId);

  
            modelBuilder.Entity<SerieGenero>()
                .HasOne(sg => sg.Genero)
                .WithMany(g => g.SerieGeneros)
                .HasForeignKey(sg => sg.GeneroId);

            //Relacion de genero con SerieGenero, uno a muchos.
            modelBuilder.Entity<Genero>()
                .HasMany(g => g.SerieGeneros)
                .WithOne(sg => sg.Genero)
                .HasForeignKey(sg => sg.GeneroId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "property configuration"

            #region Serie

            modelBuilder.Entity<Serie>().Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Serie>()
                .Property(s => s.ImagenPortada)
                .IsRequired();
            modelBuilder.Entity<Serie>()
             .Property(s => s.EnlaceVideoYouTube)
             .IsRequired();

            #endregion

            #region Productora
            modelBuilder.Entity<Productora>()
                .Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            #endregion

            #endregion
        }
    }
}
