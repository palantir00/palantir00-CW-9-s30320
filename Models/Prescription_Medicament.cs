using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Palantir00CW9S30320.Models {
    public class Prescription_Medicament {
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }
        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }
        public int Dose { get; set; }
        public string Description { get; set; }
    }
}