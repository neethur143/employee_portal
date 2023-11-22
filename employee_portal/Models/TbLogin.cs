using System;
using System.Collections.Generic;

namespace employee_portal.Models;

public partial class TbLogin
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? EmployeeId { get; set; }
}
