using System;
using System.Collections.Generic;
using System.Text;
using DAL.App.Interfaces.Helpers;
using DAL.Interfaces;
using DAL.Interfaces.Repositories;

namespace DAL.App.EF.Helpers
{
    public class EFRepositoryProvider : IRepositoryProvider
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly Dictionary<Type, object> _repositoryCache = new Dictionary<Type, object>();

        public EFRepositoryProvider(IDataContext dataContext, IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _applicationDbContext = dataContext as ApplicationDbContext;
            if (_applicationDbContext == null)
            {
                throw new NullReferenceException("No EF dbcontext found in UOW");
            }
        }

        public IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public TRepository GetCustomRepository<TRepository>() where TRepository : class
        {
            throw new NotImplementedException();
        }
    }
}
