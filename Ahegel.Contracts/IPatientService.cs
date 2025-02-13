using Ahegel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahegel.Contracts
{
    public interface IPatientService
    {
        // Service interface for managing patients.

        // Retrieves all patients.
        Task<List<Patient>> GetPatients();

        // Retrieves a specific patient by ID.
        Task<Patient> GetPatient(int id);

        // Creates a new patient.
        Task<Patient> CreatePatient(Patient patient);

        // Updates an existing patient.
        Task<Patient> UpdatePatient(int id, Patient patient);

        // Soft deletes a patient by ID.
        Task<bool> SoftDeletePatient(int id);
    }
}
