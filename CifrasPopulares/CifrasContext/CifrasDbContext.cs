using CifrasPopulares.Models;
using Microsoft.EntityFrameworkCore;


namespace CifrasPopulares.CifrasContext
{
    class CifrasDbContext : DbContext
    {
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Ranking> Rankings { get; set; }
        public DbSet<RankingMusica> RankingMusicas { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=cifrasDb;user=root;password=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Artista>();
            modelBuilder.Entity<Ranking>();

            modelBuilder.Entity<Musica>()
                .HasOne<Artista>(c => c.Artista)
                .WithMany(f => f.Musicas)
                .HasForeignKey(c => c.ArtistaID);

            modelBuilder.Entity<RankingMusica>()
                .HasOne<Musica>(ec => ec.Musica)
                .WithMany(e => e.RankingMusicas)
                .HasForeignKey(ec => ec.MusicaID);

            modelBuilder.Entity<RankingMusica>()
                .HasOne<Ranking>(ec => ec.Ranking)
                .WithMany(c => c.RankingMusicas)
                .HasForeignKey(ec => ec.RankingID);

        }
    }
}