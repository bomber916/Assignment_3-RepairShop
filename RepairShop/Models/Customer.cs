using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepairShop.Models
{
    public class Customer
    {
		public Customer()
		{
			Devices = new HashSet<Device>();
		}
        #region Properties

        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
		[Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
		#endregion

		#region Children
		public ICollection<Device> Devices { get; set; }
		#endregion
	}
}
