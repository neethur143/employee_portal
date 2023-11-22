using System;
using System.Collections.Generic;

namespace employee_portal.Models;

public partial class TbDepartment
{
    public int Id { get; set; }

    public int DepartmentId { get; set; }

    public string Departmentname { get; set; } = null!;

    public int? ManagerId { get; set; }
}
