using System;
using System.Collections.Generic;

namespace Palantir00CW9S30320.DTOs
{
    public class PatientWithPrescriptionsDto
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public DateTime BirthDate { get; set; }

        public List<PrescriptionDto> Prescriptions { get; set; }
    }

    public class PrescriptionDto
    {
        public int IdPrescription { get; set; }
        public DateTime Date      { get; set; }
        public DateTime DueDate   { get; set; }
        public DoctorDto Doctor   { get; set; }
        public List<MedicamentDto> Medicaments { get; set; }
    }

    public class DoctorDto
    {
        public int IdDoctor   { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }
    }
}