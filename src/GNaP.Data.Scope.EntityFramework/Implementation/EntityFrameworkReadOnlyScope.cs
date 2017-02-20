/*
 * Copyright (C) 2014 Mehdi El Gueddari
 * http://mehdi.me
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 */

namespace GNaP.Data.Scope.EntityFramework.Implementation
{
    using System.Data;
    using System.Data.Entity;
    using Interfaces;

    internal class EntityFrameworkReadOnlyScope : IDbReadOnlyScope
    {
        private readonly EntityFrameworkScope _internalScope;

        public EntityFrameworkReadOnlyScope()
            : this(joiningOption: DbScopeOption.JoinExisting, isolationLevel: null, dbFactory: null)
        { }

        public EntityFrameworkReadOnlyScope(IDbFactory dbFactory)
            : this(joiningOption: DbScopeOption.JoinExisting, isolationLevel: null, dbFactory: dbFactory)
        { }

        public EntityFrameworkReadOnlyScope(IsolationLevel isolationLevel)
            : this(joiningOption: DbScopeOption.ForceCreateNew, isolationLevel: isolationLevel, dbFactory: null)
        { }

        public EntityFrameworkReadOnlyScope(IsolationLevel isolationLevel, IDbFactory dbFactory)
            : this(joiningOption: DbScopeOption.ForceCreateNew, isolationLevel: isolationLevel, dbFactory: dbFactory)
        { }

        public EntityFrameworkReadOnlyScope(DbScopeOption joiningOption, IsolationLevel? isolationLevel)
            : this(joiningOption: joiningOption, isolationLevel: isolationLevel, dbFactory: null)
        { }

        public EntityFrameworkReadOnlyScope(DbScopeOption joiningOption, IsolationLevel? isolationLevel, IDbFactory dbFactory)
        {
            _internalScope = new EntityFrameworkScope(
                joiningOption: joiningOption,
                readOnly: true,
                isolationLevel: isolationLevel,
                dbFactory: dbFactory);
        }

        public void Dispose()
        {
            _internalScope.Dispose();
        }

        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            return _internalScope.Get<TDbContext>();
        }
    }
}
