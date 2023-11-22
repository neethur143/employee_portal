using System.Collections.Generic;

namespace employee_portal.Models
{
    public class EmployeeViewModel
    {
        public TbEmployee Employee { get; set; }
        public List<TbDepartment> Departments { get; set; }
        public List<TbJobTitle> JobTitles { get; set; }

        public EmployeeViewModel()
        {
            // Initialize properties or collections if needed.
            Employee = new TbEmployee();
            Departments = new List<TbDepartment>();
            JobTitles = new List<TbJobTitle>();
        }
    }
}



