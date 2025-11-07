//using CurrieTechnologies.Razor.SweetAlert2;
//using Datamodels.Hrms;
//using HrmsAppSolution.Services;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.EntityFrameworkCore;
//using System.Net.Mail;

//namespace HrmsSolution.Components.Pages
//{
//    public partial class Employee
//    {

//        [Inject] public EmployeeService Employeeservice { get; set; } = null!;

//        [Inject] public IDbContextFactory<Hrms_dbContext> ContextFactory { get; set; } = null!;

//        [Inject] private NavigationManager navigationManager { get; set; } = null!;

//        private List<Staff> showstaff = new List<Staff>();

//        private List<Staff> originalStaff = new List<Staff>();

//        private List<StaffDetail> originalStaffDetails = new List<StaffDetail>();

//        private string searchTerm = string.Empty;

//        private bool IsLoading { get; set; } = true;
//        private StaffDetail StaffDetails { get; set; } = new();

//        private List<StaffDetail>? _staffDetails = new List<StaffDetail>();

//        private StaffDetail _infoStaff { get; set; } = new();


//        public StaffDetail _staffDetailss = new StaffDetail();

//        private int indexnumber { get; set; } = 0;

//        protected override async Task OnInitializedAsync()
//        {
//            await LoadData();
//            IsLoading = false;
//            StateHasChanged();
//        }


//        private async Task LoadData()
//        {
//            await Task.Delay(100);
//            originalStaffDetails = (await Employeeservice.GetAllStaffDetailsAsync()).ToList();
//            StateHasChanged();
//        }

//        private string ConvertStaffDepartToString(char? staffDepart)
//        {
//            return staffDepart?.ToString() ?? "N/A";
//        }


//        //ค้นหาแบบ กรอกชื่อ
//        private void HandleSearch()
//        {
//            if (!originalStaff.Any())
//            {
//                originalStaff = new List<Staff>(showstaff);
//            }

//            if (!string.IsNullOrWhiteSpace(searchTerm))
//            {
//                showstaff = originalStaff.Where(staff =>
//                staff.StaffNameThai?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
//                staff.StaffSnameThai?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
//                staff.StaffNameEng?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
//                staff.StaffSnameEng?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
//                staff.StaffEmail?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true)
//            .ToList();
//            }
//            else
//            {
//                showstaff = new List<Staff>(originalStaff);
//            }
//        }

//        private void HandleKeyDown(KeyboardEventArgs e)
//        {
//            if (e.Key == "Enter")
//            {
//                HandleSearch();
//                // ปิดการติดตามปุ่ม "Enter"
//            }
//        }


//        private async Task Onswitch(string staffId)
//        {

//            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
//            {
//                Title = locallizer["ยืนยันเปลี่ยนสถานะ ?"],
//                Text = locallizer["คุณต้องการเปลี่ยนสถานะใช่หรือไม่ ?"],
//                Icon = SweetAlertIcon.Warning,
//                ShowCancelButton = true,
//                ConfirmButtonText = locallizer["เปลี่ยน"],
//                CancelButtonText = locallizer["ยกเลิก"]
//            });
//            if (!string.IsNullOrEmpty(result.Value))
//            {
//                try
//                {
//                    var db = ContextFactory.CreateDbContext();

//                    // แปลง staffId เป็นประเภทข้อมูลที่ตรงกับ Primary Key ของ StaffDetails
//                    var id = await db.StaffDetails.FindAsync(staffId);

//                    // ตรวจสอบว่า id ไม่เป็น null ก่อนที่จะทำการอัปเดต
//                    if (id != null)
//                    {
//                        // ทำการอัปเดตสถานะในฐานข้อมูล
//                        id.StaffDepart = (id.StaffDepart == "3") ? "e" : "3"; // ถ้าเป็นทำงานให้เปลี่ยนเป็นไม่ทำงาน และ ng่ทำงานให้เปลี่ยนเป็นทำงาน

