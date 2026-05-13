using CurrieTechnologies.Razor.SweetAlert2;
using Datamodels.Hrms;
using HrmsSolution.Service;
using Microsoft.AspNetCore.Components;

namespace Hrms_project.Components.Pages.Orignization
{
    public partial class Organization
    {
        [Inject]
        public IOrganizationService OriganiztionService { get; set; } = default!;
        public List<Datamodels.Hrms.Division> Divisions { get; set; } = new();

        private string searchEmpTerm = "";
        private string searchNewEmpTerm = "";
        private List<VEmployeeDetail> FilteredDeptEmps = new();
        private List<VEmployeeDetail> FilteredNewEmployees = new();

        private bool IsLoading = true;
        private bool IsLoadingDATA = false;

        // Modal Flags
        private bool _visibleDivision = false;
        private bool _visibleEditDept = false;
        private bool _visibleEditUnit = false;
        private bool _visibleManageEmps = false; // New!
        private bool _visibleAddExistingEmp = false;
        private IEnumerable<VEmployeeDetail> selectedEmployeesToAdd;

        private bool isEditingDept = false;
        private bool isEditingDivision = false;
        private bool isEditingUnit = false;

        private string searchTerm = "";
        private int currentIndex = 0;
        private int currentPage = 0;
        private int pageSize = 6;

        private List<Division> ShowDivisions = new();
        private List<Department> ShowDepartments = new();
        private List<Department> FilteredDepartments = new();
        private List<WorkUnit> workUnits = new();
        private List<VEmployeeDetail> allEmployees = new();
        private List<VEmployeeDetail> SearchResults = new();

        // Variables for Manage Employees Modal
        private List<VEmployeeDetail> currentDeptEmps = new();
        private string manageEmpDeptName = "";
        private Department manageEmpDept;

        private Division newDivision = new Division { Isactive = "1" };
        private Division editingDivision = new Division();
        private Department editingDept = new Department();
        private WorkUnit editingUnit = new WorkUnit();
        private string editingUnitDeptName = "";

        private Division CurrentDivision => (ShowDivisions != null && ShowDivisions.Count > currentIndex) ? ShowDivisions[currentIndex] : null;

        protected override async Task OnInitializedAsync() { await LoadOrganizationData(); }


        private void OpenAddExistingEmpModal()
        {
            _visibleManageEmps = false; // ปิดตัวจัดการหลักชั่วคราว
            _visibleAddExistingEmp = true;
        }

