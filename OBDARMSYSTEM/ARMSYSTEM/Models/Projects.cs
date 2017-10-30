using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ARMSYSTEM.Models
{
    public class Projects
    {
        [Display(Name = "id")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Descripion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата создания")]
        public DateTime DateCreate { get; set; }
    }
}
