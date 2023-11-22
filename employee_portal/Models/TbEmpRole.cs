using System;
using System.Collections.Generic;

namespace employee_portal.Models;

public partial class TbEmpRole
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int? EmployeeId { get; set; }
}
