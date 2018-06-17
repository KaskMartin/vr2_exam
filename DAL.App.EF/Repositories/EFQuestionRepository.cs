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
    public class EFQuestionRepository : EFRepository<Question>, IQuestionRepository
    {
        public EFQuestionRepository(DbContext dataContext) : base(dataContext)
        {
        }

        public bool Exists(int id)
        {
            return RepositoryDbSet.Any(e => e.QuestionId == id);
        }
    }
}
