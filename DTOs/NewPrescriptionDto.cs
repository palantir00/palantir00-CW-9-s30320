using System;
using System.Collections.Generic;

namespace Palantir00CW9S30320.DTOs
{
    public class NewPrescriptionDto
    {
        public DateTime Date    { get; set; }
        public DateTime DueDate { get; set; }
        public int PatientId    { get; set; }
        public int DoctorId     { get; set; }
        public List<MedicamentDto> Medicaments { get; set; }
    }

    public class MedicamentDto
    {
        public int IdMedicament  { get; set; }
        public int Dose          { get; set; }
        public string Description{ get; set; }
    }
}