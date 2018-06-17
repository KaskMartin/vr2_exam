using System;
using System.Collections.Generic;
using System.Text;
using DAL.EF;
using DAL.App.EF.Repositories;
using DAL.App.Interfaces.Helpers;
using DAL.App.Interfaces.Repositories;
using DAL.Interfaces;
using DAL.TaisKoht.EF.Repositories;

namespace DAL.App.EF.Helpers
{
    public class EFRepositoryFactory : IRepositoryFactory
    {
        private readonly Dictionary<Type, Func<IDataContext, object>> _customRepositoryFactories
            = GetCustomRepoFactories();

        private static Dictionary<Type, Func<IDataContext, object>> GetCustomRepoFactories()
        {
            return new Dictionary<Type, Func<IDataContext, object>>()
            {
                {typeof(IQuestionRepository), (dataContext) => new EFQuestionRepository(dataContext as ApplicationDbContext) },
                {typeof(IAnswerRepository), (dataContext) => new EFAnswerRepository(dataContext as ApplicationDbContext) },
                {typeof(IApplicationUserRepository), (dataContext) => new EFApplicationUserRepository(dataContext as ApplicationDbContext) }
            };
        }
        public Func<IDataContext, object> GetCustomRepositoryFactory<TRepoInterface>() where TRepoInterface : class
        {
            _customRepositoryFactories.TryGetValue(
                typeof(TRepoInterface),
                out Func<IDataContext, object> factory
            );
            return factory;
        }

        public Func<IDataContext, object> GetStandardRepositoryFactory<TEntity>() where TEntity : class
        {
            return (dataContext) => new EFRepository<TEntity>(dataContext as ApplicationDbContext);
        }
    }
}
