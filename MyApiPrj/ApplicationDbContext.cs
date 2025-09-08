using Microsoft.EntityFrameworkCore;
//dotnet ef migrations add Init
//dotnet ef database update
public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    //It's a Container of Settings: The DbContextOptions object holds all
    //  the configuration you set up in  Program.cs - the connection string,
    //  the database provider (SQLite, SQL Server, etc.), logging settings, and more.
    public ApplicationDbContext(DbContextOptions
    //It's Strongly-Typed: Using DbContextOptions<ApplicationDbContext>
    // (instead of just DbContextOptions) ensures that the configuration
    // intended for your ApplicationDbContext doesn't get accidentally
    // passed to a different DbContext class if you had one. 
    //It provides type safety.
    <ApplicationDbContext>

    //This is about inheritance. Your ApplicationDbContext inherits from the base DbContext class (from the EF Core library).
    // The base DbContext class requires this configuration information to know how to connect to the database.
    // By writing : base(options), you are simply passing the configuration (the DbContextOptions object) up to the base class's constructor.
    // Analogy: Imagine you're giving a package to a courier service (DbContext).
    // You receive the package with instructions (DbContextOptions<ApplicationDbContext> options).
    // You don't open it or use it yourself. You just hand it over to the courier.
    // The courier (base class) knows how to read the instructions and deliver the package.

    options) : base(options)
    {

    }


}

//note : In older versions of EF, you would override the OnConfiguring method, which is now considered less flexible:
// NOT THE RECOMMENDED WAY ANYMORE
// public class ApplicationDbContext : DbContext
// {
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//          You have to hardcode the configuration here, which is bad!
//         optionsBuilder.UseSqlite("Data Source=mydatabase.db");
//     }
// }

//Why the constructor injection way is better:
// No Hardcoding: The connection string stays in appsettings.json.
// Dependency Injection: The context is easily injectable and testable.
// Flexibility: You can easily change the database provider (e.g., from SQLite to SQL Server) 
// just by changing the registration in Program.cs without touching the ApplicationDbContext class itself.
//  This is a key principle called Dependency Inversion.