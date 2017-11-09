using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARMSYSTEM.Models
{    
    public class PhoneBases
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "id")]
        public int id { get; set; }
        [Display(Name = "Phone")]
        public long? Phone { get; set; }
        [Display(Name = "id Project")]
        public int? idProject { get; set; }
        [Display(Name = "id Base")]
        public int? idBase { get; set; }
    }
}
