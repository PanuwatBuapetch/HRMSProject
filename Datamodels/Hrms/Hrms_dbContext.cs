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

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

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

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<VEmployeeDetail> VEmployeeDetails { get; set; }

    public virtual DbSet<VManagementDetail> VManagementDetails { get; set; }

    public virtual DbSet<WorkUnit> WorkUnits { get; set; }

    public virtual DbSet<WorkflowAttachment> WorkflowAttachments { get; set; }

    public virtual DbSet<WorkflowComment> WorkflowComments { get; set; }

    public virtual DbSet<WorkflowDefinition> WorkflowDefinitions { get; set; }

    public virtual DbSet<WorkflowDocument> WorkflowDocuments { get; set; }

    public virtual DbSet<WorkflowInbox> WorkflowInboxes { get; set; }

    public virtual DbSet<WorkflowInstance> WorkflowInstances { get; set; }

    public virtual DbSet<WorkflowStep> WorkflowSteps { get; set; }

    public virtual DbSet<WorkflowTransaction> WorkflowTransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=HRMS;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Logid).HasName("studentlog_pkey");

            entity.ToTable("AuditLog", "person");

            entity.Property(e => e.Logid)
                .HasDefaultValueSql("nextval('person.studentlog_logid_seq'::regclass)")
                .HasColumnName("logid");
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
            entity
                .HasNoKey()
                .ToView("v_management_details", "person");

            entity.Property(e => e.AdminNameThai).HasMaxLength(400);
            entity.Property(e => e.DivisionFull).HasMaxLength(200);
            entity.Property(e => e.Isactive).HasMaxLength(1);
            entity.Property(e => e.Key).HasMaxLength(400);
            entity.Property(e => e.StaffId).HasMaxLength(200);
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

        modelBuilder.Entity<WorkflowAttachment>(entity =>
        {
            entity.HasKey(e => e.WorkflowAttachmentId).HasName("workflow_attachments_pkey");

            entity.ToTable("workflow_attachments", "person");

            entity.Property(e => e.WorkflowAttachmentId)
                .HasMaxLength(36)
                .HasColumnName("workflow_attachment_id");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .HasColumnName("file_name");
            entity.Property(e => e.FilePath).HasColumnName("file_path");
            entity.Property(e => e.FileSize).HasColumnName("file_size");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("uploaded_at");
            entity.Property(e => e.UploadedBy)
                .HasMaxLength(100)
                .HasColumnName("uploaded_by");
            entity.Property(e => e.WorkflowDocumentId)
                .HasMaxLength(36)
                .HasColumnName("workflow_document_id");

            entity.HasOne(d => d.WorkflowDocument).WithMany(p => p.WorkflowAttachments)
                .HasForeignKey(d => d.WorkflowDocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_attachment_document");
        });

        modelBuilder.Entity<WorkflowComment>(entity =>
        {
            entity.HasKey(e => e.WorkflowCommentId).HasName("workflow_comments_pkey");

            entity.ToTable("workflow_comments", "person");

            entity.Property(e => e.WorkflowCommentId)
                .HasMaxLength(36)
                .HasColumnName("workflow_comment_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(100)
                .HasColumnName("employee_id");
            entity.Property(e => e.WorkflowDocumentId)
                .HasMaxLength(36)
                .HasColumnName("workflow_document_id");

            entity.HasOne(d => d.WorkflowDocument).WithMany(p => p.WorkflowComments)
                .HasForeignKey(d => d.WorkflowDocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_comment_document");
        });

        modelBuilder.Entity<WorkflowDefinition>(entity =>
        {
            entity.HasKey(e => e.WorkflowDefinitionId).HasName("workflow_definitions_pkey");

            entity.ToTable("workflow_definitions", "person");

            entity.HasIndex(e => e.WorkflowCode, "workflow_definitions_workflow_code_key").IsUnique();

            entity.Property(e => e.WorkflowDefinitionId)
                .HasMaxLength(36)
                .HasColumnName("workflow_definition_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.DefinitionJson).HasColumnName("definition_json");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkflowCode)
                .HasMaxLength(100)
                .HasColumnName("workflow_code");
            entity.Property(e => e.WorkflowName)
                .HasMaxLength(255)
                .HasColumnName("workflow_name");
        });

        modelBuilder.Entity<WorkflowDocument>(entity =>
        {
            entity.HasKey(e => e.WorkflowDocumentId).HasName("workflow_documents_pkey");

            entity.ToTable("workflow_documents", "person");

            entity.HasIndex(e => e.RequesterEmployeeId, "idx_workflow_documents_requester");

            entity.HasIndex(e => e.Status, "idx_workflow_documents_status");

            entity.Property(e => e.WorkflowDocumentId)
                .HasMaxLength(36)
                .HasColumnName("workflow_document_id");
            entity.Property(e => e.CompletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("completed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrentStep)
                .HasDefaultValue(1)
                .HasColumnName("current_step");
            entity.Property(e => e.DocumentData)
                .HasColumnType("jsonb")
                .HasColumnName("document_data");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(100)
                .HasColumnName("document_no");
            entity.Property(e => e.DocumentTitle)
                .HasMaxLength(255)
                .HasColumnName("document_title");
            entity.Property(e => e.DocumentType)
                .HasMaxLength(100)
                .HasColumnName("document_type");
            entity.Property(e => e.RequesterEmployeeId)
                .HasMaxLength(100)
                .HasColumnName("requester_employee_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Pending'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkflowDefinitionId)
                .HasMaxLength(36)
                .HasColumnName("workflow_definition_id");

            entity.HasOne(d => d.WorkflowDefinition).WithMany(p => p.WorkflowDocuments)
                .HasForeignKey(d => d.WorkflowDefinitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_document_definition");
        });

        modelBuilder.Entity<WorkflowInbox>(entity =>
        {
            entity.HasKey(e => e.WorkflowInboxId).HasName("workflow_inbox_pkey");

            entity.ToTable("workflow_inbox", "person");

            entity.HasIndex(e => e.ReceiverEmployeeId, "idx_workflow_inbox_receiver");

            entity.Property(e => e.WorkflowInboxId)
                .HasMaxLength(36)
                .HasColumnName("workflow_inbox_id");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.ReceivedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("received_at");
            entity.Property(e => e.ReceiverEmployeeId)
                .HasMaxLength(100)
                .HasColumnName("receiver_employee_id");
            entity.Property(e => e.ReceiverRole)
                .HasMaxLength(100)
                .HasColumnName("receiver_role");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Pending'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.WorkflowDocumentId)
                .HasMaxLength(36)
                .HasColumnName("workflow_document_id");
            entity.Property(e => e.WorkflowInstanceId)
                .HasMaxLength(36)
                .HasColumnName("workflow_instance_id");

            entity.HasOne(d => d.WorkflowDocument).WithMany(p => p.WorkflowInboxes)
                .HasForeignKey(d => d.WorkflowDocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_inbox_document");

            entity.HasOne(d => d.WorkflowInstance).WithMany(p => p.WorkflowInboxes)
                .HasForeignKey(d => d.WorkflowInstanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_inbox_instance");
        });

        modelBuilder.Entity<WorkflowInstance>(entity =>
        {
            entity.HasKey(e => e.WorkflowInstanceId).HasName("workflow_instances_pkey");

            entity.ToTable("workflow_instances", "person");

            entity.HasIndex(e => e.Status, "idx_workflow_instances_status");

            entity.Property(e => e.WorkflowInstanceId)
                .HasMaxLength(36)
                .HasColumnName("workflow_instance_id");
            entity.Property(e => e.CurrentApproverEmployeeId)
                .HasMaxLength(100)
                .HasColumnName("current_approver_employee_id");
            entity.Property(e => e.CurrentApproverRole)
                .HasMaxLength(100)
                .HasColumnName("current_approver_role");
            entity.Property(e => e.CurrentStepNo)
                .HasDefaultValue(1)
                .HasColumnName("current_step_no");
            entity.Property(e => e.EndedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ended_at");
            entity.Property(e => e.StartedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("started_at");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Pending'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.WorkflowDefinitionId)
                .HasMaxLength(36)
                .HasColumnName("workflow_definition_id");
            entity.Property(e => e.WorkflowDocumentId)
                .HasMaxLength(36)
                .HasColumnName("workflow_document_id");

            entity.HasOne(d => d.WorkflowDefinition).WithMany(p => p.WorkflowInstances)
                .HasForeignKey(d => d.WorkflowDefinitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_instance_definition");

            entity.HasOne(d => d.WorkflowDocument).WithMany(p => p.WorkflowInstances)
                .HasForeignKey(d => d.WorkflowDocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_instance_document");
        });

        modelBuilder.Entity<WorkflowStep>(entity =>
        {
            entity.HasKey(e => e.WorkflowStepId).HasName("workflow_steps_pkey");

            entity.ToTable("workflow_steps", "person");

            entity.Property(e => e.WorkflowStepId)
                .HasMaxLength(36)
                .HasColumnName("workflow_step_id");
            entity.Property(e => e.ApproverEmployeeId)
                .HasMaxLength(100)
                .HasColumnName("approver_employee_id");
            entity.Property(e => e.ApproverRole)
                .HasMaxLength(100)
                .HasColumnName("approver_role");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Expression).HasColumnName("expression");
            entity.Property(e => e.IsRequired)
                .HasDefaultValue(true)
                .HasColumnName("is_required");
            entity.Property(e => e.OnApprove)
                .HasMaxLength(100)
                .HasColumnName("on_approve");
            entity.Property(e => e.OnReject)
                .HasMaxLength(100)
                .HasColumnName("on_reject");
            entity.Property(e => e.StepName)
                .HasMaxLength(255)
                .HasColumnName("step_name");
            entity.Property(e => e.StepNo).HasColumnName("step_no");
            entity.Property(e => e.StepType)
                .HasMaxLength(100)
                .HasColumnName("step_type");
            entity.Property(e => e.WorkflowDefinitionId)
                .HasMaxLength(36)
                .HasColumnName("workflow_definition_id");

            entity.HasOne(d => d.WorkflowDefinition).WithMany(p => p.WorkflowSteps)
                .HasForeignKey(d => d.WorkflowDefinitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_workflow_definition");
        });

        modelBuilder.Entity<WorkflowTransaction>(entity =>
        {
            entity.HasKey(e => e.WorkflowTransactionId).HasName("workflow_transactions_pkey");

            entity.ToTable("workflow_transactions", "person");

            entity.HasIndex(e => e.WorkflowDocumentId, "idx_workflow_transactions_document");

            entity.Property(e => e.WorkflowTransactionId)
                .HasMaxLength(36)
                .HasColumnName("workflow_transaction_id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.ActionDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("action_date");
            entity.Property(e => e.ApproverEmployeeId)
                .HasMaxLength(100)
                .HasColumnName("approver_employee_id");
            entity.Property(e => e.ApproverRole)
                .HasMaxLength(100)
                .HasColumnName("approver_role");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.StepName)
                .HasMaxLength(255)
                .HasColumnName("step_name");
            entity.Property(e => e.StepNo).HasColumnName("step_no");
            entity.Property(e => e.WorkflowDocumentId)
                .HasMaxLength(36)
                .HasColumnName("workflow_document_id");
            entity.Property(e => e.WorkflowInstanceId)
                .HasMaxLength(36)
                .HasColumnName("workflow_instance_id");

            entity.HasOne(d => d.WorkflowDocument).WithMany(p => p.WorkflowTransactions)
                .HasForeignKey(d => d.WorkflowDocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_transaction_document");

            entity.HasOne(d => d.WorkflowInstance).WithMany(p => p.WorkflowTransactions)
                .HasForeignKey(d => d.WorkflowInstanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_transaction_instance");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
