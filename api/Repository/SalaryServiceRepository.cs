using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class SalaryServiceRepository : ISalaryServiceRepository
    {
        private readonly ApplicationDBContext _context;

        public SalaryServiceRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<SalaryService> AssignEmployeeToService(SalaryService employeeService)
        {
            _ = await _context.SalaryServices.AddAsync(employeeService);
            _ = await _context.SaveChangesAsync();
            return employeeService;
        }

        public async Task<SalaryService> UpdateEmployeeService(int employeeServiceId, SalaryService employeeService)
        {
            var existingService = await _context.SalaryServices.FindAsync(employeeServiceId);
            if (existingService == null) return null;

            existingService.ServiceeId = employeeService.ServiceeId;
            existingService.AppUserId = employeeService.AppUserId;

            _ = await _context.SaveChangesAsync();
            return existingService;
        }

        public async Task<bool> RemoveEmployeeFromService(int employeeServiceId)
        {
            var employeeService = await _context.SalaryServices.FindAsync(employeeServiceId);
            if (employeeService == null) return false;

            _ = _context.SalaryServices.Remove(employeeService);
            _ = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Servicee>> GetServicesByEmployee(string employeeId)
        {
            return await _context.SalaryServices
                .Where(s => s.AppUserId == employeeId)
                .Select(s => s.Servicee)
                .ToListAsync();
        }

        public async Task<List<AppUser>> GetEmployeesByService(int serviceId)
        {
            return await _context.SalaryServices
                .Where(s => s.ServiceeId == serviceId)
                .Select(s => s.AppUser)
                .ToListAsync();
        }

        public async Task<SalaryService> GetEmployeeServiceById(int employeeServiceId)
        {
            return await _context.SalaryServices.FindAsync(employeeServiceId);
        }

    }
}
