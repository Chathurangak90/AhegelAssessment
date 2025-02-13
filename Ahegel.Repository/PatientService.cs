using Ahegel.Contracts;
using Ahegel.Entities;
using Ahegel.Entities.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahegel.Repository
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;
        public PatientService(ApplicationDbContext context) { _context = context; }

        // Retrieves all non-deleted patients with their records.
        public async Task<List<Patient>> GetPatients()
        {
            return await _context.Patients
                .Where(p => !p.IsDeleted)
                .Select(p => new Patient
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    Records = p.Records.Select(r => new PatientRecord
                    {
                        Id = r.Id,
                        PatientId = r.Id,
                        Description = r.Description,
                        CreatedAt = r.CreatedAt
                    }).ToList()
                })
                .ToListAsync();
        }


        // Retrieves a specific patient by ID with their records.
        public async Task<Patient> GetPatientById(int id) =>
            await _context.Patients.Include(p => p.Records).FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        public async Task<Patient> CreatePatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        // Updates an existing patient record.
        public async Task<Patient> UpdatePatient(int id, Patient patient)
        {
            var existingPatient = await _context.Patients.FindAsync(id);
            if (existingPatient == null || existingPatient.IsDeleted) return null;

            existingPatient.Name = patient.Name;
            existingPatient.Age = patient.Age;
            await _context.SaveChangesAsync();
            return existingPatient;
        }

        // deletes a patient record.
        public async Task<bool> SoftDeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;

            patient.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
