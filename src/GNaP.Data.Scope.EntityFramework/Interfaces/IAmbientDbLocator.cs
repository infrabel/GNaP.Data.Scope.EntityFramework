/*
 * Copyright (C) 2014 Mehdi El Gueddari
 * http://mehdi.me
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 */

namespace GNaP.Data.Scope.EntityFramework.Interfaces
{
    using System.Data.Entity;

    /// <summary>
    /// Convenience methods to retrieve ambient DbContext instances.
    /// </summary>
    public interface IAmbientDbLocator
    {
        /// <summary>
        /// If called within the scope of an EntityFrameworkScope, gets or creates
        /// the ambient DbContext instance for the provided DbContext type.
        ///
        /// Otherwise returns null.
        /// </summary>
        TDbContext Get<TDbContext>() where TDbContext : DbContext;
    }
}
