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
    [Table("Project")]
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required(AllowEmptyStrings= false, ErrorMessage="Er moet een projectnaam worden ingevuld")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Color { get; set; }
        //list of all tasks of the project
        public virtual ICollection<Tasks> Tasks { get; set; }
        //user of the project
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        //sets the userid of the project
        public void SetUserId(string id)
        {
            UserId = id;
        }
        //gets the total time spend on the project
        public double GetTotalTime()
        {
            if (Tasks == null)
            {
                return 0;
            }
            double totalHours = 0;
            foreach (Tasks task in Tasks)
            {
                totalHours += task.GetHours();
            }
            return totalHours;
        }
        //gets the total time of the project formatted in 'hh:mm:ss'
        public string GetHoursConverted()
        {
            TimeSpan result = TimeSpan.FromHours(GetTotalTime());
            string fromTimeString = result.ToString("hh':'mm':'ss");
            return fromTimeString;
        }
        //get the totaltime of the project formatted in 'hh.mm'
        public string GetHoursForChart()
        {
            TimeSpan result = TimeSpan.FromHours(GetTotalTime());
            string fromTimeString = result.ToString("hh'.'mm");
            return fromTimeString;
        }

        //gets the totaltime of tasks of the project formatted in 'hh:mm'
        public string GetHoursForChartText()
        {
            TimeSpan result = TimeSpan.FromHours(GetTotalTime());
            string fromTimeString = result.ToString("hh':'mm");
            return fromTimeString;
        }
    }
}