using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepairShop.Models
{
    public class DeviceService
    {
        #region Properties

        [Required]
        public long Id { get; set; }

        [Required]
        public long DeviceId { get; set; }

        [Required]
        public long ServiceId { get; set; }

		[Display(Name ="Date Started")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime? DateStarted { get; set; }

		[Display(Name ="Date Completed")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime? DateCompleted { get; set; }

		[MaxLength(2000, ErrorMessage = "Maximum length for this field is 2000 characters.")]
        public string Comments { get; set; }

        #endregion

        #region Parents

        public virtual Device Device { get; set; }
        public virtual Service Service { get; set; }

        #endregion
    }
}
