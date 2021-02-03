using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CR_Tracker.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        [Required]
        public DateTime NoteDate { get; set; }
        public string Description { get; set; }

    }
}
