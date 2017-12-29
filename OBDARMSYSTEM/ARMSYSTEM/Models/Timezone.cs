using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARMSYSTEM.Models
{    
    public class Timezone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "id")]
        public int id { get; set; }
        [MaxLength(50)]
        [Display(Name = "�����")]
        public string city { get; set; }
        [MaxLength(50)]
        [Display(Name = "������")]
        public string region { get; set; }
        [MaxLength(50)]
        [Display(Name = "����������� �����")]
        public string FO { get; set; }
        [Display(Name = "���������")]
        public int population { get; set; }
        [Display(Name = "������� ����")]
        public byte timezone { get; set; }
    }
}
