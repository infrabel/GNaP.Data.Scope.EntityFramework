/*
 * Copyright (C) 2014 Mehdi El Gueddari
 * http://mehdi.me
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 */

namespace GNaP.Data.Scope.EntityFramework.Implementation
{
    using System;
    using System.Data;
    using Interfaces;

    public class EntityFrameworkScopeFactory : IDbScopeFactory
    {
        private readonly IDbFactory _dbFactory;

        public EntityFrameworkScopeFactory()
            : this(dbFactory: null)
        { }

        public EntityFrameworkScopeFactory(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public IDbScope Create(DbScopeOption joiningOption = DbScopeOption.JoinExisting)
        {
            return new EntityFrameworkScope(
                joiningOption: joiningOption,
                readOnly: false,
                isolationLevel: null,
                dbFactory: _dbFactory);
        }

        public IDbReadOnlyScope CreateReadOnly(DbScopeOption joiningOption = DbScopeOption.JoinExisting)
        {
            return new EntityFrameworkReadOnlyScope(
                joiningOption: joiningOption,
                isolationLevel: null,
                dbFactory: _dbFactory);
        }

        public IDbScope CreateWithTransaction(IsolationLevel isolationLevel)
        {
            return new EntityFrameworkScope(
                joiningOption: DbScopeOption.ForceCreateNew,
                readOnly: false,
                isolationLevel: isolationLevel,
                dbFactory: _dbFactory);
        }

        public IDbReadOnlyScope CreateReadOnlyWithTransaction(IsolationLevel isolationLevel)
        {
            return new EntityFrameworkReadOnlyScope(
                joiningOption: DbScopeOption.ForceCreateNew,
                isolationLevel: isolationLevel,
                dbFactory: _dbFactory);
        }

        public IDisposable SuppressAmbientScope()
        {
            return new AmbientContextSuppressor();
        }
    }
}
