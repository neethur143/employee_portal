using System;
using System.Collections.Generic;

namespace employee_portal.Models;

public partial class TbJobTitle
{
    public int Id { get; set; }

    public int JobId { get; set; }

    public int? DepartmentId { get; set; }

    public string? TitleName { get; set; }
}
