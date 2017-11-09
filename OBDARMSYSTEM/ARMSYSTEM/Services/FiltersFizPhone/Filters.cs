using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMSYSTEM.Services.FiltersFizPhone
{
    public class Filters
    {
        public int Id { get; set; }
        public bool Exclude { get; set; } //по этому параметру определяется добавлять в выборку или исключение из выборки
        public bool IsStatic { get; set; } //стационарный телефон да/нет
        public bool IsMobile { get; set; } //сотовый телефон да/нет
        public bool DateTimeRange { get; set; } // выбор временного диапазона обновления данных
        public DateTime DataTimeRangeStart { get; set; }
        public DateTime DataTimeRangeFinish { get; set; }
        public List<string> MaskPhones { get; set; } //Маски телефонов ("7351*", "73519*", "735124700??")
        public List<string> Cities { get; set; } //Список городов        
        public Dictionary<string, string> Streets { get; set; } //Список улиц (город, улица)        
    }
}
