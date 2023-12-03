using HR.LeaveManagement.Presentation.Contracts;
using HR.LeaveManagement.Presentation.Models;
using HR.LeaveManagement.Presentation.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Presentation.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypesController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

        // GET: LeaveTypesController
        public async Task<ActionResult> Index()
        {
            var leaveTypes = await _leaveTypeService.GetLeaveTypesAsync();
            return View(leaveTypes);
        }

        // GET: LeaveTypesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            LeaveTypeViewModel leaveType = await _leaveTypeService.GetLeaveTypeAsync(id);
            return View(leaveType);
        }

        // GET: LeaveTypesController/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: LeaveTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveTypeViewModel createLeaveTypeViewModel)
        {
            try
            {
                BaseResponse<int> response = await _leaveTypeService.CreateLeaveTypeAsync(createLeaveTypeViewModel);
                if (response.Status is true)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, response.ValidationErrors);
            }

            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return View(createLeaveTypeViewModel);
        }

        // GET: LeaveTypesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            LeaveTypeViewModel leaveType = await _leaveTypeService.GetLeaveTypeAsync(id);
            return View(leaveType);
        }

        // POST: LeaveTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LeaveTypeViewModel leaveTypeViewModel)
        {
            try
            {
                BaseResponse<int> response = await _leaveTypeService.UpdateLeaveTypeAsync(id, leaveTypeViewModel);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return View(leaveTypeViewModel);
        }

        // POST: LeaveTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                BaseResponse<int> response = await _leaveTypeService.DeleteLeaveTypeAsync(id);
                if (response.Status is true)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, response.ValidationErrors);
            }

            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return View();
        }
    }
}