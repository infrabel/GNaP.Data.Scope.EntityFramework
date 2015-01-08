namespace GNaP.Data.Scope.EntityFramework.Demo.BusinessLogicServices
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DatabaseContext;
    using Interfaces;

    public class UserCreditScoreService
    {
        private readonly IDbScopeFactory _dbScopeFactory;

        public UserCreditScoreService(IDbScopeFactory dbScopeFactory)
        {
            if (dbScopeFactory == null)
                throw new ArgumentNullException("dbScopeFactory");

            _dbScopeFactory = dbScopeFactory;
        }

        public void UpdateCreditScoreForAllUsers()
        {
            /*
             * Demo of DbScope + parallel programming.
             */

            using (var dbScope = _dbScopeFactory.Create())
            {
                //-- Get all users
                var dbContext = dbScope.Get<UserManagementDbContext>();
                var userIds = dbContext.Users.Select(u => u.Id).ToList();

                Console.WriteLine("Found {0} users in the database. Will calculate and store their credit scores in parallel.", userIds.Count);

                //-- Calculate and store the credit score of each user
                // We're going to imagine that calculating a credit score of a user takes some time.
                // So we'll do it in parallel.

                // You MUST call SuppressAmbientScope() when kicking off a parallel execution flow
                // within a DbScope. Otherwise, this DbScope will remain the ambient scope
                // in the parallel flows of execution, potentially leading to multiple threads
                // accessing the same DbContext instance.
                using (_dbScopeFactory.SuppressAmbientScope())
                {
                    Parallel.ForEach(userIds, UpdateCreditScore);
                }

                // Note: SaveChanges() isn't going to do anything in this instance since all the changes
                // were actually made and saved in separate DbScopes created in separate threads.
                dbScope.SaveChanges();
            }
        }

        public void UpdateCreditScore(Guid userId)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var dbContext = dbScope.Get<UserManagementDbContext>();

                var user = dbContext.Users.Find(userId);
                if (user == null)
                    throw new ArgumentException(String.Format("Invalid userId provided: {0}. Couldn't find a User with this ID.", userId));

                // Simulate the calculation of a credit score taking some time
                var random = new Random(Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(random.Next(300, 1000));

                user.CreditScore = random.Next(1, 100);
                dbScope.SaveChanges();
            }
        }
    }
}
