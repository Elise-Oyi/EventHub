using AutoMapper;
using EventHub.DTO;
using EventHub.Models;

namespace EventHub.Profiles
{
    public class EmployeesProfile:Profile
    {
        public EmployeesProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<NewEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
        }
    }
}
