using System;
using System.Collections.Generic;

namespace employee_portal.Models;

public partial class TbEmployee
{
    internal  bool SalaryIncremented;

    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string? EmployeeCode { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public decimal? Salary { get; set; }

    public int? JobId { get; set; }

    public int? DepartmentId { get; set; }

    public DateTime DateOfBirth { get; set; }

    public DateTime DateOfJoin { get; set; }

}
