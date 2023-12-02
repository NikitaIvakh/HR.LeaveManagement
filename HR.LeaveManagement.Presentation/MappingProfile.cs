using AutoMapper;
using HR.LeaveManagement.Presentation.Models;
using HR.LeaveManagement.Presentation.Services.Base;

namespace HR.LeaveManagement.Presentation
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeViewModel>().ReverseMap();
            CreateMap<LeaveTypeDto, LeaveTypeViewModel>().ReverseMap();
        }
    }
}