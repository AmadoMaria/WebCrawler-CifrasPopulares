﻿// <auto-generated />
using System;
using CifrasPopulares.CifrasContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CifrasPopulares.Migrations
{
    [DbContext(typeof(CifrasDbContext))]
    [Migration("20200918133135_PrimeiroDatabase")]
    partial class PrimeiroDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CifrasPopulares.Models.Artista", b =>
                {
                    b.Property<int>("ArtistaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(767)");

                    b.HasKey("ArtistaID");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("Artistas");
                });

            modelBuilder.Entity("CifrasPopulares.Models.Musica", b =>
                {
                    b.Property<int>("MusicaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ArtistaID")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.HasKey("MusicaID");

                    b.HasIndex("ArtistaID");

                    b.ToTable("Musicas");
                });

            modelBuilder.Entity("CifrasPopulares.Models.Ranking", b =>
                {
                    b.Property<int>("RankingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("data")
                        .HasColumnType("datetime");

                    b.HasKey("RankingID");

                    b.ToTable("Rankings");
                });

            modelBuilder.Entity("CifrasPopulares.Models.RankingMusica", b =>
                {
                    b.Property<int>("RankingID")
                        .HasColumnType("int");

                    b.Property<int>("MusicaID")
                        .HasColumnType("int");

                    b.Property<int>("PosicaoMusica")
                        .HasColumnType("int");

                    b.HasKey("RankingID", "MusicaID");

                    b.HasIndex("MusicaID");

                    b.ToTable("RankingMusicas");
                });

            modelBuilder.Entity("CifrasPopulares.Models.Musica", b =>
                {
                    b.HasOne("CifrasPopulares.Models.Artista", "Artista")
                        .WithMany("Musicas")
                        .HasForeignKey("ArtistaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CifrasPopulares.Models.RankingMusica", b =>
                {
                    b.HasOne("CifrasPopulares.Models.Musica", null)
                        .WithMany("RankingMusicas")
                        .HasForeignKey("MusicaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CifrasPopulares.Models.Ranking", null)
                        .WithMany("RankingMusicas")
                        .HasForeignKey("RankingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
