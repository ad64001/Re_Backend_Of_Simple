﻿using Re_Backend.Common.Attributes;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.IRespository;
using Re_Backend.Domain.UserDomain.IServices;

namespace Re_Backend.Application.UserAplication
{
    [Injectable]
    public class RoleService : IRoleService
    {
        private readonly IRolesRespository _rolesRespository;

        public RoleService(IRolesRespository rolesRespository)
        {
            _rolesRespository = rolesRespository;
        }
        public async Task<List<Role>> GetRoles()
        {
            return await _rolesRespository.QueryAllRole();
        }
    }
}
