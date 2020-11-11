using AutoMapper;
using EmployeeManager.ServerApp.Models;
using EmployeeManager.ServerApp.Models.BindingTargets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.ServerApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeData>()
                .ForMember(x => x.HiringDate, opt => opt.MapFrom(d => d.HiringDate.ToString("yyyy-MM-dd")))
                .ForMember(x => x.TerminationDate, opt => opt.MapFrom(d => d.TerminationDate.HasValue ?
                d.TerminationDate.Value.ToString("yyyy-MM-dd") : string.Empty));
            
                CreateMap<EmployeeData, Employee>()
                .ForMember(x => x.HiringDate, opt => opt.MapFrom(d => DateTime.ParseExact(d.HiringDate, "yyyy-MM-dd", null)))
                .ForMember(x => x.TerminationDate, opt => opt.MapFrom(d => !string.IsNullOrWhiteSpace(d.TerminationDate) ? 
                (DateTime?)DateTime.ParseExact(d.TerminationDate, "yyyy-MM-dd", null) : null));

            CreateMap<Position, PositionData>();
            CreateMap<PositionData, Position>();
        }
    }
}
