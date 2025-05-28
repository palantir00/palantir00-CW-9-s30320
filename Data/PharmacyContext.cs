using Microsoft.EntityFrameworkCore;
using Palantir00CW9S30320.Models;

namespace Palantir00CW9S30320.Data {
    public class PharmacyContext : DbContext {
        public PharmacyContext(DbContextOptions<PharmacyContext> options) : base(options) {}
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1) Klucze główne dla każdej encji:
            modelBuilder.Entity<Patient>()
                .HasKey(p => p.IdPatient);

            modelBuilder.Entity<Doctor>()
                .HasKey(d => d.IdDoctor);

            modelBuilder.Entity<Medicament>()
                .HasKey(m => m.IdMedicament);

            modelBuilder.Entity<Prescription>()
                .HasKey(pr => pr.IdPrescription);

            // 2) Klucz dla tabeli łączącej (wiele-do-wielu):
            modelBuilder.Entity<Prescription_Medicament>()
                .HasKey(pm => new { pm.PrescriptionId, pm.MedicamentId });

            // 3) Relacje:
            modelBuilder.Entity<Prescription_Medicament>()
                .HasOne(pm => pm.Prescription)
                .WithMany(p => p.Prescription_Medicaments)
                .HasForeignKey(pm => pm.PrescriptionId);

            modelBuilder.Entity<Prescription_Medicament>()
                .HasOne(pm => pm.Medicament)
                .WithMany(m => m.Prescription_Medicaments)
                .HasForeignKey(pm => pm.MedicamentId);
            
            modelBuilder.Entity<Patient>().HasData(
                new Patient { IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", BirthDate = new DateTime(1980,1,1) }
            );
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { IdDoctor = 1, FirstName = "Anna", LastName = "Nowak", Email = "anna.nowak@przyklad.pl" }
            );
            modelBuilder.Entity<Medicament>().HasData(
                new Medicament { IdMedicament = 1, Name = "Ibuprofen", Description = "Przeciwbólowy", Price = 5m }
            );

        }

    }
}