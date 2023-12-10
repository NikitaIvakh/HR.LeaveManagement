using AutoMapper;
using HR.LeaveManagement.Presentation.Models;
using HR.LeaveManagement.Presentation.Models.LeaveTypes;
using HR.LeaveManagement.Presentation.Services.Base;

namespace HR.LeaveManagement.Presentation
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeViewModel>().ReverseMap();
            CreateMap<UpdateLeaveTypesDto, UpdateLeaveTypeViewModel>().ReverseMap();
            CreateMap<DeleteLeaveTypeDto, DeleteLeaveTypeViewModel>().ReverseMap();
            CreateMap<CreateLeaveRequestDto, CreateRequestViewModel>().ReverseMap();
            CreateMap<LeaveRequestDto, LeaveRequestViewModel>()
                .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequest.DateTime))
                .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
                .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
                .ReverseMap();
            CreateMap<LeaveRequestListDto, LeaveRequestViewModel>().ReverseMap();
            CreateMap<LeaveTypeDto, LeaveTypeViewModel>().ReverseMap();
            CreateMap<RegisterViewModel, RegistrationRequest>().ReverseMap();
        }
    }
}