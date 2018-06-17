using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces.Repositories;
using Domain;

namespace DAL.App.Interfaces.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        /// <summary>
        /// Check for entity existance by PK value
        /// </summary>
        /// <param name="id">Question PK value</param>
        /// <returns></returns>
        bool Exists(int id);
    }
}
