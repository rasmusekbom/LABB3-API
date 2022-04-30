using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rasmus_labb3_Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        [Required]
        public string Name { get; set; }
        public int PhoneNr { get; set; }
    }
}
