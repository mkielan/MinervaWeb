using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minerva.Resources;

namespace Minerva.Models.Api.Directory
{
    public class Add
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        [StringLength(200, ErrorMessageResourceName="StringLength", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string Name { get; set; }

        //[Required]
        public long? ParentId { get; set; }

        [StringLength(400, ErrorMessageResourceName="StringLength", ErrorMessageResourceType = typeof(Resources.Validation))]
        [Display(Name = "Description", ResourceType = typeof(Resources.Model))]
        public string Description { get; set; }
    }
}
