namespace F1GuessAPI.Entites
{
    public class F1GuessContext : DbContext
    {
        public F1GuessContext(DbContextOptions<F1GuessContext> options) : base(options)
        {

        }
    }
}
