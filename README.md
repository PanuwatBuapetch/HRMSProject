# 🏢 Modern HRMS Management System (Demo)

**Project Status:** 🟢 Active / Demo Version  
**Developer:** ภานุวัฒน์ บัวเพชร (Panuwat Buapetch)  
**Target Industry:** Manufacturing & Distribution (Optimized for 2S Metal)

---

## 🚀 Overview
ระบบบริหารจัดการทรัพยากรบุคคล (HRMS) ที่ถูกพัฒนาด้วยเทคโนโลยี **Blazor Web App (.NET 8)** เพื่อพิสูจน์แนวคิดการทำระบบ Enterprise Application ที่มีความปลอดภัยสูง ใช้งานง่าย และรองรับการขยายตัวขององค์กรในอนาคต 

---

## 💎 Key Technical Highlights

### 🛡️ 1. Enterprise Security & RBAC
- **Authentication & Authorization:** ระบบแยกสิทธิ์ผู้ใช้งานชัดเจน (Admin / User)
- **Route Guard Logic:** ระบบจะทำการตรวจเช็คสิทธิ์การเข้าถึงหน้าเว็บ (Authorization) ทุกครั้งที่มีการเปลี่ยนหน้า หากพยายามเข้าถึงข้อมูลโดยไม่ได้รับอนุญาต ระบบจะ Redirect ดีดกลับสู่หน้า Login ทันที

### 🌍 2. Global Readiness (Multi-language)
- **Zero-Refresh Localization:** พัฒนา `JsonLocalizationService` เพื่อให้ผู้ใช้สลับภาษา (ไทย-อังกฤษ) ได้ทันทีโดยไม่ต้องโหลดหน้าเว็บใหม่
- **Dynamic Content:** ข้อมูลคำแปลถูกจัดการผ่านไฟล์ JSON ทำให้ง่ายต่อการแก้ไขและเพิ่มภาษาอื่นๆ ในอนาคต

### 💾 3. State Persistence & Performance
- **Client-Side Storage:** ใช้ Browser LocalStorage ในการจดจำสถานะการล็อกอินและภาษาที่เลือกไว้ ทำให้ระบบจดจำผู้ใช้ได้แม้จะปิดเบราว์เซอร์
- **Optimization:** ออกแบบมาเพื่อลดภาระการทำงานของ Server และรองรับการใช้งานผ่านอินเทอร์เน็ตความเร็วต่ำที่ไซต์งาน

---

## 🛠️ Tech Stack
| Category | Technology |
| :--- | :--- |
| **Framework** | .NET 8 (Blazor Web App) |
| **UI Library** | Ant Design Blazor |
| **Notifications** | SweetAlert2 (CurrieTechnologies) |
| **Architecture** | Partial Classes (Code-Behind Pattern) |
| **Icons** | Bootstrap Icons |

---

## 📸 Screenshots
หน้ารายงาน Dashboard
<img width="2483" height="1274" alt="image" src="https://github.com/user-attachments/assets/b4611393-1578-437c-8a18-f94288560148" />

หน้าต่างจัดการข้อมูลบุคลากร Employee 
<img width="2506" height="1280" alt="image" src="https://github.com/user-attachments/assets/0b507ea4-41fc-4721-8076-80fb99d90bb5" />

หน้า Loginเข้าสู่ระบบ HRMS
<img width="2486" height="1278" alt="image" src="https://github.com/user-attachments/assets/39a4e1bb-53d6-42df-9710-a0ce2789c5cd" />

---

## 🏃 Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (v17.8+) หรือ VS Code

### Installation
1. Clone the repository
   ```bash
   git clone [https://github.com/your-username/HRMSProject.git](https://github.com/your-username/HRMSProject.git)
