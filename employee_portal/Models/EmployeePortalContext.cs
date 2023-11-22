using Microsoft.EntityFrameworkCore;

namespace employee_portal.Models;

public partial class EmployeePortalContext : DbContext
{
    public EmployeePortalContext()
    {
    }

    public EmployeePortalContext(DbContextOptions<EmployeePortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbDepartment> TbDepartments { get; set; }

    public virtual DbSet<TbEmpRole> TbEmpRoles { get; set; }

    public virtual DbSet<TbEmployee> TbEmployees { get; set; }

    public virtual DbSet<TbEmployeeDepart> TbEmployeeDeparts { get; set; }

    public virtual DbSet<TbJobTitle> TbJobTitles { get; set; }

    public virtual DbSet<TbLogin> TbLogins { get; set; }

    public virtual DbSet<TbRole> TbRoles { get; set; }

    public virtual DbSet<TbSalaryIncrement> TbSalaryIncrements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=Employee_Portal;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbDepartment>(entity =>
        {
            entity.HasKey(e => e.DepartmentId);

            entity.ToTable("tb_department");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnName("department_id");
            entity.Property(e => e.Departmentname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("departmentname");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
        });

        modelBuilder.Entity<TbEmpRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_emp_role");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
        });

        modelBuilder.Entity<TbEmployee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK_tb_employee_1");

            entity.ToTable("tb_employee");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("employee_id");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.DateOfJoin)
                .HasColumnType("date")
                .HasColumnName("date_of_join");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("employee_code");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstname");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastname");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Salary)
                .HasColumnType("money")
                .HasColumnName("salary");
        });

        modelBuilder.Entity<TbEmployeeDepart>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_employee_depart");

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
        });

        modelBuilder.Entity<TbJobTitle>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK_Employee Experience");

            entity.ToTable("tb_job_title");

            entity.Property(e => e.JobId)
                .ValueGeneratedNever()
                .HasColumnName("job_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.TitleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title_name");
        });

        modelBuilder.Entity<TbLogin>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_login");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<TbRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Roles");

            entity.ToTable("tb_roles");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("role_ID");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<TbSalaryIncrement>(entity =>
        {
            entity.HasKey(e => e.IncrementId).HasName("PK_Salary increment");

            entity.ToTable("tb_salary_increment");

            entity.Property(e => e.IncrementId)
                //.ValueGeneratedNever()
                .ValueGeneratedOnAdd()
                .HasColumnName("increment_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            //entity.Property(e => e.Id)
                //.ValueGeneratedOnAdd()
                //.HasColumnName("id");
            entity.Property(e => e.IncrementDate)
                .HasColumnType("date")
                .HasColumnName("incrementDate");
            entity.Property(e => e.Increpersent).HasColumnName("increpersent");
            entity.Property(e => e.NewSalary)
                .HasColumnType("money")
                .HasColumnName("newSalary");
            entity.Property(e => e.OldSalary)
                .HasColumnType("money")
                .HasColumnName("oldSalary");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
