using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Api.File
{
    public class View
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
        
        /// <summary>
        /// Rozszerzenie
        /// </summary>
        public string Ext { get; set; }

        public string Creator { get; set; }

        /// <summary>
        /// Ostatnia modyfikująca osoba
        /// </summary>
        public string LastModificator { get; set; }

        public string Phone { get; set; }

        public string[] Tags { get; set; }
    }
}
