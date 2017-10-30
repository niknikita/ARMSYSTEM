using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARMSYSTEM
{    
    public class DefCode
    {
        [Display(Name = "id")]
        public long id { get; set; }

        [Display(Name = "defcode")]
        public int? defcode { get; set; }

        [Display(Name = "������ ���������")]
        public long range1 { get; set; }

        [Display(Name = "����� ���������")]
        public long range2 { get; set; }

        [MaxLength(200)]
        [Display(Name = "���. �����")]
        public string geo { get; set; }

        [MaxLength(100)]
        [Display(Name = "���������")]
        public string provider { get; set; }
    }
}
