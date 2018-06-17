using System;
using System.Collections.Generic;
using System.Text;
using DAL.EF;
using DAL.App.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.TaisKoht.EF.Repositories
{
    public class EFApplicationUserRepository : EFRepository<ApplicationUser>, IApplicationUserRepository
    {
        public EFApplicationUserRepository(DbContext dataContext) : base(dataContext)
        {
        }
        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
