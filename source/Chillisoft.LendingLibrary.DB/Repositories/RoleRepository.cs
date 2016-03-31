using System;
using System.Collections.Generic;
using System.Linq;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Core.Interfaces.Repositories;

namespace Chillisoft.LendingLibrary.DB.Repositories
{
    public class RoleRepository:IRolesRepository
    {
        private readonly ILendingLibraryDbContext _lendingLibraryDbContext;

        public RoleRepository(ILendingLibraryDbContext lendingLibraryDbContext)
        {
            if (lendingLibraryDbContext == null) throw new ArgumentNullException(nameof(lendingLibraryDbContext));
            _lendingLibraryDbContext = lendingLibraryDbContext;
        }


        public Roles GetRoleById(string roleId)
        {
            return _lendingLibraryDbContext.Roles.FirstOrDefault(roles => roles.Id==roleId);

        }

        public List<Roles> GetAllRoles()
        {
            return _lendingLibraryDbContext.Roles.ToList();
        }
    }
}