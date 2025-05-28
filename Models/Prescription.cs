using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Palantir00CW9S30320.Models {
    public class Prescription {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; } = new List<Prescription_Medicament>();
    }
}