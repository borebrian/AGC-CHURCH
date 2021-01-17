using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AGC_CHURCH.Models
{
    public class User
    {
        [Key]
        public string id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string provider { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string ChurchBranch { get; set; }
        [Required]
        public string BabtismStatus { get; set; }
    }
}
