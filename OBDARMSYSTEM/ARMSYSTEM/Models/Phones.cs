using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ARMSYSTEM.Models
{
    public class Phones
    {
        [Display(Name = "id")]
        public long Id { get; set; }
        [Display(Name = "Телефон")]
        public long Phone { get; set; }              //телефон
        [Display(Name = "Город")]
        public string City { get; set; }             //Город
        [Display(Name = "Улица")]
        public string Street { get; set; }           //Улица
        [Display(Name = "Дом")]
        public string House { get; set; }            //Дом
        [Display(Name = "Квартира/офис")]
        public string Apartment { get; set; }        //квартира, офис, комната
        [Display(Name = "Пол")]
        public string Sex { get; set; }              //Пол
        [Display(Name = "Имя")]
        public string Name { get; set; }             //Имя
        [Display(Name = "Фамилия")]
        public string Family { get; set; }           //Фамилия
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }       //Отчество

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата актуальности")]
        public DateTime DateUpdate { get; set; }       // добавить дату актуальности
        //[Display(Name ="Проекты")]
        //public List<int> Porjects { get; set; }// проект/проекты придумать удобный вывод если >1
    }
}
