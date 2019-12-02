using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LearnDI.Connection
{
    public class DiDbContextFactory : IDesignTimeDbContextFactory<DiDbContext>
    {
        public DiDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DiDbContext>();
            string connectionString = "Data Source=CPP00134171D\\ANHVT22;Initial Catalog=DiDB;Integrated Security=True";

            builder.UseSqlServer(connectionString);
            return new DiDbContext(builder.Options);
        }
    }
}
