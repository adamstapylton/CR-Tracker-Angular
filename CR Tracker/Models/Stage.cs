using System.ComponentModel.DataAnnotations;

namespace CR_Tracker.Models
{
    public class Stage
    {
        public int StageId { get; set; }
        [Required]
        public string StageName { get; set; }
        [Required]
        public int StageOrder { get; set; }
    }
}