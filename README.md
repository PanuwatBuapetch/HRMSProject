
```markdown
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
| **Database** | PostgreSQL 17 (pgAdmin 4) |
| **UI Library** | Ant Design Blazor |
| **Notifications** | SweetAlert2 (CurrieTechnologies) |
| **Architecture** | Partial Classes (Code-Behind Pattern) |
| **Icons** | Bootstrap Icons |

---

## 🗄️ Database Setup (PostgreSQL)
เพื่อให้ระบบทำงานได้สมบูรณ์ โปรดตั้งค่าฐานข้อมูลตามขั้นตอนดังนี้:

### 1. การเตรียมฐานข้อมูล
- เปิด **pgAdmin 4** หรือ Tool ที่ท่านใช้งาน
- สร้าง Database ใหม่ชื่อ: `HRMS_Project`

### 2. การลงข้อมูล (SQL Script)
- นำไฟล์ SQL ที่อยู่ในโปรเจกต์ไปรันใน **Query Tool**:
- 📄 [ดาวน์โหลด/ดูไฟล์ Database_HRMS.sql](./Database_HRMS.sql)

### 3. การเชื่อมต่อ (Connection String)
- แก้ไขไฟล์ `appsettings.json` ในโปรเจกต์ให้ตรงกับเครื่องของท่าน:
```json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost; Port=5432; Database=HRMS_Project; Username=postgres; Password=YOUR_PASSWORD"
}

```

---

## 🏃 Getting Started

### Prerequisites

* [.NET 8.0 SDK]()
* [PostgreSQL 17]()
* Visual Studio 2022 (v17.8+) หรือ VS Code

### Installation

1. Clone the repository
```bash
git clone https://github.com/PanuwatBuapetch/HRMSProject.git

```


2. Restore NuGet Packages
```bash
dotnet restore

```


3. Run the application
```bash
dotnet watch run

```



---

## 🔑 ข้อมูลเข้าใช้งาน

| Role | Username | Password |
| --- | --- | --- |
| **Admin** | superadmin | super123 |


---

## 📞 Contact

* **ชื่อ:** ภานุวัฒน์ บัวเพชร (ภูมิ)
* **เบอร์โทร:** 096-816-2902
* **อีเมล:** panuwat.b.2026@gmail.com

