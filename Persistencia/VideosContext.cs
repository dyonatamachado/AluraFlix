using AluraFlix.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AluraFlix.Persistencia
{
    public class VideosContext : DbContext
    {
        public VideosContext(DbContextOptions<VideosContext> options) : base(options)
        {

        }
        public DbSet<Video> Videos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>(e => 
            {
                e.HasKey(v => v.Id);
            });
        }

    }
}