//                        // บันทึกการเปลี่ยนแปลงลงในฐานข้อมูล
//                        await db.SaveChangesAsync();

//                        // รีเฟรชข้อมูลในตารางหลังจากการอัปเดต
//                        await UpdateTable();

//                        // แสดงข้อความเปลี่ยนสถานะสำเร็จ
//                        await Swal.FireAsync(new SweetAlertOptions
//                        {
//                            Title = locallizer["เปลี่ยนสถานะสำเร็จ"],
//                            Text = locallizer["คุณเปลี่ยนสถานะบุคลากรสำเร็จ"],
//                            Icon = SweetAlertIcon.Success,
//                            ShowConfirmButton = false,
//                            Timer = 2000
//                        });

//                        // นำผู้ใช้ไปยังหน้า employees หลังจากการเปลี่ยนสถานะ
//                        await LoadData();
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine(ex.ToString());
//                    throw;
//                }
//            }
//        }

//        private async Task Delete(string staffId)
//        {
//            Staff? infostaff = await Employeeservice.DeleteEmployeeAsync(staffId);

//            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
//            {
//                Title = locallizer["ลบข้อมูล ?"],
//                Text = locallizer["คุณต้องการลบข้อมูล คุณ"] + " " + infostaff.FullNameThai + " " + locallizer["ใช่หรือไม่ ?"],
//                Icon = SweetAlertIcon.Warning,
//                ShowCancelButton = true,
//                ConfirmButtonText = locallizer["ลบ"],
//                CancelButtonText = locallizer["ยกเลิก"]
//            });
//            if (!string.IsNullOrEmpty(result.Value))
//            {
//                try
//                {
//                    var db = ContextFactory.CreateDbContext();
//                    var id = await db.StaffDetails.FindAsync(staffId);
//                    db.StaffDetails.Remove(id);
//                    await db.SaveChangesAsync();
//                    await UpdateTable();
//                    StateHasChanged();

//                    await Swal.FireAsync(new SweetAlertOptions
//                    {
//                        Title = locallizer["ลบข้อมูลเสร็จสิ้น"],
//                        Text = locallizer["คุณลบข้อมูลผู้บริหารเสร็จสิ้น"],
//                        Icon = SweetAlertIcon.Success,
//                        ShowConfirmButton = false,
//                        Timer = 2000
//                    });
//                    // นำผู้ใช้ไปยังหน้า employees หลังจากการเปลี่ยนสถานะ
//                    await LoadData();
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine(ex.ToString());
//                    throw;
//                }
//            }
//        }

//        //private async void ResetPassword(string staffId)
//        //{
//        //    SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
//        //    {
//        //        Title = locallizer["รีเช็ตรหัสผ่าน ?"],
//        //        Text = locallizer["คุณต้องการรีเช็ตรหัสผ่านหรือไม่ ?"],
//        //        Icon = SweetAlertIcon.Warning,
//        //        ShowCancelButton = true,
//        //        ConfirmButtonText = locallizer["รีเช็ต"],
//        //        CancelButtonText = locallizer["ยกเลิก"]
//        //    });
//        //    if (!string.IsNullOrEmpty(result.Value))
//        //    {
//        //        try
//        //        {
//        //            StaffDetail? showstaffs = await serviceDb.GetAllstafftoeditId(staffId);
//        //            if (showstaffs != null)
//        //            {
//        //                _infoStaff = showstaffs;
//        //            }
//        //            _infoStaff.SecretCode = new Random().Next(100000, 999999);
//        //            _infoStaff.Password = null;
//        //            var db = ContextFactory.CreateDbContext();
//        //            await serviceDb.Updatedata(_infoStaff);
//        //            SendInfomationToMail();
//        //            await Swal.FireAsync(new SweetAlertOptions
//        //            {
//        //                Title = locallizer["เสร็จสิ้น"],
//        //                Text = locallizer["รีเช็ตรหัสผ่านเสร็จสิ้น"],
//        //                Icon = SweetAlertIcon.Success,
//        //                ShowConfirmButton = false,
//        //                Timer = 5000
//        //            });
//        //            navigationManager.NavigateTo($"/infonewsecertcode/{_infoStaff.StaffId}");
//        //        }
//        //        catch (Exception ex)
//        //        {
//        //            Console.WriteLine(ex.ToString());
//        //            throw;
//        //        }
//        //    }
//        //}

