using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahegel.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<PatientRecord> Records { get; set; } = new();
    }
}
