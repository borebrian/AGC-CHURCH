using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AGC_CHURCH.Models
{
    public class Gender
    {
        [Key]
        [MaxLength(50)]
      
        public string id { get; set; }
        [Required]
        public string strGender { get; set; }
    }
}
