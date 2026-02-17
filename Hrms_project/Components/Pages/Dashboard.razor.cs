using ChartJs.Blazor.BarChart;
using ChartJs.Blazor.Common;
using ChartJs.Blazor.Common.Axes;
using ChartJs.Blazor.Common.Axes.Ticks;
using ChartJs.Blazor.PieChart;
using ChartJs.Blazor.Util;
using Datamodels.Hrms;
using HrmsSolution.Service;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsApp.Pages
{
    public partial class Dashboard : ComponentBase
    {
        // --- Dependency Injection ---
        [Inject] public IEmployeeService EmployeeService { get; set; } = null!;
        [Inject] public IDepartmentService DepartmentService { get; set; } = null!;
        [Inject] public IManagementService ManagementService { get; set; } = null!;

        // --- Data Properties ---
        protected List<Employee> staffdetails = new();
        protected List<Management> Managements = new();
        protected List<Department> departments = new();
        protected int PositionsCount = 0;
        protected bool IsLoading { get; set; } = true;

        // --- Chart Configurations ---
        protected BarConfig? _barConfig;
        protected PieConfig? _statusPieConfig;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                IsLoading = true;

                // 1. ดึงข้อมูลพนักงานทั้งหมด
                var empList = await EmployeeService.GetAllEmployeesAsync();
                staffdetails = empList?.ToList() ?? new List<Employee>();

                // 2. ดึงข้อมูลแผนก
                var deptList = await DepartmentService.GetAllDepartmentsAsync();
                departments = deptList?.ToList() ?? new List<Department>();

                // 3. ดึงข้อมูลการแต่งตั้งผู้บริหาร
                var mgtList = await ManagementService.GetAllManagementAsync();
                Managements = mgtList?.ToList() ?? new List<Management>();

                // 4. ดึงข้อมูลตำแหน่งบริหารทั้งหมด
                var posList = await ManagementService.GetAllManagementPositionsAsync();
                PositionsCount = posList?.Count() ?? 0;

                // 5. ตั้งค่ากราฟ
                ConfigureBarConfig();
                ConfigureStatusPieConfig();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Dashboard Error: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
                StateHasChanged();
            }
        }

        /// <summary>
        /// กราฟแท่งแสดงจำนวนพนักงานรายแผนก
        /// </summary>
        private void ConfigureBarConfig()
        {
            _barConfig = new BarConfig();
            _barConfig.Options = new BarOptions
            {
                Responsive = true,
                MaintainAspectRatio = false,
                Title = new OptionsTitle { Display = true, Text = "จำนวนพนักงานแยกตามแผนก" },
                Legend = new Legend { Display = false },
                Scales = new BarScales
                {
                    YAxes = new List<CartesianAxis>
                    {
                        new LinearCartesianAxis { Ticks = new LinearCartesianTicks { BeginAtZero = true } }
                    }
                }
            };

            var dataset = new BarDataset<int>
            {
                Label = "จำนวน (คน)",
                BackgroundColor = ColorUtil.ColorHexString(54, 162, 235) // สีฟ้า
            };

            foreach (var dept in departments)
            {
                _barConfig.Data.Labels.Add(dept.DeptNameThai);
                // นับจำนวนพนักงานที่ DeptId ตรงกับแผนกนั้นๆ
                dataset.Add(staffdetails.Count(e => e.DeptId == dept.DeptId));
            }

            _barConfig.Data.Datasets.Clear();
            _barConfig.Data.Datasets.Add(dataset);
        }

        /// <summary>
        /// กราฟวงกลมแสดงสัดส่วนสถานะพนักงาน
        /// </summary>
        private void ConfigureStatusPieConfig()
        {
            _statusPieConfig = new PieConfig();
            _statusPieConfig.Options = new PieOptions
            {
                Responsive = true,
                MaintainAspectRatio = false,
                Title = new OptionsTitle { Display = true, Text = "สัดส่วนสถานะการทำงาน" }
            };

            var dataset = new PieDataset<int>
            {
                BackgroundColor = new[]
                {
                    ColorUtil.ColorHexString(40, 167, 69), // สีเขียว (Active)
                    ColorUtil.ColorHexString(220, 53, 69)  // สีแดง (Inactive)
                }
            };

            _statusPieConfig.Data.Labels.Clear();
            _statusPieConfig.Data.Labels.Add("กำลังทำงาน");
            _statusPieConfig.Data.Labels.Add("พ้นสภาพ/ลาออก");

            // Logic นับสถานะ: สมมติว่าใช้ฟิลด์ IsActive ใน Model Employee
            // ถ้าพี่ไม่มีฟิลด์นี้ ให้เปลี่ยนเงื่อนไขตาม Database พี่นะครับ
            int activeCount = staffdetails.Count(); // ตัวอย่าง: นับรวมทั้งหมดเป็น Active ไปก่อน
            int inactiveCount = 0;

            dataset.Add(activeCount);
            dataset.Add(inactiveCount);

            _statusPieConfig.Data.Datasets.Clear();
            _statusPieConfig.Data.Datasets.Add(dataset);
        }
    }
}