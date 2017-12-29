using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ARMSYSTEM.ViewModels
{
    public class FilterItemViewModel
    {
        public bool Include { get; set; } //по этому параметру определяется добавлять в выборку или исключение из выборки
        public bool IsStatic { get; set; } //стационарный телефон да/нет
        public bool IsMobile { get; set; } //сотовый телефон да/нет
        public bool DateTimeRange { get; set; } // выбор временного диапазона обновления данных
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата начала")]
        public DateTime DataTimeRangeStart { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата конца")]
        public DateTime DataTimeRangeFinish { get; set; }
        public string Cities { get; set; } //Список городов        
        public string Streets { get; set; } //Список улиц
    }
}
