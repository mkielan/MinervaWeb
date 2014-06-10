using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources
{
    public class File : AbstractEntity
    {
        [Key, ForeignKey("DiskStructure")]
        public long DiskStructureId { get; set; }

        [Required]
        [MaxLength(5)]
        public string Extension { get; set; }

        /// <summary>
        /// Plik w postaci tablicy bajtów. Atrybut nie mapowany do bazy.
        /// Powinien być obsłużony w repozytorium tak, 
        /// aby pobierać ten plik z jakiejś przestrzeni dyskowej.
        /// </summary>
        [NotMapped]
        public byte[] Body { get; set; }

        public virtual DiskStructure DiskStructure { get; set; }
    }
}
