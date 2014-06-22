using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Minerva.Models.Web.Share
{
    public class ShareModel
    {
        [Required(ErrorMessageResourceName="Required", ErrorMessageResourceType=typeof(Resources.Validation))]
        [Display(Name = "Usernames", ResourceType = typeof(Resources.Model))]
        public string Usernames { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int[] DiskItemIds { get; set; }

        public string[] Users() {
            if(Usernames == null) return new string[0];

            return Usernames.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
