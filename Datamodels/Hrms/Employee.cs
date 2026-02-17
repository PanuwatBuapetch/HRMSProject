using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string? FirstNameThai { get; set; }

    public string? FirstNameEng { get; set; }

    public string? LastNameThai { get; set; }

    public string? LastNameEng { get; set; }

    public string? FullNameThai { get; set; }

    public string? FullNameEng { get; set; }

    public string? TitleId { get; set; }

    public string? PositionId { get; set; }

    public string? LocationId { get; set; }

    public string? DivisionId { get; set; }

    public string? DeptId { get; set; }

    public string? TeamId { get; set; }

    public string? UnitId { get; set; }

    public string? PictureUrl { get; set; }

    public string? EmploymentStatus { get; set; }

    public string? Email { get; set; }

    public string? EndDate { get; set; }

    public string? CitizenId { get; set; }

    public string? TerminationDate { get; set; }

    public string? StartDate { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Pincode { get; set; }

    public decimal? SecretCode { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Gender { get; set; }

    public string? Nationality { get; set; }

    public string? Religion { get; set; }

    public string? MilitaryStatus { get; set; }

    public string? EmergencyContactName { get; set; }

    public string? EmergencyContactPhone { get; set; }

    public string? EmergencyContactRelation { get; set; }

    public string? CurrentAddress { get; set; }

    public string? PermanentAddress { get; set; }

    public string? BankName { get; set; }

    public string? BankAccountNo { get; set; }

    public string? SocialSecurityNo { get; set; }

    public string? TaxId { get; set; }

    public string? ManagerId { get; set; }
}
