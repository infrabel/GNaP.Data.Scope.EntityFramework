﻿/*
 * Copyright (C) 2014 Mehdi El Gueddari
 * http://mehdi.me
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 */

namespace GNaP.Data.Scope.EntityFramework.Interfaces
{
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates and manages the DbContext instances used by this code block.
    ///
    /// You typically use an EntityFrameworkScope at the business logic service level. Each
    /// business transaction (i.e. each service method) that uses Entity Framework must
    /// be wrapped in an EntityFrameworkScope, ensuring that the same DbContext instances
    /// are used throughout the business transaction and are committed or rolled
    /// back atomically.
    ///
    /// Think of it as TransactionScope but for managing DbContext instances instead
    /// of database transactions. Just like a TransactionScope, an EntityFrameworkScope is
    /// ambient, can be nested and supports async execution flows.
    ///
    /// And just like TransactionScope, it does not support parallel execution flows.
    /// You therefore MUST suppress the ambient EntityFrameworkScope before kicking off parallel
    /// tasks or you will end up with multiple threads attempting to use the same DbContext
    /// instances (use IDbScopeFactory.SuppressAmbientScope() for this).
    ///
    /// You can access the DbContext instances that this scopes manages via either:
    /// - its Get() method, or
    /// - an IAmbientDbContextLocator
    ///
    /// (you would typically use the later in the repository / query layer to allow your repository
    /// or query classes to access the ambient DbContext instances without giving them access to the actual
    /// EntityFrameworkScope).
    /// </summary>
    public interface IDbScope : IDisposable
    {
        /// <summary>
        /// Saves the changes in all the DbContext instances that were created within this scope.
        /// This method can only be called once per scope.
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// Saves the changes in all the DbContext instances that were created within this scope.
        /// This method can only be called once per scope.
        /// </summary>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Saves the changes in all the DbContext instances that were created within this scope.
        /// This method can only be called once per scope.
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancelToken);

        /// <summary>
        /// Reloads the provided persistent entities from the data store
        /// in the DbContext instances managed by the parent scope.
        ///
        /// If there is no parent scope (i.e. if this EntityFrameworkScope
        /// if the top-level scope), does nothing.
        ///
        /// This is useful when you have forced the creation of a new
        /// EntityFrameworkScope and want to make sure that the parent scope
        /// (if any) is aware of the entities you've modified in the
        /// inner scope.
        ///
        /// (this is a pretty advanced feature that should be used
        /// with parsimony).
        /// </summary>
        void RefreshEntitiesInParentScope(IEnumerable entities);

        /// <summary>
        /// Reloads the provided persistent entities from the data store
        /// in the DbContext instances managed by the parent scope.
        ///
        /// If there is no parent scope (i.e. if this EntityFrameworkScope
        /// if the top-level scope), does nothing.
        ///
        /// This is useful when you have forced the creation of a new
        /// EntityFrameworkScope and want to make sure that the parent scope
        /// (if any) is aware of the entities you've modified in the
        /// inner scope.
        ///
        /// (this is a pretty advanced feature that should be used
        /// with parsimony).
        /// </summary>
        Task RefreshEntitiesInParentScopeAsync(IEnumerable entities);

        /// <summary>
        /// Get a DbContext instance managed by this EntityFrameworkScope. Don't call SaveChanges() on the DbContext itself!
        /// Save the scope instead.
        /// </summary>
        TDbContext Get<TDbContext>() where TDbContext : DbContext;
    }
}
