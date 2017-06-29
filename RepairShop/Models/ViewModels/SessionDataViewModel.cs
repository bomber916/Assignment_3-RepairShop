using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepairShop.Models.ViewModels
{
	public class SessionDataViewModel
	{
		public User User { get; set; }
		public List<DeviceService> ShoppingCart {get;set;}

		[DisplayFormat(DataFormatString = "{0:C}")]
		public Decimal SubTotal
		{
			get
			{
				Decimal subTotal = 0;
				foreach(var item in ShoppingCart)
				{
					subTotal += item.Service.Price * item.Quantity ?? 0;
				}
				return subTotal;
			}
		}
    }
}
