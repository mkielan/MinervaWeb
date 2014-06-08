using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources.Internal
{
    public class File : AbstractFkEntity<Int64>
    {
        [Required]
        [MaxLength(200)]
        public string Extension { get; set; }

        public string MimeType { get; set; }

        public string Content { get; set; }
        
        public DiskStructure DiskStructure { get; set; }

        /// <summary>
        /// Plik w postaci tablicy bajtów. Atrybut nie mapowany do bazy.
        /// Powinien być obsłużony w repozytorium tak, 
        /// aby pobierać ten plik z jakiejś przestrzeni dyskowej.
        /// </summary>
        [NotMapped]
        public byte[] Body { get; set; }
    }
}
