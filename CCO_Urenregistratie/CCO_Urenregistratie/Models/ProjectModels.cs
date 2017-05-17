using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        public int UserId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Color { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}