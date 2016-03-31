using System.Collections.Generic;
using Chillisoft.LendingLibrary.Core.Domain;

namespace Chillisoft.LendingLibrary.Core.Interfaces.Repositories
{
    public interface IRolesRepository
    {
        Roles  GetRoleById(string roleId);
        List<Roles> GetAllRoles();
    }
}