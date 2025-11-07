using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Datamodels.Hrms;

public partial class Hrms_dbContext : DbContext
{
    public Hrms_dbContext(DbContextOptions<Hrms_dbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Campus> Campuses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<ExampleTest> ExampleTests { get; set; }

    public virtual DbSet<Executive> Executives { get; set; }

    public virtual DbSet<ExecutivePosition> ExecutivePositions { get; set; }

    public virtual DbSet<ExecutiveStaff> ExecutiveStaffs { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<LoginSession> LoginSessions { get; set; }

    public virtual DbSet<Mission> Missions { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StaffDetail> StaffDetails { get; set; }

    public virtual DbSet<StaffPosition> StaffPositions { get; set; }

    public virtual DbSet<StaffTitle> StaffTitles { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Campus>(entity =>
        {
            entity.HasKey(e => e.CampId).HasName("campus_pkey");

            entity.ToTable("campus", "person");

            entity.Property(e => e.CampId)
                .HasMaxLength(2)
                .HasColumnName("camp_id");
            entity.Property(e => e.CampAdd)
                .HasMaxLength(2000)
                .HasColumnName("camp_add");
            entity.Property(e => e.CampDesc)
                .HasMaxLength(2000)
                .HasColumnName("camp_desc");
            entity.Property(e => e.CampNameEng)
                .HasMaxLength(200)
                .HasColumnName("camp_name_eng");
            entity.Property(e => e.CampNameThai)
                .HasMaxLength(200)
                .HasColumnName("camp_name_thai");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("department_pkey");

            entity.ToTable("department", "person");

            entity.Property(e => e.DeptId)
                .HasMaxLength(3)
                .HasColumnName("dept_id");
            entity.Property(e => e.CampusId)
                .HasMaxLength(4)
                .HasColumnName("campus_id");
            entity.Property(e => e.CentralId)
                .HasMaxLength(10)
                .HasColumnName("central_id");
            entity.Property(e => e.DeptDesc)
                .HasMaxLength(200)
                .HasColumnName("dept_desc");
            entity.Property(e => e.DeptMissionId)
                .HasMaxLength(10)
                .HasColumnName("dept_mission_id");
            entity.Property(e => e.DeptNameEng)
                .HasMaxLength(200)
                .HasColumnName("dept_name_eng");
            entity.Property(e => e.DeptNameThai)
                .HasMaxLength(200)
                .HasColumnName("dept_name_thai");
            entity.Property(e => e.DeptTel)
                .HasMaxLength(100)
                .HasColumnName("dept_tel");
            entity.Property(e => e.DeptTelFax)
                .HasMaxLength(200)
                .HasColumnName("dept_tel_fax");
            entity.Property(e => e.ExternalDocNo)
                .HasMaxLength(100)
                .HasColumnName("external_doc_no");
            entity.Property(e => e.FacId)
                .HasMaxLength(3)
                .HasColumnName("fac_id");
            entity.Property(e => e.InternalDocNo)
                .HasMaxLength(100)
                .HasColumnName("internal_doc_no");
            entity.Property(e => e.Isactive)
                .HasMaxLength(1)
                .HasColumnName("isactive");
            entity.Property(e => e.IscedId)
                .HasMaxLength(4)
                .HasColumnName("isced_id");
            entity.Property(e => e.JobId)
                .HasMaxLength(4)
                .HasColumnName("job_id");
            entity.Property(e => e.MissionId)
                .HasMaxLength(10)
                .HasColumnName("mission_id");
            entity.Property(e => e.NewDeptId)
                .HasMaxLength(100)
                .HasColumnName("new_dept_id");
            entity.Property(e => e.OldDept)
                .HasMaxLength(3)
                .HasColumnName("old_dept");
            entity.Property(e => e.Priority)
                .HasMaxLength(3)
                .HasColumnName("priority");
        });

        modelBuilder.Entity<ExampleTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("example_test_pkey");

            entity.ToTable("example_test", "person");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CampId)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("camp_id");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Executive>(entity =>
        {
            entity.ToTable("executive", "person");

            entity.Property(e => e.ExecutiveId)
                .HasMaxLength(400)
                .HasColumnName("executive_id");
            entity.Property(e => e.CampusId)
                .HasMaxLength(2)
                .HasColumnName("campus_id");
            entity.Property(e => e.DeptId)
                .HasMaxLength(3)
                .HasColumnName("dept_id");
            entity.Property(e => e.ExecutivePositionId)
                .HasMaxLength(2)
                .HasColumnName("executive_position_id");
            entity.Property(e => e.FacId)
                .HasMaxLength(2)
                .HasColumnName("fac_id");
            entity.Property(e => e.Isactive)
                .HasMaxLength(1)
                .HasColumnName("isactive");
            entity.Property(e => e.SectId)
                .HasMaxLength(3)
                .HasColumnName("sect_id");
            entity.Property(e => e.StaffAdminTemp)
                .HasMaxLength(20)
                .HasColumnName("staff_admin_temp");
            entity.Property(e => e.StaffId)
                .HasMaxLength(7)
                .HasColumnName("staff_id");
            entity.Property(e => e.UnitId)
                .HasMaxLength(3)
                .HasColumnName("unit_id");
        });

        modelBuilder.Entity<ExecutivePosition>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("executive_position", "person");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .HasColumnName("id");
            entity.Property(e => e.Level)
                .HasDefaultValue(1)
                .HasColumnName("level");
            entity.Property(e => e.NameEng)
                .HasMaxLength(400)
                .HasColumnName("name_eng");
            entity.Property(e => e.NameThai)
                .HasMaxLength(400)
                .HasColumnName("name_thai");
        });

        modelBuilder.Entity<ExecutiveStaff>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("executive_staff", "person");

            entity.Property(e => e.AdminId)
                .HasMaxLength(2)
                .HasColumnName("admin_id");
            entity.Property(e => e.AdminLevel).HasColumnName("admin_level");
            entity.Property(e => e.AdminNameEng)
                .HasMaxLength(400)
                .HasColumnName("admin_name_eng");
            entity.Property(e => e.AdminNameThai)
                .HasMaxLength(400)
                .HasColumnName("admin_name_thai");
            entity.Property(e => e.CampusId)
                .HasMaxLength(2)
                .HasColumnName("campus_id");
            entity.Property(e => e.CitizenId)
                .HasMaxLength(13)
                .HasColumnName("citizen_id");
            entity.Property(e => e.DeptId)
                .HasMaxLength(3)
                .HasColumnName("dept_id");
            entity.Property(e => e.DivisionFull)
                .HasMaxLength(200)
                .HasColumnName("division_full");
            entity.Property(e => e.ExecutiveName)
                .HasMaxLength(401)
                .HasColumnName("executive_name");
            entity.Property(e => e.ExecutiveNameKey)
                .HasMaxLength(400)
                .HasColumnName("executive_name_key");
            entity.Property(e => e.FacId)
                .HasMaxLength(2)
                .HasColumnName("fac_id");
            entity.Property(e => e.Isactive)
                .HasMaxLength(1)
                .HasColumnName("isactive");
            entity.Property(e => e.Key)
                .HasMaxLength(400)
                .HasColumnName("key");
            entity.Property(e => e.PosKey)
                .HasMaxLength(4)
                .HasColumnName("pos_key");
            entity.Property(e => e.SectId)
                .HasMaxLength(3)
                .HasColumnName("sect_id");
            entity.Property(e => e.StaffAdminTemp)
                .HasMaxLength(20)
                .HasColumnName("staff_admin_temp");
            entity.Property(e => e.StaffDepart)
                .HasMaxLength(1)
                .HasColumnName("staff_depart");
            entity.Property(e => e.StaffId)
                .HasMaxLength(7)
                .HasColumnName("staff_id");
            entity.Property(e => e.StaffNameThai)
                .HasMaxLength(200)
                .HasColumnName("staff_name_thai");
            entity.Property(e => e.StaffSnameThai)
                .HasMaxLength(200)
                .HasColumnName("staff_sname_thai");
            entity.Property(e => e.UnitId)
                .HasMaxLength(3)
                .HasColumnName("unit_id");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacId).HasName("faculty_pkey");

