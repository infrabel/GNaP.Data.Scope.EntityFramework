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
        public IDbScope Create(DbScopeOption joiningOption = DbScopeOption.JoinExisting)
        {
            return new EntityFrameworkScope(
                joiningOption: joiningOption,
                readOnly: false,
                isolationLevel: null);
        }

        public IDbReadOnlyScope CreateReadOnly(DbScopeOption joiningOption = DbScopeOption.JoinExisting)
        {
            return new EntityFrameworkReadOnlyScope(
                joiningOption: joiningOption,
                isolationLevel: null);
        }

        public IDbScope CreateWithTransaction(IsolationLevel isolationLevel)
        {
            return new EntityFrameworkScope(
                joiningOption: DbScopeOption.ForceCreateNew,
                readOnly: false,
                isolationLevel: isolationLevel);
        }

        public IDbReadOnlyScope CreateReadOnlyWithTransaction(IsolationLevel isolationLevel)
        {
            return new EntityFrameworkReadOnlyScope(
                joiningOption: DbScopeOption.ForceCreateNew,
                isolationLevel: isolationLevel);
        }

        public IDisposable SuppressAmbientScope()
        {
            return new AmbientContextSuppressor();
        }
    }
}
