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
    public class ProjectModels
    {
    }
    [Table("Project")]
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Color { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public void SetUserId(string id)
        {
            UserId = id;
        }
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

        public string GetHoursConverted()
        {
            TimeSpan result = TimeSpan.FromHours(GetTotalTime());
            string fromTimeString = result.ToString("hh':'mm':'ss");
            return fromTimeString;
        }

        public string GetHoursForChart()
        {
            TimeSpan result = TimeSpan.FromHours(GetTotalTime());
            string fromTimeString = result.ToString("hh'.'mm");
            return fromTimeString;
        }

        public string GetHoursForChartText()
        {
            TimeSpan result = TimeSpan.FromHours(GetTotalTime());
            string fromTimeString = result.ToString("hh':'mm");
            return fromTimeString;
        }
    }
}