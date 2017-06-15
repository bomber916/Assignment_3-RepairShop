using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepairShop.Models.ViewModels
{
    public class CustomerDetailViewModel
    {
		public Customer Customer { get; set; }
		public ICollection<Device> Devices { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
		[Display(Name ="Total Earned")]
		public decimal TotalEarned { get; set; }
    }
}
