namespace F1GuessAPI.Entites
{
    public class F1GuessContext : DbContext
    {
        public F1GuessContext(DbContextOptions<F1GuessContext> options) : base(options)
        {

        }

        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;
        public virtual DbSet<TblGuess> TblGuess { get; set; } = null!;
        public virtual DbSet<TblUserScoreboard> TblUserScoreboard { get; set; } = null!;
    }
}
