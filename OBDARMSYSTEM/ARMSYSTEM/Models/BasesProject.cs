using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARMSYSTEM.Models
{    
    public class BasesProject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]        
        [Display(Name = "id")]
        public int id { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "��������")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "����� �������")]
        public int idProject { get; set; }

        [Display(Name = "����� ������������ ����")]
        public int idParrentBase { get; set; }

        [Required]
        [Display(Name = "�������")]
        public string filters { get; set; }

        [MaxLength]
        [Display(Name = "��������")]
        public string description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [Required]
        [Display(Name = "���� ��������")]
        public DateTime dateCreate { get; set; }
    }
}
