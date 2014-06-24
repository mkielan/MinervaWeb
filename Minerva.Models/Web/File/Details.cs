using Minerva.Models.Web.Comment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Minerva.Models.Web.File
{
    public class Details
    {
        [HiddenInput(DisplayValue=false)]
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Model))]
        public string Name { get; set; }

        [Display(Name = "Creator", ResourceType = typeof(Resources.Model))]
        public string Creator { get; set; }

        [Display(Name = "LastModificator", ResourceType = typeof(Resources.Model))]
        public string LastModificator { get; set; }

        [Display(Name = "Created", ResourceType = typeof(Resources.Model))]
        public DateTime CreatedTime { get; set; }

        [Display(Name = "LastModification", ResourceType = typeof(Resources.Model))]
        public DateTime? ModificationTime { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Model))]
        public string Description { get; set; }

        [Display(Name = "AvailableFor", ResourceType = typeof(Resources.Model))]
        public string[] AvailabeFor { get; set; }
    }
}
