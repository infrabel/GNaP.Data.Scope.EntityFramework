namespace GNaP.Data.Scope.EntityFramework.Demo.DatabaseContext
{
    using System.Data.Entity;
    using System.Reflection;
    using DomainModel;

    public class UserManagementDbContext : DbContext
    {
        // Map our 'User' model by convention
        public DbSet<User> Users { get; set; }

        public UserManagementDbContext()
            : base(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\ef.mdf;Integrated Security=True")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Overrides for the convention-based mappings.
            // We're assuming that all our fluent mappings are declared in this assembly.
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(UserManagementDbContext)));
        }
    }
}
