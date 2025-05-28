using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Palantir00CW9S30320.Data;
using Palantir00CW9S30320.DTOs;

namespace Palantir00CW9S30320.Services
{
    public interface IPrescriptionService
    {
        Task AddPrescriptionAsync(NewPrescriptionDto dto);
        Task<PatientWithPrescriptionsDto> GetPatientAsync(int patientId);
    }
}