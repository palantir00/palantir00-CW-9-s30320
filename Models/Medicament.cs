using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Palantir00CW9S30320.Models {
    public class Medicament {
        public int IdMedicament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; } = new List<Prescription_Medicament>();
    }
}