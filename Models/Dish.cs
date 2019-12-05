using System.ComponentModel.DataAnnotations;
using System;

namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId {get;set;}

        [Required]
        [MaxLength(45)]
        [Display(Name="Name of Dish")]
        public string Name {get;set;}

        [Required]
        [MaxLength(45)]
        [Display(Name="Chef's Name")]
        public string Chef {get;set;}

        [Required]
        [Display(Name="Tastiness")]
        public int Tastiness {get;set;}

        [Required]
        [Display(Name="# of Calories")]
        public int Calories {get;set;}

        [Required]
        [Display(Name="Description")]
        public string Description {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdateddAt {get;set;} = DateTime.Now;
    }
}