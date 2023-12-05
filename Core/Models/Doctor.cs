using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Doctor 
    {
        public int Id { get; set; }      
        public double Price { get; set; }

        //navigation property for Application User
        
        [ForeignKey("ApplicationUsers")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUsers { get; set; }

        //navigation property for specialization
        public virtual Specialization Specialization { get; set; }
        public int SpecializationId { get; set; }

       // nav property for 1-M relationship with Appointment
       //public virtual List<Appointment> Appointments { get; set; } 
       //  هنحطها في ناحية ال ميني علشان قاعدة وان تو ميني باجي في ناحية الميني و احط ال اي دي بتاع ال وان 

    }
}
