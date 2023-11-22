using System;
using System.Collections.Generic;

namespace employee_portal.Models;

public partial class TbEmployeeDepart
{
    public int Id { get; set; }

    public int DepartmentId { get; set; }

    public int? EmployeeId { get; set; }
}
