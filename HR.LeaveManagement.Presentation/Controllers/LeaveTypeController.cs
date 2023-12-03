using HR.LeaveManagement.Presentation.Contracts;
using HR.LeaveManagement.Presentation.Models;
using HR.LeaveManagement.Presentation.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Presentation.Controllers
{
    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypeController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

        // GET: LeaveTypeController
        public async Task<ActionResult> Index()
        {
            List<LeaveTypeViewModel> leaveTypes = await _leaveTypeService.GetLeaveTypes();
            return View(leaveTypes);
        }

        // GET: LeaveTypeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            LeaveTypeViewModel leaveType = await _leaveTypeService.GetLeaveType(id);
            return View(leaveType);
        }

        // GET: LeaveTypeController/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: LeaveTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveTypeViewModel createLeaveTypeViewModel)
        {
            try
            {
                BaseResponse<int> response = await _leaveTypeService.CreateLeaveType(createLeaveTypeViewModel);
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

        // GET: LeaveTypeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            LeaveTypeViewModel leaveType = await _leaveTypeService.GetLeaveType(id);
            return View(leaveType);
        }

        // POST: LeaveTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LeaveTypeViewModel leaveTypeViewModel)
        {
            try
            {
                BaseResponse<int> response = await _leaveTypeService.UpdateLeaveType(id, leaveTypeViewModel);
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

            return View(leaveTypeViewModel);
        }

        // POST: LeaveTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                BaseResponse<int> response = await _leaveTypeService.DeleteLeaveType(id);
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