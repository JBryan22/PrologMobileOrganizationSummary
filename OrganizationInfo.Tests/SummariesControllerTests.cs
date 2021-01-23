using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using OrganizationInfo.Controllers;
using OrganizationInfo.Models;
using OrganizationInfo.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OrganizationInfo.Tests
{
    [TestClass]
    public class SummariesControllerTests
    {
        Mock<IPrologMobileService> mockService = new Mock<IPrologMobileService>();

        private void MockServiceSetup()
        {
            mockService.Setup(mock => mock.GetOrganizations()).ReturnsAsync(new List<Organization>()
                {
                    new Organization(){Id = "1", Name ="Jesse LLC"},
                    new Organization(){Id = "2", Name ="Bryan Inc."},
                    new Organization(){Id = "3", Name ="ProlabMobile"},
                });

            mockService.Setup(mock => mock.GetOrgUsers("1")).ReturnsAsync(new List<User>()
                {
                    new User(){Id = "1", Email = "jbryguy@hotmail.com"},
                    new User(){Id = "2", Email = "test123@hotmail.com"},
                });
            mockService.Setup(mock => mock.GetOrgUsers("2")).ReturnsAsync(new List<User>()
                {
                    new User(){Id = "3", Email = "user2test1@hotmail.com"},
                    new User(){Id = "4", Email = "ueser2test2@hotmail.com"},
                });
            mockService.Setup(mock => mock.GetOrgUsers("3")).ReturnsAsync(new List<User>()
                {
                    
                });

            mockService.Setup(mock => mock.GetUserPhones("1", "1")).ReturnsAsync(new List<Phone>()
                {
                    new Phone(){Id = "1", Blacklist=true},
                    new Phone(){Id = "2", Blacklist=false},
                    new Phone(){Id = "3", Blacklist=true},
                });
            mockService.Setup(mock => mock.GetUserPhones("1", "2")).ReturnsAsync(new List<Phone>()
                {
                    new Phone(){Id = "4", Blacklist=false},
                    new Phone(){Id = "5", Blacklist=false},
                    new Phone(){Id = "6", Blacklist=true},
                });
            mockService.Setup(mock => mock.GetUserPhones("2", "3")).ReturnsAsync(new List<Phone>()
                {
                    new Phone(){Id = "7", Blacklist=false},
                    new Phone(){Id = "8", Blacklist=false},
                });
            mockService.Setup(mock => mock.GetUserPhones("2", "4")).ReturnsAsync(new List<Phone>()
                {
                    new Phone(){Id = "9", Blacklist=false},
                    new Phone(){Id = "10", Blacklist=false},
                });
        }

        [TestMethod]
        public async void  TestGetOrganizationSummaries()
        {
            SummariesController controller = new SummariesController(mockService.Object);

            JsonResult os = await controller.GetOrganizationSummaries();
            string Osjson = JsonConvert.SerializeObject(os);

            List<OrganizationSummary> expectedObj = new List<OrganizationSummary>()
            {
                new OrganizationSummary()
                {
                    Id = "1",
                    Name = "Jesse LLC",
                    BlacklistTotal = "3",
                    TotalCount = "6",
                    Users = new List<User>()
                    {
                        new User(){Id = "1", Email = "jbryguy@hotmail.com", PhoneCount = 3},
                        new User(){Id = "2", Email = "test123@hotmail.com", PhoneCount = 3},
                    }
                },
                new OrganizationSummary()
                {
                    Id = "2",
                    Name = "Bryan Inc.",
                    BlacklistTotal = "0",
                    TotalCount = "4",
                    Users = new List<User>()
                    {
                        new User(){Id = "3", Email = "user2test1@hotmail.com", PhoneCount = 2},
                        new User(){Id = "4", Email = "ueser2test2@hotmail.com", PhoneCount = 2},
                    }
                },
                new OrganizationSummary()
                {
                    Id = "3",
                    Name = "ProlabMobile",
                    BlacklistTotal = "0",
                    TotalCount = "0",
                    Users = new List<User>()
                }
            };

            string expected = JsonConvert.SerializeObject(expectedObj);

            Assert.AreEqual(expected, Osjson);
        }
    }
}