            entity.ToTable("faculty", "person");

            entity.Property(e => e.FacId)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("fac_id");
            entity.Property(e => e.CampId)
                .HasMaxLength(2)
                .HasColumnName("camp_id");
            entity.Property(e => e.CentralId)
                .HasMaxLength(10)
                .HasColumnName("central_id");
            entity.Property(e => e.EmpNumber)
                .HasMaxLength(10)
                .HasColumnName("emp_number");
            entity.Property(e => e.ExternalDocNo)
                .HasMaxLength(50)
                .HasColumnName("external_doc_no");
            entity.Property(e => e.FacDesc)
                .HasMaxLength(60)
                .HasColumnName("fac_desc");
            entity.Property(e => e.FacGrpId)
                .HasMaxLength(2)
                .HasColumnName("fac_grp_id");
            entity.Property(e => e.FacNameEng)
                .HasMaxLength(200)
                .HasColumnName("fac_name_eng");
            entity.Property(e => e.FacNameThai)
                .HasMaxLength(200)
                .HasColumnName("fac_name_thai");
            entity.Property(e => e.FacTelFax)
                .HasMaxLength(50)
                .HasColumnName("fac_tel_fax");
            entity.Property(e => e.InternalDocNo)
                .HasMaxLength(50)
                .HasColumnName("internal_doc_no");
            entity.Property(e => e.Isactive)
                .HasMaxLength(1)
                .HasColumnName("isactive");
            entity.Property(e => e.IscedId)
                .HasMaxLength(10)
                .HasColumnName("isced_id");
            entity.Property(e => e.MissionId)
                .HasMaxLength(10)
                .HasColumnName("mission_id");
            entity.Property(e => e.NFacId)
                .HasMaxLength(5)
                .HasColumnName("n_fac_id");
            entity.Property(e => e.NewFacId)
                .HasMaxLength(10)
                .HasColumnName("new_fac_id");
            entity.Property(e => e.OldFac)
                .HasMaxLength(2)
                .HasColumnName("old_fac");
            entity.Property(e => e.ShortNameThai)
                .HasMaxLength(10)
                .HasColumnName("short_name_thai");
            entity.Property(e => e.StaffNumber)
                .HasMaxLength(10)
                .HasColumnName("staff_number");
        });

        modelBuilder.Entity<LoginSession>(entity =>
        {
            entity.ToTable("login_session", "person");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.DateCreated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateExpired)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_expired");
            entity.Property(e => e.Ip).HasColumnName("ip");
            entity.Property(e => e.StaffId)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("staff_id");
        });

        modelBuilder.Entity<Mission>(entity =>
        {
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

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.SectId).HasName("section_pkey");

            entity.ToTable("section", "person");

            entity.Property(e => e.SectId)
                .HasMaxLength(3)
                .HasColumnName("sect_id");
            entity.Property(e => e.CampusId)
                .HasMaxLength(2)
                .HasColumnName("campus_id");
            entity.Property(e => e.CentralId)
                .HasMaxLength(10)
                .HasColumnName("central_id");
            entity.Property(e => e.DeptId)
                .HasMaxLength(3)
                .HasColumnName("dept_id");
            entity.Property(e => e.ExternalDocNo)
                .HasMaxLength(100)
                .HasColumnName("external_doc_no");
            entity.Property(e => e.FacId)
                .HasMaxLength(2)
                .HasColumnName("fac_id");
            entity.Property(e => e.InternalDocNo)
                .HasMaxLength(100)
                .HasColumnName("internal_doc_no");
            entity.Property(e => e.Isactive)
                .HasMaxLength(1)
                .HasColumnName("isactive");
            entity.Property(e => e.MissionId)
                .HasMaxLength(10)
                .HasColumnName("mission_id");
            entity.Property(e => e.NewSectionId)
                .HasMaxLength(10)
                .HasColumnName("new_section_id");
            entity.Property(e => e.OldSect)
                .HasMaxLength(3)
                .HasColumnName("old_sect");
            entity.Property(e => e.SectDesc)
                .HasMaxLength(60)
                .HasColumnName("sect_desc");
            entity.Property(e => e.SectNameEng)
                .HasMaxLength(200)
                .HasColumnName("sect_name_eng");
            entity.Property(e => e.SectNameThai)
                .HasMaxLength(200)
                .HasColumnName("sect_name_thai");
            entity.Property(e => e.SectTelFax)
                .HasMaxLength(50)
                .HasColumnName("sect_tel_fax");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("staff", "person");

            entity.Property(e => e.CampNameEng)
                .HasMaxLength(200)
                .HasColumnName("camp_name_eng");
            entity.Property(e => e.CampNameThai)
                .HasMaxLength(200)
                .HasColumnName("camp_name_thai");
            entity.Property(e => e.CampusId)
                .HasMaxLength(2)
                .HasColumnName("campus_id");
            entity.Property(e => e.DeptId)
                .HasMaxLength(3)
                .HasColumnName("dept_id");
            entity.Property(e => e.DeptNameEng)
                .HasMaxLength(200)
                .HasColumnName("dept_name_eng");
            entity.Property(e => e.DeptNameThai)
                .HasMaxLength(200)
                .HasColumnName("dept_name_thai");
            entity.Property(e => e.FacId)
                .HasMaxLength(2)
                .HasColumnName("fac_id");
            entity.Property(e => e.FacNameEng)
                .HasMaxLength(200)
                .HasColumnName("fac_name_eng");
            entity.Property(e => e.FacNameThai)
                .HasMaxLength(200)
                .HasColumnName("fac_name_thai");
            entity.Property(e => e.FullNameEng)
                .HasMaxLength(401)
                .HasColumnName("full_name_eng");
            entity.Property(e => e.FullNameThai)
                .HasMaxLength(401)
                .HasColumnName("full_name_thai");
            entity.Property(e => e.PosId)
                .HasMaxLength(4)
                .HasColumnName("pos_id");
            entity.Property(e => e.PosNameEng)
                .HasMaxLength(400)
                .HasColumnName("pos_name_eng");
            entity.Property(e => e.PosNameThai)
                .HasMaxLength(400)
                .HasColumnName("pos_name_thai");
            entity.Property(e => e.PosType)
                .HasMaxLength(2)
                .HasColumnName("pos_type");
            entity.Property(e => e.SectId)
                .HasMaxLength(3)
                .HasColumnName("sect_id");
            entity.Property(e => e.SectNameEng)
                .HasMaxLength(200)
                .HasColumnName("sect_name_eng");
            entity.Property(e => e.SectNameThai)
                .HasMaxLength(200)
                .HasColumnName("sect_name_thai");
            entity.Property(e => e.StaffDateOut)
                .HasMaxLength(8)
                .HasColumnName("staff_date_out");
            entity.Property(e => e.StaffDepart)
                .HasMaxLength(1)
                .HasColumnName("staff_depart");
            entity.Property(e => e.StaffEmail)
                .HasMaxLength(40)
                .HasColumnName("staff_email");
            entity.Property(e => e.StaffEnd)
                .HasMaxLength(8)
                .HasColumnName("staff_end");
            entity.Property(e => e.StaffId)
                .HasMaxLength(7)
                .HasColumnName("staff_id");
            entity.Property(e => e.StaffNameEng)
                .HasMaxLength(200)
                .HasColumnName("staff_name_eng");
            entity.Property(e => e.StaffNameThai)
                .HasMaxLength(200)
                .HasColumnName("staff_name_thai");
            entity.Property(e => e.StaffPersId)
                .HasMaxLength(13)
                .HasColumnName("staff_pers_id");
            entity.Property(e => e.StaffPicture)
                .HasMaxLength(12)
                .HasColumnName("staff_picture");
            entity.Property(e => e.StaffSnameEng)
                .HasMaxLength(200)
                .HasColumnName("staff_sname_eng");
            entity.Property(e => e.StaffSnameThai)
                .HasMaxLength(200)
                .HasColumnName("staff_sname_thai");
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
            entity.Property(e => e.UnitId)
                .HasMaxLength(3)
                .HasColumnName("unit_id");
            entity.Property(e => e.UnitNameEng)
                .HasMaxLength(70)
                .HasColumnName("unit_name_eng");
            entity.Property(e => e.UnitNameThai)
                .HasMaxLength(70)
                .HasColumnName("unit_name_thai");
            entity.Property(e => e.WorkBegin)
                .HasMaxLength(8)
                .HasColumnName("work_begin");
            entity.Property(e => e.WorkEnd)
                .HasMaxLength(8)
                .HasColumnName("work_end");
        });

        modelBuilder.Entity<StaffDetail>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("staff_detail_pkey");

            entity.ToTable("staff_detail", "person");

            entity.Property(e => e.StaffId)
                .HasMaxLength(7)
                .HasColumnName("staff_id");
            entity.Property(e => e.CampusId)
                .HasMaxLength(2)
                .HasColumnName("campus_id");
            entity.Property(e => e.DeptId)
                .HasMaxLength(3)
                .HasColumnName("dept_id");
            entity.Property(e => e.FacId)
                .HasMaxLength(2)
                .HasColumnName("fac_id");
            entity.Property(e => e.FullNameEng)
                .HasMaxLength(401)
                .HasColumnName("full_name_eng");
            entity.Property(e => e.FullNameThai)
                .HasMaxLength(401)
                .HasColumnName("full_name_thai");
            entity.Property(e => e.Password)
                .HasMaxLength(400)
                .HasColumnName("password");
            entity.Property(e => e.Pincode)
                .HasMaxLength(400)
                .HasColumnName("pincode");
            entity.Property(e => e.PosId)
                .HasMaxLength(4)
                .HasColumnName("pos_id");
            entity.Property(e => e.SecretCode)
                .HasPrecision(6)
                .HasColumnName("secret_code");
            entity.Property(e => e.SectId)
                .HasMaxLength(3)
                .HasColumnName("sect_id");
            entity.Property(e => e.StaffDateOut)
                .HasMaxLength(8)
                .HasColumnName("staff_date_out");
            entity.Property(e => e.StaffDepart)
                .HasMaxLength(1)
                .HasColumnName("staff_depart");
            entity.Property(e => e.StaffEmail)
                .HasMaxLength(40)
                .HasColumnName("staff_email");
            entity.Property(e => e.StaffEnd)
                .HasMaxLength(8)
                .HasColumnName("staff_end");
            entity.Property(e => e.StaffNameEng)
                .HasMaxLength(200)
                .HasColumnName("staff_name_eng");
            entity.Property(e => e.StaffNameThai)
                .HasMaxLength(200)
                .HasColumnName("staff_name_thai");
            entity.Property(e => e.StaffPersId)
                .HasMaxLength(13)
                .HasColumnName("staff_pers_id");
            entity.Property(e => e.StaffPicture)
                .HasMaxLength(12)
                .HasColumnName("staff_picture");
            entity.Property(e => e.StaffSnameEng)
                .HasMaxLength(200)
                .HasColumnName("staff_sname_eng");
            entity.Property(e => e.StaffSnameThai)
                .HasMaxLength(200)
                .HasColumnName("staff_sname_thai");
            entity.Property(e => e.TitleId)
                .HasMaxLength(2)
                .HasColumnName("title_id");
            entity.Property(e => e.UnitId)
                .HasMaxLength(3)
                .HasColumnName("unit_id");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
            entity.Property(e => e.WorkBegin)
                .HasMaxLength(8)
                .HasColumnName("work_begin");
            entity.Property(e => e.WorkEnd)
                .HasMaxLength(8)
                .HasColumnName("work_end");
        });

        modelBuilder.Entity<StaffPosition>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("staff_position", "person");

            entity.Property(e => e.PosId)
                .HasMaxLength(4)
                .HasColumnName("pos_id");
            entity.Property(e => e.PosNameEng)
                .HasMaxLength(400)
                .HasColumnName("pos_name_eng");
            entity.Property(e => e.PosNameThai)
                .HasMaxLength(400)
                .HasColumnName("pos_name_thai");
            entity.Property(e => e.PosType)
                .HasMaxLength(2)
                .HasColumnName("pos_type");
        });

        modelBuilder.Entity<StaffTitle>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("staff_title", "person");

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

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.ToTable("unit", "person");

            entity.Property(e => e.UnitId)
                .HasMaxLength(3)
                .HasColumnName("unit_id");
            entity.Property(e => e.CampusId)
                .HasMaxLength(2)
                .HasColumnName("campus_id");
            entity.Property(e => e.CentralId)
                .HasMaxLength(10)
                .HasColumnName("central_id");
            entity.Property(e => e.DeptId)
                .HasMaxLength(3)
                .HasColumnName("dept_id");
            entity.Property(e => e.ExternalDocNo)
                .HasMaxLength(100)
                .HasColumnName("external_doc_no");
            entity.Property(e => e.FacId)
                .HasMaxLength(2)
                .HasColumnName("fac_id");
            entity.Property(e => e.InternalDocNo)
                .HasMaxLength(100)
                .HasColumnName("internal_doc_no");
            entity.Property(e => e.Isactive)
                .HasMaxLength(1)
                .HasColumnName("isactive");
            entity.Property(e => e.MissionId)
                .HasMaxLength(10)
                .HasColumnName("mission_id");
            entity.Property(e => e.NewUnitId)
                .HasMaxLength(3)
                .HasColumnName("new_unit_id");
            entity.Property(e => e.OldUnit)
                .HasMaxLength(3)
                .HasColumnName("old_unit");
            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.SectId)
                .HasMaxLength(3)
                .HasColumnName("sect_id");
            entity.Property(e => e.UnitDesc)
                .HasMaxLength(60)
                .HasColumnName("unit_desc");
            entity.Property(e => e.UnitNameEng)
                .HasMaxLength(70)
                .HasColumnName("unit_name_eng");
            entity.Property(e => e.UnitNameThai)
                .HasMaxLength(70)
                .HasColumnName("unit_name_thai");
            entity.Property(e => e.UnitTelFax)
                .HasMaxLength(50)
                .HasColumnName("unit_tel_fax");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
