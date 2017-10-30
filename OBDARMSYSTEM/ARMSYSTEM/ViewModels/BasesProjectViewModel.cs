using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARMSYSTEM.ViewModels
{
    public class BasesProjectViewModel
    {
        [Display(Name = "id")]
        public int id { get; set; }
     
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Display(Name = "Название проекта")]
        public string ProjectName { get; set; }

        [Display(Name = "номер родительской базы")]
        public int idParrentBase { get; set; }
             
        [Display(Name = "фильтры")]
        public string filters { get; set; }
        
        [Display(Name = "Описание")]
        public string description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата создания")]
        public DateTime dateCreate { get; set; }
        

    }
    
}
