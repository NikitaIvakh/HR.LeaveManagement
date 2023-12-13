using AutoMapper;
using HR.LeaveManagement.Presentation.Models.Accounts;
using HR.LeaveManagement.Presentation.Models.LeaveAllLocations;
using HR.LeaveManagement.Presentation.Models.LeaveRequests;
using HR.LeaveManagement.Presentation.Models.LeaveTypes;
using HR.LeaveManagement.Presentation.Services.Base;

namespace HR.LeaveManagement.Presentation
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region LeaveTypeDto Mapping
            CreateMap<LeaveTypeDto, LeaveTypeViewModel>().ReverseMap();
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeViewModel>().ReverseMap();
            CreateMap<UpdateLeaveTypesDto, UpdateLeaveTypeViewModel>().ReverseMap();
            CreateMap<DeleteLeaveTypeDto, DeleteLeaveTypeViewModel>().ReverseMap();
            #endregion

            #region LeaveRequestDto Mapping
            CreateMap<CreateLeaveRequestDto, CreateRequestViewModel>().ReverseMap();
            CreateMap<LeaveRequestDto, LeaveRequestViewModel>()
                .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequest.DateTime))
                .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
                .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime)).ReverseMap();
            CreateMap<LeaveRequestListDto, LeaveRequestViewModel>()
                .ForMember(key => key.DateRequested, opt => opt.MapFrom(key => key.DateRequest.DateTime))
                .ForMember(key => key.StartDate, opt => opt.MapFrom(key => key.StartDate.DateTime))
                .ForMember(key => key.EndDate, opt => opt.MapFrom(key => key.EndDate.DateTime)).ReverseMap();
            #endregion

            #region LeaveAllLocationDto Mapping
            CreateMap<LeaveAllLocationDto, LeaveAllocationViewModel>().ReverseMap();
            CreateMap<CreateLeaveAllLocationDto, CreateLeaveTypeViewModel>().ReverseMap();
            CreateMap<UpdateLeaveTypesDto, UpdateLeaveTypeViewModel>().ReverseMap();
            CreateMap<DeleteLeaveTypeDto, DeleteLeaveTypeViewModel>().ReverseMap();
            #endregion

            CreateMap<RegisterViewModel, RegistrationRequest>().ReverseMap();
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}