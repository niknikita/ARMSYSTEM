using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARMSYSTEM.Models
{
    [Table("Filters")]
    public class Filter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "id")]
        public int id { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }        
        [Display(Name = "Template")]
        public string Template { get; set; }
    }
}
