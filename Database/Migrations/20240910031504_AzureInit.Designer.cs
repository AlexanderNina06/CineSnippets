﻿// <auto-generated />
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240910031504_AzureInit")]
    partial class AzureInit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Database.Models.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Generos", (string)null);
                });

            modelBuilder.Entity("Database.Models.Productora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Productoras", (string)null);
                });

            modelBuilder.Entity("Database.Models.Serie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EnlaceVideoYouTube")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagenPortada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ProductoraId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductoraId");

                    b.ToTable("Series", (string)null);
                });

            modelBuilder.Entity("Database.Models.SerieGenero", b =>
                {
                    b.Property<int>("SerieId")
                        .HasColumnType("int");

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.HasKey("SerieId", "GeneroId");

                    b.HasIndex("GeneroId");

                    b.ToTable("SeriesGeneros", (string)null);
                });

            modelBuilder.Entity("Database.Models.Serie", b =>
                {
                    b.HasOne("Database.Models.Productora", "Productora")
                        .WithMany("Series")
                        .HasForeignKey("ProductoraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Productora");
                });

            modelBuilder.Entity("Database.Models.SerieGenero", b =>
                {
                    b.HasOne("Database.Models.Genero", "Genero")
                        .WithMany("SerieGeneros")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.Serie", "Serie")
                        .WithMany("SerieGeneros")
                        .HasForeignKey("SerieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genero");

                    b.Navigation("Serie");
                });

            modelBuilder.Entity("Database.Models.Genero", b =>
                {
                    b.Navigation("SerieGeneros");
                });

            modelBuilder.Entity("Database.Models.Productora", b =>
                {
                    b.Navigation("Series");
                });

            modelBuilder.Entity("Database.Models.Serie", b =>
                {
                    b.Navigation("SerieGeneros");
                });
#pragma warning restore 612, 618
        }
    }
}
