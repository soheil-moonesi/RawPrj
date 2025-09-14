using Microsoft.EntityFrameworkCore;
//dotnet ef migrations add Init
//dotnet ef database update
//dotnet ef migrations remove

//dotnet ef database drop

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
        //SeedDatabase();
    }

    //why we must use model builder :
    //The primary job of the ModelBuilder is to let you configure your model using a fluent API. This is more powerful than using data annotations (attributes on your classes) because it:
    //Keons Configuration Separate: Your entity classes (Product) remain clean POCOs (Plain Old CLR Objects) without being cluttered with database-specific attributes.
    //Is More Expressive: You can configure complex relationships, inheritance strategies (TPH, TPT, TPC), composite keys, and seed data in a very readable way.
    //Provides Full Control: You can have conditional logic in OnModelCreating to configure your model differently based on environment, user, or other factors.

    //How to Spot a Callback:
    // You override a virtual or abstract method from a base class.
    // You implement an interface method required by a framework.
    // You pass a method (often a lambda) as a parameter to a framework method (e.g., .ForEach(...)).
    // The control flow is inverted: The framework controls when and if your code runs. This is often called the Hollywood Principle: "Don't call us, we'll call you."
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(p => p.ProductIdentifier); // Defines the Primary Key
            entity.Property(p => p.ProductName).IsRequired().HasMaxLength(200);

            entity.HasOne(p => p.ProductManufacture) // A Product has one Manufacture...
            .WithMany(m => m.ManufactureProducts) // ...and a Manufacture has many Products
            .HasForeignKey(p => p.ManufactureTraceId); // ...using this property as the Foreign Key
        }
        );

        modelBuilder.Entity<Manufacture>(entity =>
        {
            entity.ToTable("Manufactures");
            entity.HasKey(m => m.ManufacturerIdentifier);
        });


        modelBuilder.Entity<Product>().HasData(new Product
        {
            ProductIdentifier = 1,
            ProductName = "SLS",
            ManufactureTraceId = 1
        });


        modelBuilder.Entity<Manufacture>().HasData(new Manufacture
        {
            ManufacturerIdentifier = 1,
            ManufactureName = "Benz",
            ManufactureCountry = "Germany"

        });

    }





    //Why This Approach is Problematic
    // Violates Single Responsibility Principle: The DbContext's job is
    // to manage database connections and track changes, not to handle data seeding.
    // Unexpected Side Effects: The constructor runs every time a new DbContext instance is created.
    // This could happen multiple times during a request or in different parts of your application.
    // Inefficient: You're attempting to add seed data every single time the context is instantiated, which is not what you want.
    // Breaks Patterns: If you're using Repository/Unit of Work patterns, you're abstracting away the DbContext.
    //  Putting seeding logic here undermines that abstraction.

    // public void SeedDatabase()
    // {
    //     Products.Add(new Product
    //     {
    //         Id = 1,
    //         Name = "TestProduct1"
    //     });

    // }

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



// The "When": Model Building Phase
// This method is called once per application instance (usually per DbContext type) when the model is being built and cached. 
//This happens the first time a DbContext of this type is used.
// It is not called on every database query or every time you create a new DbContext instance.
// This makes it extremely efficient for one-time setup tasks like configuration and seeding.
// Analogy: Building a House
// DbContext is the construction site.
// ModelBuilder is the foreman who shows up at the very beginning of the project with the blueprints.
// OnModelCreating is your chance to give instructions to the foreman on how to read and modify those blueprints before any foundation is poured.
// You (ApplicationDbContext) are the architect.
// The foreman (ModelBuilder) doesn't exist until the construction company (EF Core) starts the project, and he only asks for your instructions once, at the very start.


// • Add-Migration: Adds a new migration
// • Drop-Database: Drops the database
// • Get-DbContext: Lists and gets info about DbContext types
// • Script-Migrations: Creates SQL scripts for migration
// • Update-Database: Updates the database to the latest migration