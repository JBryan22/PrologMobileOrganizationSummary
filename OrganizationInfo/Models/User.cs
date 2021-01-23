using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationInfo.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public int PhoneCount { get; set; } = 0;

        public override bool Equals(System.Object otherProduct)
		{
			if (!(otherProduct is User))
			{
				return false;
			}
			else
			{
				User newUser = (User)otherProduct;


				return this.Id.Equals(newUser.Id) &&
					   this.Email.Equals(newUser.Email) &&
					   this.PhoneCount.Equals(newUser.PhoneCount);
			}
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
    }


}
