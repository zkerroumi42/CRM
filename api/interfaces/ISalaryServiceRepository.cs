using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.models;

namespace api.interfaces
{
    public interface ISalaryServiceRepository
    {
        Task<SalaryService> AssignEmployeeToService(SalaryService employeeService);
        Task<SalaryService> UpdateEmployeeService(int employeeServiceId, SalaryService employeeService);
        Task<bool> RemoveEmployeeFromService(int employeeServiceId);
        Task<List<Servicee>> GetServicesByEmployee(string employeeId);
        Task<List<AppUser>> GetEmployeesByService(int serviceId);
        Task<SalaryService> GetEmployeeServiceById(int employeeServiceId);
        
    }
}