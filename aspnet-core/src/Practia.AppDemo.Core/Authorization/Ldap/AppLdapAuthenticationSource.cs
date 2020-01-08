using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Practia.AppDemo.Authorization.Users;
using Practia.AppDemo.MultiTenancy;

namespace Practia.AppDemo.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}