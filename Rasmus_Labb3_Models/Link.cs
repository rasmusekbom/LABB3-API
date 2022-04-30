using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rasmus_labb3_Models
{
    public class Link
    {
        [Key]
        public int LinkId { get; set; }
        [Required]
        public string LinkUrl { get; set; }
        [Required]
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
