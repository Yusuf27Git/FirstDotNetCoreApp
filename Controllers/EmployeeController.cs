using DotNetCore5_CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore5_CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DotNetCore5_CRUDContext _dbContext;

        // Constructor
        public EmployeeController(DotNetCore5_CRUDContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: EmployeeController
        public async Task<IActionResult> Index()
        {
            var employees = await _dbContext.Employees.ToListAsync();
            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public async Task<IActionResult> Details(int? employeeId)
        {
            if (employeeId == null)
            {
                return NotFound();
            }
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(m => m.EmployeeId == employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // AddOrEdit Get Method

        public async Task<IActionResult> AddOrEdit(int? employeeId)
        {
            ViewBag.PageName = employeeId == null ? "Create Employee" : "Edit Employee";
            ViewBag.IsEdit = employeeId == null ? false : true;

            if(employeeId == null)
            {
                return View();
            }
            else
            {
                var objEmployee = await _dbContext.Employees.FindAsync(employeeId);
                if (objEmployee == null)
                    return NotFound();

                return View(objEmployee);
            }
        }

        // AddOrEdit Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int employeeId, [Bind("EmployeeId,Name,Designation,Address,Salary,JoiningDate")] Employee formEmployee)
        {
            bool isEmployeeExist = false;
            Employee employee = await _dbContext.Employees.FindAsync(employeeId);

            if(employee != null)
            {
                isEmployeeExist = true;
            }
            else
            {
                employee = new Employee();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    employee.Name = formEmployee.Name;
                    employee.Designation = formEmployee.Designation;
                    employee.Address = formEmployee.Address;
                    employee.Salary = formEmployee.Salary;
                    employee.JoiningDate = formEmployee.JoiningDate;

                    if (isEmployeeExist)
                    {
                        _dbContext.Update(employee);
                    }
                    else
                    {
                        _dbContext.Add(employee);
                    }

                    await _dbContext.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(formEmployee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? employeeId)
        {
            if (employeeId == null)
            {
                return NotFound();
            }
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(m => m.EmployeeId == employeeId);

            return View(employee);
        }

        // POST: Employees/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int employeeId)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId);
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
