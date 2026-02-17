using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Datamodels.Hrms;

public partial class Hrms_dbContext : DbContext
{
    public Hrms_dbContext()
    {
    }

    public Hrms_dbContext(DbContextOptions<Hrms_dbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeDocument> EmployeeDocuments { get; set; }

    public virtual DbSet<EmployeeSession> EmployeeSessions { get; set; }

    public virtual DbSet<EmployeeTitle> EmployeeTitles { get; set; }

    public virtual DbSet<JobPosition> JobPositions { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Management> Managements { get; set; }

    public virtual DbSet<ManagementPosition> ManagementPositions { get; set; }

    public virtual DbSet<Mission> Missions { get; set; }

    public virtual DbSet<Studentlog> Studentlogs { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<VEmployeeDetail> VEmployeeDetails { get; set; }

    public virtual DbSet<VManagementDetail> VManagementDetails { get; set; }

    public virtual DbSet<WorkUnit> WorkUnits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=HRMS;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("department_pkey");

            entity.ToTable("department", "person");

            entity.Property(e => e.DeptId)
                .HasMaxLength(20)
                .HasColumnName("dept_id");
            entity.Property(e => e.DeptDesc)
                .HasMaxLength(200)
                .HasColumnName("dept_desc");
            entity.Property(e => e.DeptNameEng)
                .HasMaxLength(200)
                .HasColumnName("dept_name_eng");
            entity.Property(e => e.DeptNameThai)
                .HasMaxLength(200)
                .HasColumnName("dept_name_thai");
            entity.Property(e => e.DivisionId)
                .HasMaxLength(20)
                .HasColumnName("division_id");
            entity.Property(e => e.Isactive)
                .HasMaxLength(10)
                .HasColumnName("isactive");
            entity.Property(e => e.LocationId)
                .HasMaxLength(20)
                .HasColumnName("location_id");
            entity.Property(e => e.MissionId)
                .HasMaxLength(10)
                .HasColumnName("mission_id");
        });

        modelBuilder.Entity<Division>(entity =>
        {
            entity.HasKey(e => e.DivisionId).HasName("division_pkey");

            entity.ToTable("division", "person");

            entity.Property(e => e.DivisionId)
                .HasMaxLength(20)
                .HasColumnName("division_id");
            entity.Property(e => e.CentralId)
                .HasMaxLength(10)
                .HasColumnName("central_id");
            entity.Property(e => e.DivisionDesc)
                .HasMaxLength(60)
                .HasColumnName("division_desc");
            entity.Property(e => e.DivisionNameEng)
                .HasMaxLength(200)
                .HasColumnName("division_name_eng");
            entity.Property(e => e.DivisionNameThai)
                .HasMaxLength(200)
                .HasColumnName("division_name_thai");
            entity.Property(e => e.Isactive)
                .HasMaxLength(10)
                .HasColumnName("isactive");
            entity.Property(e => e.LocationId)
                .HasMaxLength(20)
                .HasColumnName("location_id");
            entity.Property(e => e.MissionId)
                .HasMaxLength(10)
                .HasColumnName("mission_id");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pkey");

            entity.ToTable("employee", "person");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(200)
                .HasColumnName("employee_id");
            entity.Property(e => e.BankAccountNo)
                .HasMaxLength(50)
                .HasColumnName("bank_account_no");
            entity.Property(e => e.BankName)
                .HasMaxLength(100)
                .HasColumnName("bank_name");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CitizenId)
                .HasMaxLength(13)
                .HasColumnName("citizen_id");
            entity.Property(e => e.CurrentAddress).HasColumnName("current_address");
            entity.Property(e => e.DeptId)
                .HasMaxLength(20)
                .HasColumnName("dept_id");
            entity.Property(e => e.DivisionId)
                .HasMaxLength(20)
                .HasColumnName("division_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.EmergencyContactName)
                .HasMaxLength(200)
                .HasColumnName("emergency_contact_name");
            entity.Property(e => e.EmergencyContactPhone)
                .HasMaxLength(50)
                .HasColumnName("emergency_contact_phone");
            entity.Property(e => e.EmergencyContactRelation)
                .HasMaxLength(50)
                .HasColumnName("emergency_contact_relation");
            entity.Property(e => e.EmploymentStatus)
                .HasMaxLength(50)
                .HasColumnName("employment_status");
            entity.Property(e => e.EndDate)
                .HasMaxLength(20)
                .HasColumnName("end_date");
            entity.Property(e => e.FirstNameEng)
                .HasMaxLength(200)
                .HasColumnName("first_name_eng");
            entity.Property(e => e.FirstNameThai)
                .HasMaxLength(200)
                .HasColumnName("first_name_thai");
            entity.Property(e => e.FullNameEng)
                .HasMaxLength(401)
                .HasColumnName("full_name_eng");
            entity.Property(e => e.FullNameThai)
                .HasMaxLength(401)
                .HasColumnName("full_name_thai");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.LastNameEng)
                .HasMaxLength(200)
                .HasColumnName("last_name_eng");
            entity.Property(e => e.LastNameThai)
                .HasMaxLength(200)
                .HasColumnName("last_name_thai");
            entity.Property(e => e.LocationId)
                .HasMaxLength(20)
                .HasColumnName("location_id");
            entity.Property(e => e.ManagerId)
                .HasMaxLength(200)
                .HasColumnName("manager_id");
            entity.Property(e => e.MilitaryStatus)
                .HasMaxLength(20)
                .HasColumnName("military_status");
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .HasColumnName("nationality");
            entity.Property(e => e.Password)
                .HasMaxLength(400)
                .HasColumnName("password");
            entity.Property(e => e.PermanentAddress).HasColumnName("permanent_address");
            entity.Property(e => e.PictureUrl).HasColumnName("picture_url");
            entity.Property(e => e.Pincode)
                .HasMaxLength(400)
                .HasColumnName("pincode");
            entity.Property(e => e.PositionId)
                .HasMaxLength(20)
                .HasColumnName("position_id");
            entity.Property(e => e.Religion)
                .HasMaxLength(50)
                .HasColumnName("religion");
            entity.Property(e => e.SecretCode)
                .HasPrecision(6)
                .HasColumnName("secret_code");
            entity.Property(e => e.SocialSecurityNo)
                .HasMaxLength(20)
                .HasColumnName("social_security_no");
            entity.Property(e => e.StartDate)
                .HasMaxLength(20)
                .HasColumnName("start_date");
            entity.Property(e => e.TaxId)
                .HasMaxLength(20)
                .HasColumnName("tax_id");
            entity.Property(e => e.TeamId)
                .HasMaxLength(50)
                .HasColumnName("team_id");
            entity.Property(e => e.TerminationDate)
                .HasMaxLength(20)
                .HasColumnName("termination_date");
            entity.Property(e => e.TitleId)
                .HasMaxLength(50)
                .HasColumnName("title_id");
            entity.Property(e => e.UnitId)
                .HasMaxLength(20)
                .HasColumnName("unit_id");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        modelBuilder.Entity<EmployeeDocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("employee_document_pkey");

            entity.ToTable("employee_document", "person");

            entity.Property(e => e.DocumentId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("document_id");
            entity.Property(e => e.DocumentType)
                .HasMaxLength(50)
                .HasColumnName("document_type");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(200)
                .HasColumnName("employee_id");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .HasColumnName("file_name");
            entity.Property(e => e.FileUrl).HasColumnName("file_url");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("uploaded_at");
        });

        modelBuilder.Entity<EmployeeSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employee_session_pkey");

            entity.ToTable("employee_session", "person");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.DateCreated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateExpired)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_expired");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(200)
                .HasColumnName("employee_id");
            entity.Property(e => e.Ip).HasColumnName("ip");
        });

        modelBuilder.Entity<EmployeeTitle>(entity =>
        {
            entity.HasKey(e => e.TitleId).HasName("employee_title_pkey");

            entity.ToTable("employee_title", "person");

            entity.Property(e => e.TitleId)
                .HasMaxLength(2)
                .HasColumnName("title_id");
            entity.Property(e => e.TitleNameEng)
                .HasMaxLength(200)
                .HasColumnName("title_name_eng");
            entity.Property(e => e.TitleNameThai)
                .HasMaxLength(200)
                .HasColumnName("title_name_thai");
            entity.Property(e => e.TitleShortEng)
                .HasMaxLength(200)
                .HasColumnName("title_short_eng");
            entity.Property(e => e.TitleShortThai)
                .HasMaxLength(200)
                .HasColumnName("title_short_thai");
        });

        modelBuilder.Entity<JobPosition>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("job_position_pkey");

            entity.ToTable("job_position", "person");

            entity.Property(e => e.PositionId)
                .HasMaxLength(20)
                .HasColumnName("position_id");
            entity.Property(e => e.PositionNameEng)
                .HasMaxLength(400)
                .HasColumnName("position_name_eng");
            entity.Property(e => e.PositionNameThai)
                .HasMaxLength(400)
                .HasColumnName("position_name_thai");
            entity.Property(e => e.PositionType)
                .HasMaxLength(2)
                .HasColumnName("position_type");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("location_pkey");

            entity.ToTable("location", "person");

            entity.Property(e => e.LocationId)
                .HasMaxLength(20)
                .HasColumnName("location_id");
            entity.Property(e => e.LocationAddress)
                .HasMaxLength(2000)
                .HasColumnName("location_address");
            entity.Property(e => e.LocationDesc)
                .HasMaxLength(2000)
                .HasColumnName("location_desc");
            entity.Property(e => e.LocationNameEng)
                .HasMaxLength(200)
                .HasColumnName("location_name_eng");
            entity.Property(e => e.LocationNameThai)
                .HasMaxLength(200)
                .HasColumnName("location_name_thai");
        });

        modelBuilder.Entity<Management>(entity =>
        {
            entity.HasKey(e => e.ManagementId).HasName("management_pkey");

            entity.ToTable("management", "person");

            entity.Property(e => e.ManagementId)
                .HasMaxLength(400)
                .HasColumnName("management_id");
            entity.Property(e => e.DeptId)
                .HasMaxLength(50)
                .HasColumnName("dept_id");
            entity.Property(e => e.DivisionId)
                .HasMaxLength(50)
                .HasColumnName("division_id");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(200)
                .HasColumnName("employee_id");
            entity.Property(e => e.Isactive)
                .HasMaxLength(1)
                .HasColumnName("isactive");
            entity.Property(e => e.LocationId)
                .HasMaxLength(50)
                .HasColumnName("location_id");
            entity.Property(e => e.ManagementPositionId)
                .HasMaxLength(50)
                .HasColumnName("management_position_id");
            entity.Property(e => e.TeamId)
                .HasMaxLength(50)
                .HasColumnName("team_id");
            entity.Property(e => e.TempAdminCode)
                .HasMaxLength(20)
                .HasColumnName("temp_admin_code");
            entity.Property(e => e.UnitId)
                .HasMaxLength(50)
                .HasColumnName("unit_id");
        });

        modelBuilder.Entity<ManagementPosition>(entity =>
        {
            entity.HasKey(e => e.ManagementPositionId).HasName("management_position_pkey");

            entity.ToTable("management_position", "person");

            entity.Property(e => e.ManagementPositionId)
                .HasMaxLength(2)
                .HasColumnName("management_position_id");
            entity.Property(e => e.PositionLevel)
                .HasDefaultValue(1)
                .HasColumnName("position_level");
            entity.Property(e => e.PositionNameEng)
                .HasMaxLength(400)
                .HasColumnName("position_name_eng");
            entity.Property(e => e.PositionNameThai)
                .HasMaxLength(400)
                .HasColumnName("position_name_thai");
        });

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.HasKey(e => e.MissionId).HasName("mission_pkey");

            entity.ToTable("mission", "person");

            entity.Property(e => e.MissionId)
                .HasMaxLength(10)
                .HasColumnName("mission_id");
            entity.Property(e => e.MissionNameEng)
                .HasMaxLength(200)
                .HasColumnName("mission_name_eng");
            entity.Property(e => e.MissionNameThai)
                .HasMaxLength(200)
                .HasColumnName("mission_name_thai");
        });

        modelBuilder.Entity<Studentlog>(entity =>
        {
            entity.HasKey(e => e.Logid).HasName("studentlog_pkey");

            entity.ToTable("studentlog", "person");

            entity.Property(e => e.Logid).HasColumnName("logid");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.Actionat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("actionat");
            entity.Property(e => e.Actionby)
                .HasMaxLength(50)
                .HasColumnName("actionby");
            entity.Property(e => e.Studentid).HasColumnName("studentid");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("team_pkey");

            entity.ToTable("team", "person");

            entity.Property(e => e.TeamId)
                .HasMaxLength(3)
                .HasColumnName("team_id");
            entity.Property(e => e.DeptId)
                .HasMaxLength(3)
                .HasColumnName("dept_id");
            entity.Property(e => e.DivisionId)
                .HasMaxLength(2)
                .HasColumnName("division_id");
            entity.Property(e => e.Isactive)
                .HasMaxLength(1)
                .HasColumnName("isactive");
            entity.Property(e => e.LocationId)
                .HasMaxLength(2)
                .HasColumnName("location_id");
            entity.Property(e => e.MissionId)
                .HasMaxLength(10)
                .HasColumnName("mission_id");
            entity.Property(e => e.TeamNameEng)
                .HasMaxLength(200)
                .HasColumnName("team_name_eng");
            entity.Property(e => e.TeamNameThai)
                .HasMaxLength(200)
                .HasColumnName("team_name_thai");
        });

        modelBuilder.Entity<VEmployeeDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_employee_details", "person");

            entity.Property(e => e.CitizenId).HasMaxLength(13);
            entity.Property(e => e.DeptId).HasMaxLength(20);
            entity.Property(e => e.DeptNameEng).HasMaxLength(200);
            entity.Property(e => e.DeptNameThai).HasMaxLength(200);
            entity.Property(e => e.DivisionId).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EmployeeId).HasMaxLength(200);
            entity.Property(e => e.EmploymentStatus).HasMaxLength(50);
            entity.Property(e => e.EndDate).HasMaxLength(20);
            entity.Property(e => e.FirstNameEng).HasMaxLength(200);
            entity.Property(e => e.FirstNameThai).HasMaxLength(200);
            entity.Property(e => e.FullNameEng).HasMaxLength(401);
            entity.Property(e => e.FullNameThai).HasMaxLength(401);
            entity.Property(e => e.LastNameEng).HasMaxLength(200);
            entity.Property(e => e.LastNameThai).HasMaxLength(200);
            entity.Property(e => e.LocationId).HasMaxLength(20);
            entity.Property(e => e.PositionId).HasMaxLength(20);
            entity.Property(e => e.StartDate).HasMaxLength(20);
            entity.Property(e => e.TeamId).HasMaxLength(50);
            entity.Property(e => e.TerminationDate).HasMaxLength(20);
            entity.Property(e => e.TitleId).HasMaxLength(50);
            entity.Property(e => e.UnitId).HasMaxLength(20);
        });

        modelBuilder.Entity<VManagementDetail>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("v_management_details", "person");

            // ระบุชื่อ Column ให้ตรงกับใน SQL View เป๊ะๆ
            entity.Property(e => e.Key).HasColumnName("Key");
            entity.Property(e => e.StaffId).HasColumnName("StaffId");
            entity.Property(e => e.StaffNameThai).HasColumnName("StaffNameThai");
            entity.Property(e => e.AdminNameThai).HasColumnName("AdminNameThai");
            entity.Property(e => e.DivisionFull).HasColumnName("DivisionFull");
            entity.Property(e => e.Isactive).HasColumnName("Isactive");
        });

        modelBuilder.Entity<WorkUnit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("work_unit_pkey");

            entity.ToTable("work_unit", "person");

            entity.Property(e => e.UnitId)
                .HasMaxLength(20)
                .HasColumnName("unit_id");
            entity.Property(e => e.DeptId)
                .HasMaxLength(20)
                .HasColumnName("dept_id");
            entity.Property(e => e.DivisionId)
                .HasMaxLength(20)
                .HasColumnName("division_id");
            entity.Property(e => e.Isactive)
                .HasMaxLength(10)
                .HasColumnName("isactive");
            entity.Property(e => e.LocationId)
                .HasMaxLength(2)
                .HasColumnName("location_id");
            entity.Property(e => e.MissionId)
                .HasMaxLength(10)
                .HasColumnName("mission_id");
            entity.Property(e => e.TeamId)
                .HasMaxLength(3)
                .HasColumnName("team_id");
            entity.Property(e => e.UnitNameEng)
                .HasMaxLength(70)
                .HasColumnName("unit_name_eng");
            entity.Property(e => e.UnitNameThai)
                .HasMaxLength(70)
                .HasColumnName("unit_name_thai");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
