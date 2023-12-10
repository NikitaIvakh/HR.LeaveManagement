using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllLocation;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region LeaveRequest Mappings
            CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestListDto>().ForMember(dest => dest.DateRequest, opt => opt.MapFrom(src => src.DateCreated)).ReverseMap();
            CreateMap<LeaveRequest, CreateLeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest, UpdateLeaveRequestDto>().ReverseMap();
            #endregion LeaveRequest

            CreateMap<LeaveAllLocation, LeaveAllLocationDto>().ReverseMap();
            CreateMap<LeaveAllLocation, CreateLeaveAllLocationDto>().ReverseMap();
            CreateMap<LeaveAllLocation, UpdateLeaveAllLocationDto>().ReverseMap();

            CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
            CreateMap<LeaveType, CreateLeaveTypeDto>().ReverseMap();
            CreateMap<LeaveType, UpdateLeaveTypesDto>().ReverseMap();
            CreateMap<LeaveType, DeleteLeaveTypeDto>().ReverseMap();
        }
    }
}