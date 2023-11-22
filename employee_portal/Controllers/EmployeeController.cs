using employee_portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Mvc.Core;



namespace employee_portal.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeePortalContext _context;

        public EmployeeController(EmployeePortalContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1); // If page is null, default to page 1
            int pageSize = 4; // Set your desired page size

            var employees = _context.TbEmployees
                /*.Include(e => e.Department)*/ // Eager load the Department
                .ToPagedList(pageNumber, pageSize);

            return View(employees);
        }

        public IActionResult Search(string searchString, int? page)
        {
            int pageNumber = (page ?? 1); 
            int pageSize = 4; 

            var employees = _context.TbEmployees.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                employees = employees.Where(e =>
                    e.Firstname.ToLower().Contains(searchString) ||
                    e.EmployeeCode.ToLower().Contains(searchString)
                );
            }

            if (employees.Count() == 0)
            {
                ViewBag.Message = "No records found.";
            }

            var pagedEmployees = employees/*.Include(e => e.Department)*/
                .ToPagedList(pageNumber, pageSize);

            return View("Index", pagedEmployees);
        }
        public JsonResult GetJobTitles(int departmentId)
        {
            var jobTitles = _context.TbJobTitles
                .Where(j => j.DepartmentId == departmentId)
                .Select(j => new { value = j.JobId, text = j.TitleName })
                .ToList();

            return Json(jobTitles);
        }
        [HttpGet]
        public IActionResult AddEmployee()
        {
            var viewModel = new EmployeeViewModel
            {
                Departments = _context.TbDepartments.ToList(),
                JobTitles = _context.TbJobTitles.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult AddEmployee(EmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var existingEmployee = _context.TbEmployees.FirstOrDefault(e => e.EmployeeId == viewModel.Employee.EmployeeId ||
                                                                           e.EmployeeCode == viewModel.Employee.EmployeeCode ||
                                                                           e.Email == viewModel.Employee.Email);
                if (existingEmployee != null)
                {
                    if (existingEmployee.EmployeeId == viewModel.Employee.EmployeeId)
                    {
                        ModelState.AddModelError("Employee.EmployeeId", "An employee with the same Employee ID already exists.");
                    }
                    if (existingEmployee.EmployeeCode == viewModel.Employee.EmployeeCode)
                    {
                        ModelState.AddModelError("Employee.EmployeeCode", "An employee with the same Employee Code already exists.");
                    }
                    if (existingEmployee.Email == viewModel.Employee.Email)
                    {
                        ModelState.AddModelError("Employee.Email", "An employee with the same Email already exists.");
                    }

                    viewModel.Departments = _context.TbDepartments.ToList();
                    viewModel.JobTitles = _context.TbJobTitles.ToList();

                    return View(viewModel);
                }

                // Save the employee details to the database
                _context.TbEmployees.Add(viewModel.Employee);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Employee added successfully";
                return RedirectToAction("Index");
            }
            viewModel.Departments = _context.TbDepartments.ToList();
            viewModel.JobTitles = _context.TbJobTitles.ToList();

            return View(viewModel);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.TbEmployees.FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.TbEmployees
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.TbEmployees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.TbEmployees.Remove(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult IncrementSalary(int id)
        {
            var employee = _context.TbEmployees.FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound(); // Handle the case where the employee is not found
            }

            return View("IncrementSalary", employee);
        }

        [HttpPost]
        public IActionResult IncrementSalary(int id, decimal incrementPercentage)
        {
            var employee = _context.TbEmployees.FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound(); // Handle the case where the employee is not found
            }

            decimal oldSalary = (decimal)employee.Salary;

            // Update the salary based on manual increment
            employee.Salary = CalculateNewSalary(oldSalary, incrementPercentage);

            // Create a salary increment history record
            CreateSalaryIncrementRecord(id, oldSalary, (decimal)employee.Salary, incrementPercentage);

            // Save the updated salary back to the database
            _context.SaveChanges();

            // Set ViewData for displaying the updated salary information
            ViewData["Incremented"] = true;
            ViewData["IncrementedPercentage"] = incrementPercentage;
            ViewData["NewSalary"] = employee.Salary;

            // Return to the IncrementSalary view with updated data
            return View("IncrementSalary", employee);
        }

        private void CreateSalaryIncrementRecord(int employeeId, decimal oldSalary, decimal newSalary, decimal incrementPercentage)
        {
            // Create a salary increment history record
            var incrementRecord = new TbSalaryIncrement
            {
                EmployeeId = employeeId,
                IncrementDate = DateTime.Now,
                OldSalary = oldSalary,
                NewSalary = newSalary,
                Increpersent = (int)incrementPercentage
            };

            _context.TbSalaryIncrements.Add(incrementRecord);
        }

        private decimal CalculateNewSalary(decimal oldSalary, decimal incrementPercentage)
        {
            return oldSalary * (1 + incrementPercentage / 100); // Convert the percentage to a decimal value
        }

        public IActionResult SalaryHistory(int id)
        {
            var salaryHistory = _context.TbSalaryIncrements.Where(i => i.EmployeeId == id).ToList();
           // var salaryHistory = _context.TbSalaryIncrements.Where(i => i.EmployeeId == id).GroupBy( i => i.Id).Select(x => x.First()).ToList();
            if (salaryHistory.Count == 0)
            {
                ViewData["NoRecords"] = "No salary history records found.";
                return View(salaryHistory);
            }

            return View(salaryHistory);
        }
        public IActionResult Edit(int id)
        {
            var employee = _context.TbEmployees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateEmployee(TbEmployee employee)
        {
            if (ModelState.IsValid)
            {
                // Find the existing employee in the database
                var existingEmployee = _context.TbEmployees
                    .Where(e => e.EmployeeId == employee.EmployeeId)
                    .FirstOrDefault();

                if (existingEmployee != null)
                {
                    existingEmployee.Firstname = employee.Firstname;
                    existingEmployee.Lastname = employee.Lastname;
                    existingEmployee.Email = employee.Email;
                    existingEmployee.Phonenumber = employee.Phonenumber;
                    existingEmployee.DateOfBirth = employee.DateOfBirth;
                    existingEmployee.DateOfJoin = employee.DateOfJoin;
                    existingEmployee.Salary = employee.Salary;

                    // Save the changes to the database
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Employee Updated successfully";

                    // Redirect to the "Edit" action with the updated employee
                    return RedirectToAction("Edit", new { id = existingEmployee.EmployeeId });
                }
            }

            return View("Edit", employee); // Pass the employee object with validation errors
        }



    }
}





