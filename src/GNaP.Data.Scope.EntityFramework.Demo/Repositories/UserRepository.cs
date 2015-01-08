namespace GNaP.Data.Scope.EntityFramework.Demo.Repositories
{
    using System;
    using System.Threading.Tasks;
    using DatabaseContext;
    using DomainModel;
    using Interfaces;

    /*
     * An example "repository" relying on an ambient DbContext instance.
     *
     * Since we use EF to persist our data, the actual repository is of course the EF DbContext. This
     * class is called a "repository" for old time's sake but is merely just a collection
     * of pre-built Linq-to-Entities queries. This avoids having these queries copied and
     * pasted in every service method that need them and facilitates unit testing.
     *
     * Whether your application would benefit from using this additional layer or would
     * be better off if its service methods queried the DbContext directly or used some sort of query
     * object pattern is a design decision for you to make.
     *
     * DbContextScope is agnostic to this and will happily let you use any approach you
     * deem most suitable for your application.
     *
     */
    public class UserRepository : IUserRepository
    {
        private readonly IAmbientDbLocator _ambientDbLocator;

        private UserManagementDbContext DbContext
        {
            get
            {
                var dbContext = _ambientDbLocator.Get<UserManagementDbContext>();

                if (dbContext == null)
                    throw new InvalidOperationException("No ambient DbContext of type UserManagementDbContext found. This means that this repository method has been called outside of the scope of a DbScope. A repository must only be accessed within the scope of a DbScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbScopeFactory in your top-level business logic service method to create a DbScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbScope.cs file for more details.");

                return dbContext;
            }
        }

        public UserRepository(IAmbientDbLocator ambientDbLocator)
        {
            if (ambientDbLocator == null)
                throw new ArgumentNullException("ambientDbLocator");

            _ambientDbLocator = ambientDbLocator;
        }

        public User Get(Guid userId)
        {
            return DbContext.Users.Find(userId);
        }

        public Task<User> GetAsync(Guid userId)
        {
            return DbContext.Users.FindAsync(userId);
        }

        public void Add(User user)
        {
            DbContext.Users.Add(user);
        }
    }
}
