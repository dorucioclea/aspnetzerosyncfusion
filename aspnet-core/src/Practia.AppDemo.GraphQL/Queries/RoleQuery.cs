using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Practia.AppDemo.Authorization;
using Practia.AppDemo.Authorization.Roles;
using Practia.AppDemo.Core.Base;
using Practia.AppDemo.Core.Extensions;
using Practia.AppDemo.Dto;
using Practia.AppDemo.Types;

namespace Practia.AppDemo.Queries
{
    public class RoleQuery : AppDemoQueryBase<ListGraphType<RoleType>, List<RoleDto>>
    {
        private readonly RoleManager _roleManager;

        public static class Args
        {
            public const string Id = "id";
            public const string TenantId = "tenantId";
            public const string Name = "name";
        }

        public RoleQuery(RoleManager roleManager)
            : base("roles", new Dictionary<string, Type>
                {
                    {Args.Id, typeof(IdGraphType)},
                    {Args.TenantId, typeof(IntGraphType)},
                    {Args.Name, typeof(StringGraphType)}
                }
            )
        {
            _roleManager = roleManager;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Roles)]
        protected override async Task<List<RoleDto>> Resolve(ResolveFieldContext<object> context)
        {
            var query = _roleManager.Roles.AsNoTracking();

            context
                .ContainsArgument<int>(Args.Id, id => query = query.Where(r => r.Id == id))
                .ContainsArgument<string>(Args.TenantId, name => query = query.Where(r => r.Name == name))
                .ContainsArgument<int?>(Args.Name, tenantId => query = query.Where(r => r.TenantId == tenantId.Value));

            return await ProjectToListAsync<RoleDto>(query);
        }
    }
}