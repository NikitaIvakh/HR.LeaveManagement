using HR.LeaveManagement.Presentation.Contracts;
using HR.LeaveManagement.Presentation.Models;
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

        public LeaveRequestController(ILeaveTypeService leaveTypeService, ILeaveRequestService leaveRequestService)
        {
            _leaveTypeService = leaveTypeService;
            _leaveRequestService = leaveRequestService;
        }

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
    }
}