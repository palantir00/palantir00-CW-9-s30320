using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Palantir00CW9S30320.Data;
using Palantir00CW9S30320.DTOs;
using Palantir00CW9S30320.Models;

namespace Palantir00CW9S30320.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly PharmacyContext _context;
        public PrescriptionService(PharmacyContext context)
        {
            _context = context;
        }

        public async Task AddPrescriptionAsync(NewPrescriptionDto dto)
        {
            // 1. Walidacja ilości leków
            if (dto.Medicaments == null || dto.Medicaments.Count == 0)
                throw new InvalidOperationException("Lista leków nie może być pusta.");
            if (dto.Medicaments.Count > 10)
                throw new InvalidOperationException("Nie można dodać więcej niż 10 leków na jednej recepcie.");

            // 2. Sprawdzenie poprawności dat
            if (dto.DueDate < dto.Date)
                throw new InvalidOperationException("Data realizacji (DueDate) musi być późniejsza lub równa dacie wystawienia (Date).");

            // 3. Pobierz pacjenta
            var patient = await _context.Patients.FindAsync(dto.PatientId);
            if (patient == null)
                throw new KeyNotFoundException($"Pacjent o Id={dto.PatientId} nie istnieje.");

            // 4. Pobierz doktora
            var doctor = await _context.Doctors.FindAsync(dto.DoctorId);
            if (doctor == null)
                throw new KeyNotFoundException($"Doktor o Id={dto.DoctorId} nie istnieje.");

            // 5. Pobierz listę leków i zweryfikuj, czy wszystkie istnieją
            var medIds = dto.Medicaments.Select(m => m.IdMedicament).ToList();
            var medsFromDb = await _context.Medicaments
                                          .Where(m => medIds.Contains(m.IdMedicament))
                                          .ToListAsync();
            if (medsFromDb.Count != medIds.Count)
                throw new KeyNotFoundException("Jeden lub więcej leków na recepcie nie istnieje w bazie.");

            // 6. Utwórz encję recepty
            var prescription = new Prescription
            {
                Date  = dto.Date,
                DueDate = dto.DueDate,
                Patient = patient,
                Doctor  = doctor
            };

            // 7. Dodaj pozycje łączące (Prescription_Medicament)
            prescription.Prescription_Medicaments = dto.Medicaments
                .Select(m => new Prescription_Medicament
                {
                    MedicamentId = m.IdMedicament,
                    Dose         = m.Dose,
                    Description  = m.Description
                })
                .ToList();

            // 8. Zapisz receptę
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
        }

        public async Task<PatientWithPrescriptionsDto> GetPatientAsync(int patientId)
        {
            // 1. Pobierz pacjenta wraz z receptami, lekarzami i lekami
            var patient = await _context.Patients
                .Include(p => p.Prescriptions)
                    .ThenInclude(pr => pr.Doctor)
                .Include(p => p.Prescriptions)
                    .ThenInclude(pr => pr.Prescription_Medicaments)
                        .ThenInclude(pm => pm.Medicament)
                .SingleOrDefaultAsync(p => p.IdPatient == patientId);

            if (patient == null)
                throw new KeyNotFoundException($"Pacjent o Id={patientId} nie istnieje.");

            // 2. Zmapuj na DTO
            var result = new PatientWithPrescriptionsDto
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName  = patient.LastName,
                BirthDate = patient.BirthDate,
                Prescriptions = patient.Prescriptions
                    .OrderBy(pr => pr.DueDate)
                    .Select(pr => new PrescriptionDto
                    {
                        IdPrescription = pr.IdPrescription,
                        Date           = pr.Date,
                        DueDate        = pr.DueDate,
                        Doctor = new DoctorDto
                        {
                            IdDoctor  = pr.Doctor.IdDoctor,
                            FirstName = pr.Doctor.FirstName,
                            LastName  = pr.Doctor.LastName
                        },
                        Medicaments = pr.Prescription_Medicaments
                            .Select(pm => new MedicamentDto
                            {
                                IdMedicament = pm.MedicamentId,
                                Dose         = pm.Dose,
                                Description  = pm.Description
                            })
                            .ToList()
                    })
                    .ToList()
            };

            return result;
        }
    }
}
