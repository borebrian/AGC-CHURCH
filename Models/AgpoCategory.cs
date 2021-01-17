using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AGC_CHURCH.Models
{
    public class AgpoCategory
    {
        [Key]
        [MaxLength(50)]

        public string id { get; set; }
        [Required]
        public string strAgpo { get; set; }
    }
}
