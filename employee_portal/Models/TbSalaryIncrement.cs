using System;
using System.Collections.Generic;

namespace employee_portal.Models;

public partial class TbSalaryIncrement
{
    //public int Id { get; set; }

    public int IncrementId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? IncrementDate { get; set; }

    public decimal? OldSalary { get; set; }

    public decimal? NewSalary { get; set; }

    public int? Increpersent { get; set; }
}
