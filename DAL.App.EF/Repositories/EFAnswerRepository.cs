using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.App.Interfaces.Repositories;
using DAL.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    class EFAnswerRepository : EFRepository<Answer>, IAnswerRepository
    {
        public EFAnswerRepository(DbContext dataContext) : base(dataContext)
        {
        }

        public bool Exists(int id)
        {
            return RepositoryDbSet.Any(e => e.AnswerId == id);
        }
    }
}
