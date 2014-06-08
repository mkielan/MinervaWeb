using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources
{
    public class FtpSource : AbstractFkEntity<Int64>
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public virtual Source Source { get; set; }
    }
}