//        private void SendInfomationToMail()
//        {
//            try
//            {
//                Swal.FireAsync(new SweetAlertOptions
//                {
//                    Title = locallizer["กรุณารอสักครู่"],
//                    Icon = SweetAlertIcon.Info,
//                    Text = locallizer["กำลังดำเนินการรีเช็ตรหัสผ่าน กรุณารอสักครู่"],
//                    ShowConfirmButton = false,
//                    TimerProgressBar = true,
//                    Timer = 5000
//                });
//                using (MailMessage mail = new MailMessage())
//                {
//                    mail.From = new MailAddress("edocpsuth2024@gmail.com"); //ระบุชื่ออีเมลต้นทาง
//                    mail.To.Add(_infoStaff.StaffEmail); //ระบุชื่ออีเมลปลายทาง
//                    mail.Subject = "แจ้งการเปลี่ยนรหัสผ่านผู้ใช้งานบุคลากร ระบบเอกสารอิเล็กทรอนิกส์ (E-Docs)";
//                    mail.Body = "<p>เรียนคุณ " + _infoStaff.FullNameThai + "(" + _infoStaff.FullNameEng + ") " +
//                                "ทางเราได้ทำการรีเช็ตรหัสผ่านการเข้าสู่ระบบระบบเอกสารอิเล็กทรอนิกส์ (E-docs) แล้วเรียบร้อยโดยข้อมูลผู้ใช้งานของท่าน มีรายละเอียดดังนี้ <br/>" +
//                                "</p>" +
//                                "<b>ชื่อผู้ใช้งาน (Username) : </b>" + _infoStaff.Username + "<br/>" +
//                                "<b>รหัสพิสูจน์ตัวตน (Secret Code) : </b>" + _infoStaff.SecretCode + "<br/>" +
//                                "<p>หมายเหตุ : อีเมลฉบับนี้เป็นการแจ้งข้อมูลอัตโนมัติ ไม่สามารถตอบกลับได้ </p> <br/>" +
//                                "<p>หากมีข้อสงสัยเกี่ยวกับรายละเอียดหรือวิธีการใช้งานระบบ สามารถติดต่อสอบถาม ตามช่องทางที่แจ้งไว้ในประกาศนั้นได้</p>  <br/>" +
//                                "<p>เว็บไซต์เข้าใช้งานระบบเอกสารอิเล็กทรอนิกส์ : https://docs.psu.ac.th/ </p>  <br/>";
//                    mail.IsBodyHtml = true;

//                    using (SmtpClient stmp = new SmtpClient("smtp.gmail.com", 587))
//                    {
//                        stmp.Credentials = new System.Net.NetworkCredential("edocpsuth2024@gmail.com", "kakjeskwbevsrudh"); //อีเมล และ App Password
//                        stmp.EnableSsl = true;
//                        stmp.Send(mail);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                throw;
//            }
//        }

//        private async Task UpdateTable()
//        {
//            var db = ContextFactory.CreateDbContext();
//            _staffDetails = await db.StaffDetails.OrderByDescending(x => x.StaffId).ToListAsync();
//        }

//        private void Topage(int page, string StaffId)
//        {

//            switch (page)
//            {
//                case 1:
//                    navigationManager.NavigateTo($"/infoemployee/{StaffId}");
//                    break;

//                case 2:
//                    navigationManager.NavigateTo($"/editemployee/{StaffId}");
//                    break;
//            }
//        }

//        private void ToAddEmployee()
//        {
//            navigationManager.NavigateTo($"/addemployee");
//        }

//    }
//}
