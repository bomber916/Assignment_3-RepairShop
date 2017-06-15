using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepairShop.Models
{
    public class Service
    {
        #region Properties

        [Required]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
		public decimal Price { get; set; }

		[MaxLength(2000, ErrorMessage ="Maximum length for this field is 2000 characters.")]
        public string Description { get; set; }

        #endregion

        #region Children

        public virtual ICollection<DeviceService> DeviceServices { get; set; }

        #endregion

        public Service()
        {
            this.DeviceServices = new HashSet<DeviceService>();
        }
    }
}
