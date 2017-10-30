using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ARMSYSTEM.Models
{
    public class BlackList
    {
        [Key]
        public long id { get; set; }
        [Display(Name ="Номер")]        
        public long Phone { get; set; }
        [Display(Name = "Причина")]
        public string descripton { get; set; }
    }
}
