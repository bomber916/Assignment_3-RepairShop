using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RepairShop.Models
{
    public class User
    {
		public User()
		{
			Devices = new HashSet<Device>();
		}
        #region Properties

        [Required]
        public long Id { get; set; }

		[Required]
		[StringLength(30)]
		public string UserName { get; set; }

        [Required]
        public string Name { get; set; }

		[Required]
		public string Password { get; set; }

        public string Address { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
		[Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

		public string UserType { get; set; }
		#endregion

		#region Children
		public ICollection<Device> Devices { get; set; }
		#endregion
	}
}
