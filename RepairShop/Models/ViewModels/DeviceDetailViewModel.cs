using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairShop.Models.ViewModels
{
    public class DeviceDetailViewModel
    {
		public Device Device { get; set; }
		public PaginatedList<DeviceService> DeviceServices { get; set; }
    }
}
