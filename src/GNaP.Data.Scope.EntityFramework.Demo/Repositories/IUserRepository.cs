﻿namespace GNaP.Data.Scope.EntityFramework.Demo.Repositories
{
    using System;
    using System.Threading.Tasks;
    using DomainModel;

    public interface IUserRepository
    {
        User Get(Guid userId);
        Task<User> GetAsync(Guid userId);
        void Add(User user);
    }
}