        private async Task LoadOrganizationData()
        {
            try
            {
                IsLoading = true;
                ShowDivisions = await OrganizationService.GetAllDivisionsAsync() ?? new();
                allEmployees = await EmployeeService.GetAllEmployeeDetailsAsync() ?? new();
                if (ShowDivisions.Any())
                {
                    if (currentIndex >= ShowDivisions.Count) currentIndex = ShowDivisions.Count - 1;
                    await LoadSubData(CurrentDivision?.DivisionId);
                }
                else
                {
                    ShowDepartments.Clear();
                    FilteredDepartments.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageService.Error($"Error: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadSubData(string divisionId)
        {
            if (string.IsNullOrEmpty(divisionId)) return;
            try
            {
                IsLoadingDATA = true;
                StateHasChanged();
                ShowDepartments = await OrganizationService.GetDepartmentsByDivisionIdAsync(divisionId) ?? new();
                var unitTasks = ShowDepartments.Select(d => OrganizationService.GetWorkUnitsByDepartmentIdAsync(d.DeptId));
                var results = await Task.WhenAll(unitTasks);
                workUnits = results.Where(r => r != null).SelectMany(r => r).ToList();
                allEmployees = await EmployeeService.GetAllEmployeeDetailsAsync() ?? new();
                ApplyFilter();
            }
            finally
            {
                IsLoadingDATA = false;
                StateHasChanged();
            }
        }
        private void ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                FilteredDepartments = ShowDepartments.ToList();
            else
                FilteredDepartments = ShowDepartments.Where(d => (d.DeptNameThai?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) || (d.DeptId?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false)).ToList();

            int totalItems = FilteredDepartments.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            if (totalPages == 0)
                currentPage = 0;
            else if (currentPage >= totalPages)
                currentPage = totalPages - 1;
            StateHasChanged();
        }

        private async Task LoadNextData()
        {
            if (currentIndex < ShowDivisions.Count - 1)
            {
                currentIndex++;
                currentPage = 0;
                searchTerm = "";
                await LoadSubData(CurrentDivision?.DivisionId);
            }
        }

        private async Task LoadPreviousData()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                currentPage = 0;
                searchTerm = "";
                await LoadSubData(CurrentDivision?.DivisionId);
            }
        }

        private void HandleSearch()
        {
            currentPage = 0;
            ApplyFilter();
        }

        private void ClearData()
        {
            searchTerm = "";
            currentPage = 0;
            ApplyFilter();
        }
        private void OpenEditDivision(Division div) 
        { 
            newDivision = new Division 
            { 
                DivisionId = div.DivisionId, 
                DivisionNameThai = div.DivisionNameThai, 
                DivisionNameEng = div.DivisionNameEng, 
                Isactive = div.Isactive }; 
            isEditingDivision = true; 
            editingDivision = newDivision; _visibleDivision = true; 
        }
        private async Task HandleSaveDivision() 
        { 
            newDivision.DivisionId = newDivision.DivisionId?.Trim(); 
            bool success = isEditingDivision ? await OrganizationService.UpdateDivisionAsync(newDivision) : await OrganizationService.AddDivisionAsync(newDivision); 
            if (success) 
            { 
                _visibleDivision = false; await LoadOrganizationData(); 
                MessageService.Success("บันทึกสำเร็จ"); 
            } 
            else 
            { 
                MessageService.Error("บันทึกไม่สำเร็จ"); 
            } 
        }
        private async Task HandleDeleteDivision(Division div)
        {
            var count = allEmployees.Count(e => e.DivisionId == div.DivisionId); 
            if (count > 0) 
            { 
                await Swal.FireAsync("ไม่สามารถลบได้", $"มีบุคลากร {count} คน สังกัดอยู่\nกรุณาย้ายออกก่อน",
                SweetAlertIcon.Error); return; 
            }
            var result = await Swal.FireAsync(new SweetAlertOptions 
            { 
                Title = $"ยืนยันการลบ '{div.DivisionNameThai}'?",
                Text = "ข้อมูลทั้งหมดจะถูกลบ", Icon = SweetAlertIcon.Warning, ShowCancelButton = true, 
                ConfirmButtonText = "ลบข้อมูล", ConfirmButtonColor = "#d33" }); 
            if (!string.IsNullOrEmpty(result.Value)) { 
                if (await OrganizationService.DeleteDivisionAsync(div.DivisionId)) { 
                    await Swal.FireAsync("สำเร็จ", "ลบเรียบร้อย", SweetAlertIcon.Success); 
                    await LoadOrganizationData(); 
                } else { 
                    await Swal.FireAsync("ผิดพลาด", "ลบไม่สำเร็จ", SweetAlertIcon.Error); 
                } 
            }
        }
        private void OpenAddDepartmentModal() { 
            editingDept = new Department 
            { 
                Isactive = "1", DivisionId = CurrentDivision?.DivisionId 
            };
            isEditingDept = false; _visibleEditDept = true; 
        }
        private void OpenEditDepartment(Department dept) 
        { 
            editingDept = new Department { 
                DeptId = dept.DeptId, 
                DeptNameThai = dept.DeptNameThai,
                DeptNameEng = dept.DeptNameEng, 
                DivisionId = dept.DivisionId, 
                Isactive = dept.Isactive 
            };
            isEditingDept = true; _visibleEditDept = true; 

        }

        private async Task HandleSaveDepartment() 
        { 
            editingDept.DeptId = editingDept.DeptId?.Trim();
            bool success = isEditingDept ? await OrganizationService.UpdateDepartmentAsync(editingDept) : await OrganizationService.AddDepartmentAsync(editingDept); 
            if (success) { 
                _visibleEditDept = false;
                searchTerm = "";
                await LoadSubData(CurrentDivision?.DivisionId); 
                MessageService.Success("บันทึกสำเร็จ");
            } else {
                MessageService.Error("บันทึกไม่สำเร็จ"); 
            } 
        }
        private async Task HandleDeleteDepartment(Department dept)
        {
            var count = allEmployees.Count(e => e.DeptId == dept.DeptId);
            if (count > 0)
            {
                await Swal.FireAsync("ไม่สามารถลบได้", $"มีบุคลากร {count} คน สังกัดอยู่\nกรุณาย้ายออกก่อน", SweetAlertIcon.Error);

                return;
            }
            var result = await Swal.FireAsync(new SweetAlertOptions 
            { Title = $"ยืนยันการลบฝ่าย '{dept.DeptNameThai}'?", Text = "กลุ่มงานภายในจะถูกลบไปด้วย", Icon = SweetAlertIcon.Warning, ShowCancelButton = true, ConfirmButtonText = "ลบข้อมูล", ConfirmButtonColor = "#d33" }); if (!string.IsNullOrEmpty(result.Value)) {
                if (await OrganizationService.DeleteDepartmentAsync(dept.DeptId)) { await Swal.FireAsync("สำเร็จ", "ลบเรียบร้อย", SweetAlertIcon.Success); await LoadSubData(CurrentDivision?.DivisionId); } else { await Swal.FireAsync("ผิดพลาด", "ลบไม่สำเร็จ", SweetAlertIcon.Error); } }
        }
        private void OpenAddUnitModal(Department dept) 
        {
            editingUnit = new WorkUnit 
            { 
                DeptId = dept.DeptId, Isactive = "1" 
            }; 
            editingUnitDeptName = dept.DeptNameThai; 
            isEditingUnit = false; 
            _visibleEditUnit = true; 
        }
        private void OpenEditUnit(WorkUnit unit, Department dept) 
        { 
            editingUnit = new WorkUnit {
                UnitId = unit.UnitId,
                UnitNameThai = unit.UnitNameThai, 
                UnitNameEng = unit.UnitNameEng,
                DeptId = unit.DeptId, 
                Isactive = unit.Isactive 
            };
            editingUnitDeptName = dept.DeptNameThai;
            isEditingUnit = true; 
            _visibleEditUnit = true; 
        }
        private async Task HandleSaveUnit() 
        { 
            editingUnit.UnitId = editingUnit.UnitId?.Trim();
            bool success = isEditingUnit ? await OrganizationService.UpdateWorkUnitAsync(editingUnit) : await OrganizationService.AddWorkUnitAsync(editingUnit); 
            if (success) {
                _visibleEditUnit = false; 
                searchTerm = ""; 
                await LoadSubData(CurrentDivision?.DivisionId);
                MessageService.Success("บันทึกสำเร็จ"); 
            } else { 
                MessageService.Error("บันทึกไม่สำเร็จ");
            } 
        }
        private async Task HandleDeleteUnit(WorkUnit unit) { _visibleEditUnit = false; var count = allEmployees.Count(e => e.UnitId == unit.UnitId); if (count > 0) { await Swal.FireAsync("ไม่สามารถลบได้", $"มีบุคลากร {count} คน สังกัดอยู่\nกรุณาย้ายออกก่อน", SweetAlertIcon.Error); return; } var result = await Swal.FireAsync(new SweetAlertOptions { Title = $"ลบกลุ่มงาน '{unit.UnitNameThai}'?", Icon = SweetAlertIcon.Warning, ShowCancelButton = true, ConfirmButtonText = "ยืนยันการลบ", ConfirmButtonColor = "#d33" }); if (!string.IsNullOrEmpty(result.Value)) { if (await OrganizationService.DeleteWorkUnitAsync(unit.UnitId)) { await Swal.FireAsync("สำเร็จ", "ลบเรียบร้อย", SweetAlertIcon.Success); await LoadSubData(CurrentDivision?.DivisionId); } else { await Swal.FireAsync("ผิดพลาด", "ลบไม่สำเร็จ", SweetAlertIcon.Error); } } }

        // =========================================================
        // +++ Feature ใหม่: จัดการบุคลากร (Manage Employees) +++
        // =========================================================

        // 1. เปิด Modal รายชื่อคนในฝ่าย
        private void OpenManageEmployeesModal(Department dept)
        {
            manageEmpDept = dept;
            manageEmpDeptName = dept.DeptNameThai;

            // ตั้งค่าทั้ง 2 ตัวแปรให้เหมือนกันตอนเริ่ม
            currentDeptEmps = allEmployees
                .Where(e => !string.IsNullOrEmpty(e.DeptId) && e.DeptId.Trim() == dept.DeptId.Trim())
                .ToList();
            FilteredDeptEmps = currentDeptEmps; // ให้ตัวค้นหาแสดงผลรายการนี้

            // ตั้งค่า FilteredNewEmployees ให้แสดงพนักงานที่ยังไม่ได้สังกัดฝ่ายนี้
            FilteredNewEmployees = allEmployees
                .Where(e => string.IsNullOrEmpty(e.DeptId) || e.DeptId.Trim() != dept.DeptId.Trim())
                .ToList();

            searchEmpTerm = ""; // เคลียร์ช่องค้นหา
            searchNewEmpTerm = ""; // เคลียร์ช่องค้นหา
            selectedEmployeesToAdd = null; // เคลียร์การเลือก
            _visibleManageEmps = true;
        }

        private void HandleEmpSearch()
        {
            if (string.IsNullOrWhiteSpace(searchEmpTerm))
            {
                // ถ้าช่องว่าง ให้แสดงทั้งหมด
                FilteredDeptEmps = currentDeptEmps.ToList();
            }
            else
            {
                // ค้นหาจากชื่อ นามสกุล หรือตำแหน่ง (ค้นหาในข้อมูลของฝ่ายนี้เท่านั้น)
                FilteredDeptEmps = currentDeptEmps
                    .Where(e => (e.FirstNameThai?.Contains(searchEmpTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (e.LastNameThai?.Contains(searchEmpTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (e.PositionId?.Contains(searchEmpTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (e.EmployeeId?.Contains(searchEmpTerm, StringComparison.OrdinalIgnoreCase) ?? false))
                    .ToList();
            }
            StateHasChanged();
        }

        private void HandleSearchNewEmployee()
        {
            if (string.IsNullOrWhiteSpace(searchNewEmpTerm))
            {
                // ถ้าช่องว่าง ให้แสดงทั้งหมด
                FilteredNewEmployees = allEmployees
                    .Where(e => string.IsNullOrEmpty(e.DeptId) || e.DeptId.Trim() != (manageEmpDept?.DeptId ?? ""))
                    .ToList();
            }
            else
            {
                // ค้นหาจากชื่อ นามสกุล หรือตำแหน่ง (ค้นหาเฉพาะพนักงานที่ยังไม่ได้สังกัดฝ่ายนี้)
                FilteredNewEmployees = allEmployees
                    .Where(e => (string.IsNullOrEmpty(e.DeptId) || e.DeptId.Trim() != (manageEmpDept?.DeptId ?? "")) &&
                                ((e.FirstNameThai?.Contains(searchNewEmpTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                 (e.LastNameThai?.Contains(searchNewEmpTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                 (e.PositionId?.Contains(searchNewEmpTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                 (e.EmployeeId?.Contains(searchNewEmpTerm, StringComparison.OrdinalIgnoreCase) ?? false)))
                    .ToList();
            }
            StateHasChanged();
        }


        private async Task AssignToDept(VEmployeeDetail emp)
        {
            if (manageEmpDept == null) return;

            // สมมติว่าคุณมี Service สำหรับอัปเดตพนักงาน
            // ตัวอย่าง: emp.DeptId = manageEmpDept.DeptId;
            // await EmployeeService.UpdateEmployeeAsync(emp);

            MessageService.Success($"ย้าย {emp.FirstNameThai} เข้าสู่ฝ่ายเรียบร้อยแล้ว");

            // รีเฟรชตารางหลังย้ายสำเร็จ
            await LoadOrganizationData();
            StateHasChanged();
        }

        private async Task HandleAssignExistingEmployee()
        {
            // ตรวจสอบว่ามีข้อมูลเลือกมาไหม
            if (selectedEmployeesToAdd == null || !selectedEmployeesToAdd.Any())
            {
                MessageService.Warning("กรุณาเลือกพนักงานอย่างน้อย 1 คน");
                return;
            }

            try
            {
                foreach (var emp in selectedEmployeesToAdd)
                {
                    // !!! จุดสำคัญ: ต้องมั่นใจว่า Service นี้เป็น async และรอ (await) ให้มันทำงานจบจริงๆ !!!
                    var result = await EmployeeService.AssignEmployeeToDeptAsync(emp.EmployeeId, manageEmpDept.DeptId, manageEmpDept.DivisionId);

                    // ถ้า API ของคุณคืนค่าเป็น bool หรือ object ให้เช็คว่ามันสำเร็จไหม
                    if (result == false)
                    {
                        throw new Exception("API ส่งค่ากลับมาว่าผิดพลาด");
                    }
                }

                // ถ้าผ่านทุก Loop ให้สั่งปิด Modal และโหลดข้อมูลใหม่
                _visibleAddExistingEmp = false;
                selectedEmployeesToAdd = null;
                await LoadOrganizationData(); // โหลดหน้าใหม่
                if (manageEmpDept != null)
                {
                    OpenManageEmployeesModal(manageEmpDept); // เปิด Modal ใหม่
                }
                MessageService.Success("อัปเดตข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                // ถ้าเกิด Error ให้โชว์ Modal ผิดพลาดที่คุณเห็นในภาพ
                await Swal.FireAsync("ผิดพลาด", "ไม่สามารถอัปเดตข้อมูลได้: " + ex.Message, "error");
            }
        }


        // 2. นำคนออกจากฝ่าย (Unassign)
        private async Task HandleUnassignEmployee(VEmployeeDetail emp)
        {
            var result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = $"นำ '{emp.FirstNameThai}' ออกจากฝ่าย?",
                Text = "พนักงานจะไม่มีสังกัดชั่วคราว ข้อมูลจะไม่ถูกลบ",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "ยืนยัน",
                CancelButtonColor = "#d33"
            });

            // ตรวจสอบว่าผู้ใช้กดยืนยัน (result.IsConfirmed)
            if (result.IsConfirmed)
            {
                try
                {
                    // เรียกใช้ Service จริง
                    bool success = await EmployeeService.UnassignEmployeeAsync(emp.EmployeeId);

                    if (success)
                    {
                        await Swal.FireAsync("สำเร็จ!", "นำพนักงานออกจากหน่วยงานเรียบร้อยแล้ว", SweetAlertIcon.Success);
                        // รีเฟรชตารางพนักงาน
                        await LoadOrganizationData();
                        if (manageEmpDept != null)
                        {
                            OpenManageEmployeesModal(manageEmpDept);
                        }
                    }
                    else
                    {
                        await Swal.FireAsync("ผิดพลาด", "ไม่สามารถอัปเดตข้อมูลได้", SweetAlertIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    await Swal.FireAsync("Error", $"เกิดข้อผิดพลาด: {ex.Message}", SweetAlertIcon.Error);
                }
            }
        }

        // 3. ไปหน้าสร้างพนักงานใหม่ (พร้อมส่ง Parameter ฝ่ายไปด้วย)
        private void NavigateToAddEmployee(Department dept)
        {
            // ส่ง Query String ไปที่หน้า Create
            NavigationManager.NavigateTo($"/employees/create?divisionId={dept.DivisionId}&deptId={dept.DeptId}");
        }

        private void NavigateToEditEmployee(string employeeId)
        {
            NavigationManager.NavigateTo($"/employees/edit/{employeeId}");
        }
    }
}
