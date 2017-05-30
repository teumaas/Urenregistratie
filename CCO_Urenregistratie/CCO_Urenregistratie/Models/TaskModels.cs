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
    public class TaskModels
    {
    }
    [Table("Tasks")]
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; private set; }
        public int ProjectId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public virtual Project Project { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public void SetUserId(string id)
        {
            UserId = id;
        }
    }
}