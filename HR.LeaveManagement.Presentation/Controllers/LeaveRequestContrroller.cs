using HR.LeaveManagement.Presentation.Contracts;
using HR.LeaveManagement.Presentation.Models;
using HR.LeaveManagement.Presentation.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR.LeaveManagement.Presentation.Controllers
{
    public class LeaveRequestContrroller : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestContrroller(ILeaveTypeService leaveTypeService, ILeaveRequestService leaveRequestService)
        {
            _leaveTypeService = leaveTypeService;
            _leaveRequestService = leaveRequestService;
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
                return RedirectToAction(nameof(Index));
            }

            List<LeaveTypeViewModel> leaveTypes = await _leaveTypeService.GetLeaveTypesAsync();
            SelectList leaveTypeItems = new(leaveTypes, "Id", "Name");
            createRequestViewModel.LeaveTypes = leaveTypeItems;

            return View(createRequestViewModel);
        }
    }
}