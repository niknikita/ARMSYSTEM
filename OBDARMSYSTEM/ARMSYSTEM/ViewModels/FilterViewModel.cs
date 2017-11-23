using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARMSYSTEM.Services.FiltersFizPhone;

namespace ARMSYSTEM.ViewModels
{
    public class FilterViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public List<FilterTemplate> Template { get; set; }


        //public bool Include { get; set; } //по этому параметру определяется добавлять в выборку или исключение из выборки
        //public bool IsStatic { get; set; } //стационарный телефон да/нет
        //public bool IsMobile { get; set; } //сотовый телефон да/нет
        //public bool DateTimeRange { get; set; } // выбор временного диапазона обновления данных
        //public DateTime DataTimeRangeStart { get; set; }
        //public DateTime DataTimeRangeFinish { get; set; }
        //public List<string> Cities { get; set; } //Список городов        
        //public List<string> Streets { get; set; } //Список улиц
    }
}
