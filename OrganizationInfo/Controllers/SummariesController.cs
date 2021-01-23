using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrganizationInfo.Models;
using OrganizationInfo.Services;

namespace OrganizationInfo.Controllers
{
    [Route("api/summaries")]
    public class SummariesController : Controller
    {
        private readonly IPrologMobileService _service;
        public SummariesController(IPrologMobileService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<JsonResult> GetOrganizationSummaries()
        {
            List<OrganizationSummary> summary = new List<OrganizationSummary>();

            IEnumerable<Organization> orgs = await _service.GetOrganizations();

            foreach (Organization org in orgs)
            {
                OrganizationSummary os = new OrganizationSummary();
                os.Id = org.Id;
                os.Name = org.Name;
                int blacklistTotal = 0;
                int totalCount = 0;

                //dodging the rate limit
                await Task.Delay(1000);

                IEnumerable<User> orgUsers = await _service.GetOrgUsers(org.Id);

                foreach (User user in orgUsers)
                {
                    //dodging rate limit
                    await Task.Delay(1000);

                    os.Users.Add(user);
                    IEnumerable<Phone> userPhones = await _service.GetUserPhones(org.Id, user.Id);

                    foreach (Phone phone in userPhones)
                    {
                        user.PhoneCount++;
                        totalCount++;
                        if (phone.Blacklist)
                        {
                            blacklistTotal++;
                        }
                    }
                }
                os.BlacklistTotal = blacklistTotal.ToString();
                os.TotalCount = totalCount.ToString();
                summary.Add(os);
            }

            return Json(summary);
        }
    }
}
