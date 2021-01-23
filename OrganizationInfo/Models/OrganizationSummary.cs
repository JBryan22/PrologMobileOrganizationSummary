using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationInfo.Models
{
    public class OrganizationSummary
    {
        public OrganizationSummary()
        {
            Users = new Collection<User>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string BlacklistTotal { get; set; }
        public string TotalCount { get; set; }
        public ICollection<User> Users { get; set; }

		//this equality override turned out to be unnecessary, but i'll keep it here in case it needs to be used later
		public override bool Equals(System.Object otherProduct)
		{
			if (!(otherProduct is OrganizationSummary))
			{
				return false;
			}
			else
			{
				OrganizationSummary newOs = (OrganizationSummary)otherProduct;

				return this.Id.Equals(newOs.Id) &&
					   this.Name.Equals(newOs.Name) &&
					   this.BlacklistTotal.Equals(newOs.BlacklistTotal) &&
					   this.TotalCount.Equals(newOs.TotalCount) &&
					   this.Users.SequenceEqual(newOs.Users);
			}
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}
