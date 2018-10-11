using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chefs.Models
{
    public class User
    {   
        [Key]
        public int Chefid {get;set;}
      
        [Required]
        [MinLength(3)]
        public string FirstName {get;set;}

        [Required]
        [MinLength(3)]
        public string LastName {get;set;}

        [Required]

        public DateTime DOB {get;set;}


        public List<Dish> dishes {get;set;}

        public int Age 
        {
            get { return DateTime.Now.Year - DOB.Year;}
        }

    }
    public class Dish 
    {
        [Key]
        public int Dishid {get;set;}

        [Required]
        [MinLength(3)]
        public string Name {get;set;}

        [Required]
        [Range(5,100000)]
        public int Calories {get;set;}

        [Required]
        [MinLength(3)]
        public string Description {get;set;}

        [Required]
        public int Tastiness {get;set;}

        [Required]
        public int Chef_Chefid {get;set;}   

        [ForeignKey("Chef_Chefid")]
        public User Chef {get;set;} 
    }
}
