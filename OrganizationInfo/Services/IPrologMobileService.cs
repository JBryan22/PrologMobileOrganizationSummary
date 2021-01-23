using OrganizationInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationInfo.Services
{
    public interface IPrologMobileService
    {
        public Task<IEnumerable<Organization>> GetOrganizations();
        public Task<IEnumerable<User>> GetOrgUsers(string id);
        public Task<IEnumerable<Phone>> GetUserPhones(string orgId, string userId);
    }
}
