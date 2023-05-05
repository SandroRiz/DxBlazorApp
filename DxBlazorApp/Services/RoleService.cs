using Microgate.Extranet.Models;
using Microgate.Extranet.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microgate.Extranet.Services
{
    public class RoleService : BaseService<AspNetRole, String>
    {
        public RoleService(IDbContextFactory<ExtranetDbContext> factory) : base(factory)
        {

        }

        //QUESTO NON PUÒ ESSERE CAMBIATO, PERCHÉ il Datacontext DEVE essere utilizzato in questo modo
        
        public async Task<List<AspNetRole>> ListRolesAsync()
        {
			using var ctx = await CtxFactory.CreateDbContextAsync();
            var roles = await ctx.AspNetRoles.ToListAsync();
            return roles;
        }

        public async Task<List<AspNetRole>> GetUserRolesByUserId(string userId)
        {
            using var ctx = await CtxFactory.CreateDbContextAsync();
            var q = from r in ctx.AspNetRoles
                    join ur in ctx.AspNetUserRoles on r.Id equals ur.RoleId
                    join u in ctx.AspNetUsers on ur.UserId equals u.Id
                    where u.Id == userId
                    select r;


            var roles = await q.ToListAsync();

            return roles;
        }
    }
}
