using Microsoft.EntityFrameworkCore;

public class ApplicationDBContext:DbContext
{

     public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
    {

    }
}