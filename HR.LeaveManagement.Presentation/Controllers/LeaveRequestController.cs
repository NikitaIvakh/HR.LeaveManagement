using AutoMapper;
using HR.LeaveManagement.Presentation.Contracts;
using HR.LeaveManagement.Presentation.Models.LeaveRequests;
using HR.LeaveManagement.Presentation.Models.LeaveTypes;
using HR.LeaveManagement.Presentation.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR.LeaveManagement.Presentation.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IMapper _mapper;

        public LeaveRequestController(ILeaveTypeService leaveTypeService, ILeaveRequestService leaveRequestService, IMapper mapper)
        {
            _leaveTypeService = leaveTypeService;
            _leaveRequestService = leaveRequestService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Index()
        {
            AdminLeaveRequestViewVM adminLeaveRequestViewVM = await _leaveRequestService.GetAdminLeaveRequestViewViewModelAsync();
            return View(adminLeaveRequestViewVM);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            LeaveRequestViewModel leaveRequest = await _leaveRequestService.GetLeaveRequestAsync(id);
            return View(leaveRequest);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            List<LeaveTypeViewModel> leaveTypes = await _leaveTypeService.GetLeaveTypesAsync();
            SelectList leaveTypeItems = new(leaveTypes, "Id", "Name");
            CreateRequestViewModel model = new()
            {
                LeaveTypes = leaveTypeItems
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateRequestViewModel createRequestViewModel)
        {
            if (ModelState.IsValid)
            {
                BaseResponse<int> response = await _leaveRequestService.CreateLeaveRequestAsync(createRequestViewModel);
                if (response.Status)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, response.ValidationErrors);
            }

            List<LeaveTypeViewModel> leaveTypes = await _leaveTypeService.GetLeaveTypesAsync();
            SelectList leaveTypeItems = new(leaveTypes, "Id", "Name");
            createRequestViewModel.LeaveTypes = leaveTypeItems;

            return View(createRequestViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> ApproveRequest(int id, bool approved)
        {
            try
            {
                await _leaveRequestService.ApproveLeaveRequestAsync(id, approved);
                return RedirectToAction(nameof(Index));
            }

            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}