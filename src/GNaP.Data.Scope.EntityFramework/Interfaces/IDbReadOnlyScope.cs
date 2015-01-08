/*
 * Copyright (C) 2014 Mehdi El Gueddari
 * http://mehdi.me
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 */

namespace GNaP.Data.Scope.EntityFramework.Interfaces
{
    using System;
    using System.Data.Entity;

    /// <summary>
    /// A read-only EntityFrameworkScope. Refer to the comments for IDbScope
    /// for more details.
    /// </summary>
    public interface IDbReadOnlyScope : IDisposable
    {
        /// <summary>
        /// Get a DbContext instance managed by this EntityFrameworkScope. Don't call SaveChanges() on the DbContext itself!
        /// Save the scope instead.
        /// </summary>
        TDbContext Get<TDbContext>() where TDbContext : DbContext;
    }
}
