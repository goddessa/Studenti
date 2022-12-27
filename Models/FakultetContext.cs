

using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class FakultetContext : DbContext
    {
        public DbSet<Student> Studenti { get; set; } = null!;
        public DbSet<Predmet> Predmeti { get; set; } = null!;
        public DbSet<IspitniRok> Rokovi { get; set; } = null!;
        public DbSet<Spoj> StudentiPredmeti { get; set; } = null!;

        public FakultetContext(DbContextOptions options) : base(options)
        {
        }
     
    }
} 

