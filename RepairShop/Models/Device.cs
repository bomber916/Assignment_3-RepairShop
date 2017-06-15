using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepairShop.Models
{
    public class Device
    {
        #region Properties

        [Required]
        public long Id { get; set; }

        [Required]
        public long CustomerId { get; set; }

        [Required]
		[Display(Name = "Serial #")]
        public string SerialNbr { get; set; }

        public string Type { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        #endregion

        #region Parents

        public virtual Customer Customer { get; set; }

        #endregion

        #region Children

        public virtual ICollection<DeviceService> DeviceServices { get; set; }

        #endregion

        public Device()
        {
            this.DeviceServices = new HashSet<DeviceService>();
        }
    }
}
