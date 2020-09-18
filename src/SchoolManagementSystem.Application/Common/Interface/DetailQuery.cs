
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Common.Interface
{
    public interface IReadOnlyRepository<TEntity> : IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable, IReadOnlyBasicRepository<TEntity>, IRepository where TEntity : class, IEntity
    {
        IQueryable<TEntity> WithDetails();

        IQueryable<TEntity> WithDetails(params Expression<Func<TEntity,object>>[] propertySelectors);
    }

    public interface IReadOnlyBasicRepository<TEntity> : IRepository where TEntity : class, IEntity
    {
        long GetCount();
        Task<long> GetCountAsync(CancellationToken cancellationToken = default);
        List<TEntity> GetList(bool includeDetails = false);
        Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default);
    }
}
