using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces.Repositories;
using Domain;

namespace DAL.App.Interfaces.Repositories
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        /// <summary>
        /// Check for entity existance by PK value
        /// </summary>
        /// <param name="id">Answer PK value</param>
        /// <returns></returns>
        bool Exists(int id);
    }
}
