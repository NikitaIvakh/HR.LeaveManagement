﻿using AutoMapper;
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
                .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequest.Date))
                .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.Date))
                .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.Date)).ReverseMap();
            CreateMap<LeaveRequestListDto, LeaveRequestViewModel>()
                .ForMember(key => key.DateRequested, opt => opt.MapFrom(key => key.DateRequest.Date))
                .ForMember(key => key.StartDate, opt => opt.MapFrom(key => key.StartDate.Date))
                .ForMember(key => key.EndDate, opt => opt.MapFrom(key => key.EndDate.Date)).ReverseMap();
            #endregion

            #region LeaveAllLocationDto Mapping
            CreateMap<LeaveAllLocationDto, LeaveAllocationViewModel>().ReverseMap();
            CreateMap<CreateLeaveAllLocationDto, CreateLeaveAllocationViewModel>().ReverseMap();
            CreateMap<UpdateLeaveAllLocationDto, UpdateLeaveAllocationViewModel>().ReverseMap();
            CreateMap<LeaveAllLocationDto, ViewLeaveAllocationsViewModel>().ReverseMap();
            #endregion

            CreateMap<RegisterViewModel, RegistrationRequest>().ReverseMap();
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}