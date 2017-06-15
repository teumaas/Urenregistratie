using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CCO_Urenregistratie.Models
{
    [Table("Tasks")]
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage="Er moet een project geselecteerd worden")]
        public int ProjectId { get; set; }
        [Required(ErrorMessage="Er moet een beschrijving worden ingevuld")]
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Startdate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Enddate { get; set; }
        //the project of the task
        public virtual Project Project { get; set; }
        //the user of the task
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        //sets the userid of the task
        public void SetUserId(string id)
        {
            UserId = id;
        }

        public double GetHours()
        {
            TimeSpan time = Enddate - Startdate;
            return time.TotalHours;
        }

        public string GetHoursConverted()
        {
            TimeSpan result = TimeSpan.FromHours(GetHours());
            string fromTimeString = result.ToString("hh':'mm':'ss");
            return fromTimeString;
        }
    }
}