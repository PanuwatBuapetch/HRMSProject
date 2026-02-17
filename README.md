<!DOCTYPE html>
<html lang="th">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Portfolio: Panuwat Buapetch for 2S Metal</title>
    <script src="https://cdn.tailwindcss.com"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <link href="https://fonts.googleapis.com/css2?family=Sarabun:wght@300;400;600;800&display=swap" rel="stylesheet">
    <style>
        /* Chosen Palette: Professional Steel Blue & Clean White */
        /* Application Structure Plan:
           1. Header: Quick Intro & Target Company Alignment.
           2. Interactive HRMS Demo: Proves technical skills (Localization/Security) via direct interaction instead of static screenshots.
           3. Strategic Solutions: Maps candidate skills to specific 2S Metal benefits (Uniforms, etc.) using interactive cards.
           4. Efficiency Analytics: Chart.js visualization of time-saving potential in manufacturing contexts.
           5. Contact/CTA.
        */
        body {
            font-family: 'Sarabun', sans-serif;
            background-color: #f0f4f8; /* Light neutral background */
            color: #1e293b;
        }
        .glass-panel {
            background: rgba(255, 255, 255, 0.7);
            backdrop-filter: blur(12px);
            -webkit-backdrop-filter: blur(12px);
            border: 1px solid rgba(255, 255, 255, 0.5);
            box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06);
        }
        .accent-text {
            color: #0ea5e9; /* Sky Blue */
        }
        .chart-container {
            position: relative;
            width: 100%;
            max-width: 600px;
            height: 300px;
            max-height: 400px;
            margin: 0 auto;
        }
    </style>
    <!-- Visualization & Content Choices:
         1. Intro: Text + Typography -> Establish Context -> Clear Header.
         2. Tech Demo: Interactive DOM Manipulation -> Goal: Prove "Zero-Refresh" & "Role Guard" -> JS toggles classes/text content -> Confirms skills without needing backend.
         3. Benefits Grid: CSS Grid + JS Hover Effects -> Goal: Show understanding of 2S Metal's needs -> Linking "Uniform" to "Inventory System".
         4. Chart: Chart.js Bar Chart -> Goal: Visualize impact -> Comparing Manual vs Automated tasks -> Canvas rendering (NO SVG).
    -->
    <!-- CONFIRMATION: NO SVG graphics used. NO Mermaid JS used. -->
</head>
<body class="bg-slate-50 text-slate-800 antialiased selection:bg-blue-200 selection:text-blue-900">

    <!-- Navigation / Header -->
    <nav class="sticky top-0 z-50 glass-panel border-b border-white/20">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div class="flex justify-between h-16 items-center">
                <div class="flex flex-col">
                    <span class="font-bold text-xl tracking-tight text-slate-900">PANUWAT BUAPETCH</span>
                    <span class="text-xs text-slate-500 font-medium">CANDIDATE: WEB PROGRAMMER</span>
                </div>
                <div class="hidden md:flex space-x-8">
                    <a href="#overview" class="text-slate-600 hover:text-blue-600 transition-colors text-sm font-semibold">ภาพรวม</a>
                    <a href="#demo" class="text-slate-600 hover:text-blue-600 transition-colors text-sm font-semibold">Live Demo</a>
                    <a href="#solutions" class="text-slate-600 hover:text-blue-600 transition-colors text-sm font-semibold">โซลูชันเพื่อ 2S Metal</a>
                    <a href="#contact" class="px-4 py-2 bg-blue-600 text-white rounded-lg text-sm font-semibold shadow hover:bg-blue-700 transition">ติดต่อ</a>
                </div>
            </div>
        </div>
    </nav>

    <!-- Hero Section -->
    <header id="overview" class="relative pt-16 pb-12 lg:pt-24 lg:pb-20 overflow-hidden">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 relative z-10 text-center">
            <h1 class="text-4xl sm:text-5xl lg:text-6xl font-extrabold tracking-tight text-slate-900 mb-6">
                พัฒนาระบบ <span class="text-blue-600">Enterprise Grade</span><br>
                เพื่ออุตสาหกรรมเหล็กยุคใหม่
            </h1>
            <p class="mt-4 text-xl text-slate-600 max-w-3xl mx-auto leading-relaxed">
                เรียน ท่านคณะกรรมการคัดเลือก <span class="font-bold text-slate-800">บริษัท 2 เอส เมทัล จำกัด (มหาชน)</span><br>
                ผมขอนำเสนอ Portfolio รูปแบบ Interactive Web Application เพื่อแสดงศักยภาพในการพัฒนาระบบ
                <span class="font-semibold text-blue-600">Modern HRMS</span> ที่มีความปลอดภัยสูง รองรับหลายภาษา
                และพร้อมเชื่อมต่อกับระบบคลังสินค้าของท่าน
            </p>
            <div class="mt-10 flex justify-center gap-4">
                <a href="#demo" class="inline-flex items-center justify-center px-8 py-3 border border-transparent text-base font-medium rounded-full text-white bg-blue-600 hover:bg-blue-700 md:text-lg shadow-lg shadow-blue-500/30 transition-all hover:-translate-y-1">
                    ทดลองใช้งานระบบจำลอง
                </a>
                <a href="#solutions" class="inline-flex items-center justify-center px-8 py-3 border border-slate-300 text-base font-medium rounded-full text-slate-700 bg-white hover:bg-slate-50 md:text-lg shadow-sm transition-all hover:-translate-y-1">
                    แนวคิดพัฒนางาน
                </a>
            </div>
        </div>
        <!-- Decorative background elements using CSS (No SVG) -->
        <div class="absolute top-0 left-1/2 w-full -translate-x-1/2 h-full z-0 opacity-30 pointer-events-none">
            <div class="absolute top-20 left-10 w-72 h-72 bg-blue-200 rounded-full mix-blend-multiply filter blur-3xl animate-blob"></div>
            <div class="absolute top-20 right-10 w-72 h-72 bg-sky-200 rounded-full mix-blend-multiply filter blur-3xl animate-blob" style="animation-delay: 2s"></div>
        </div>
    </header>

    <!-- SECTION 1: TECHNICAL DEMO (Interactive) -->
    <section id="demo" class="py-16 bg-white relative">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div class="text-center mb-12">
                <h2 class="text-base text-blue-600 font-bold tracking-wide uppercase">Core Technical Capabilities</h2>
                <p class="mt-2 text-3xl leading-8 font-extrabold tracking-tight text-slate-900 sm:text-4xl">
                    ระบบจำลอง Modern HRMS (Interactive Demo)
                </p>
                <p class="mt-4 max-w-2xl text-xl text-slate-500 mx-auto">
                    พิสูจน์ฟีเจอร์เด่น: <strong>Security Route Guard</strong> และ <strong>Zero-Refresh Localization</strong> ได้จริงที่นี่
                    (ไม่ต้องดูแค่ภาพนิ่ง)
                </p>
            </div>

            <!-- The Interactive App Container -->
            <div class="lg:grid lg:grid-cols-12 lg:gap-8 items-start">
                
                <!-- Controls Panel -->
                <div class="lg:col-span-4 space-y-6">
                    <!-- Control: Role/Security -->
                    <div class="glass-panel p-6 rounded-2xl shadow-sm border border-slate-100">
                        <h3 class="text-lg font-bold text-slate-800 mb-2">1. 🛡️ Security & Role Guard</h3>
                        <p class="text-sm text-slate-600 mb-4">
                            ทดสอบระบบป้องกันข้อมูล เปลี่ยนสิทธิ์ผู้ใช้งานเพื่อดูการตอบสนองของระบบเมื่อเข้าถึงข้อมูลเงินเดือน
                        </p>
                        <div class="flex space-x-2 bg-slate-100 p-1 rounded-lg">
                            <button onclick="setRole('admin')" id="btn-admin" class="flex-1 py-2 text-sm font-medium rounded-md shadow-sm bg-white text-blue-600 transition-all">Admin</button>
                            <button onclick="setRole('user')" id="btn-user" class="flex-1 py-2 text-sm font-medium rounded-md text-slate-500 hover:text-slate-700 transition-all">User</button>
                        </div>
                        <div id="role-status" class="mt-3 text-xs text-center font-mono text-green-600">
                            Current Token: ADMIN_ACCESS_GRANTED
                        </div>
                    </div>

                    <!-- Control: Language -->
                    <div class="glass-panel p-6 rounded-2xl shadow-sm border border-slate-100">
                        <h3 class="text-lg font-bold text-slate-800 mb-2">2. 🌍 Zero-Refresh Localization</h3>
                        <p class="text-sm text-slate-600 mb-4">
                            เทคโนโลยี JsonLocalizationService ช่วยให้สลับภาษาได้ทันทีโดยไม่ต้องโหลดหน้าเว็บใหม่
                        </p>
                        <div class="flex space-x-2 bg-slate-100 p-1 rounded-lg">
                            <button onclick="setLang('th')" id="btn-th" class="flex-1 py-2 text-sm font-medium rounded-md shadow-sm bg-white text-blue-600 transition-all">ไทย (TH)</button>
                            <button onclick="setLang('en')" id="btn-en" class="flex-1 py-2 text-sm font-medium rounded-md text-slate-500 hover:text-slate-700 transition-all">English (EN)</button>
                        </div>
                    </div>

                    <!-- Control: State -->
                    <div class="glass-panel p-6 rounded-2xl shadow-sm border border-slate-100">
                        <h3 class="text-lg font-bold text-slate-800 mb-2">3. 💾 Client-Side State</h3>
                        <p class="text-sm text-slate-600 mb-4">
                            จำลองการเก็บข้อมูลลง Browser LocalStorage เพื่อความลื่นไหล
                        </p>
                        <div class="bg-slate-800 rounded p-3 font-mono text-xs text-green-400 overflow-hidden">
                            > LocalStorage Check:<br>
                            <span id="storage-display">lang: "th", role: "admin"</span>
                        </div>
                    </div>
                </div>

                <!-- Live Mockup Screen -->
                <div class="lg:col-span-8 mt-8 lg:mt-0">
                    <div class="bg-slate-900 rounded-2xl shadow-2xl overflow-hidden border border-slate-700 relative" style="min-height: 500px;">
                        <!-- Mock Browser Bar -->
                        <div class="bg-slate-800 px-4 py-3 flex items-center space-x-2 border-b border-slate-700">
                            <div class="w-3 h-3 rounded-full bg-red-500"></div>
                            <div class="w-3 h-3 rounded-full bg-yellow-500"></div>
                            <div class="w-3 h-3 rounded-full bg-green-500"></div>
                            <div class="ml-4 flex-1 bg-slate-900 rounded px-3 py-1 text-xs text-slate-400 font-mono">
                                https://hrms.2smetal.co.th/dashboard
                            </div>
                        </div>

                        <!-- App Interface -->
                        <div class="flex h-full bg-slate-50">
                            <!-- Sidebar -->
                            <div class="w-64 bg-white border-r border-slate-200 p-4 hidden md:block">
                                <div class="h-8 w-32 bg-blue-600 rounded mb-8 flex items-center justify-center text-white font-bold text-sm">2S METAL HRMS</div>
                                <nav class="space-y-2">
                                    <div class="p-2 bg-blue-50 text-blue-700 rounded font-medium text-sm cursor-pointer nav-item" data-key="dashboard">📊 ภาพรวม</div>
                                    <div class="p-2 text-slate-600 hover:bg-slate-50 rounded font-medium text-sm cursor-pointer nav-item" data-key="profile">👤 ข้อมูลส่วนตัว</div>
                                    <div class="p-2 text-slate-600 hover:bg-slate-50 rounded font-medium text-sm cursor-pointer nav-item" data-key="leave">📅 ยื่นวันลา</div>
                                    
                                    <!-- Admin Only Menu -->
                                    <div id="admin-menu" class="mt-6 pt-6 border-t border-slate-100">
                                        <div class="text-xs font-semibold text-slate-400 mb-2 px-2">ADMIN ZONE</div>
                                        <div class="p-2 text-slate-600 hover:bg-slate-50 rounded font-medium text-sm cursor-pointer nav-item" data-key="salary">💰 จัดการเงินเดือน</div>
                                        <div class="p-2 text-slate-600 hover:bg-slate-50 rounded font-medium text-sm cursor-pointer nav-item" data-key="users">👥 จัดการพนักงาน</div>
                                    </div>
                                </nav>
                            </div>

                            <!-- Main Content Area -->
                            <div class="flex-1 p-8 bg-slate-50/50 overflow-y-auto relative">
                                <!-- Unauthorized Overlay -->
                                <div id="unauthorized-overlay" class="absolute inset-0 bg-slate-900/90 z-20 flex flex-col items-center justify-center text-white hidden backdrop-blur-sm">
                                    <div class="text-6xl mb-4">⛔</div>
                                    <h2 class="text-2xl font-bold mb-2">Access Denied</h2>
                                    <p class="text-slate-400">คุณไม่มีสิทธิ์เข้าถึงส่วนนี้ (Admin Only)</p>
                                    <button onclick="resetToDashboard()" class="mt-6 px-6 py-2 bg-white text-slate-900 rounded font-bold hover:bg-slate-200">กลับสู่หน้าหลัก</button>
                                </div>

                                <!-- Header -->
                                <div class="flex justify-between items-center mb-8">
                                    <h1 class="text-2xl font-bold text-slate-800" id="page-title">แดชบอร์ดภาพรวม</h1>
                                    <div class="flex items-center space-x-3">
                                        <div class="h-8 w-8 rounded-full bg-blue-100 text-blue-600 flex items-center justify-center font-bold text-xs">PB</div>
                                        <span class="text-sm text-slate-600 font-medium" id="user-name">Phanuwat B.</span>
                                    </div>
                                </div>

                                <!-- Content Grid -->
                                <div class="grid grid-cols-1 md:grid-cols-3 gap-6 mb-6">
                                    <!-- Card 1 -->
                                    <div class="bg-white p-6 rounded-xl shadow-sm border border-slate-100">
                                        <div class="text-slate-500 text-xs font-semibold uppercase mb-2" data-key="stat_employee">พนักงานทั้งหมด</div>
                                        <div class="text-3xl font-bold text-slate-800">1,240</div>
                                        <div class="text-green-500 text-xs mt-2 font-medium">Active Status</div>
                                    </div>
                                    <!-- Card 2 -->
                                    <div class="bg-white p-6 rounded-xl shadow-sm border border-slate-100">
                                        <div class="text-slate-500 text-xs font-semibold uppercase mb-2" data-key="stat_location">สาขา</div>
                                        <div class="flex items-center space-x-2">
                                            <span class="px-2 py-1 bg-blue-100 text-blue-700 text-xs rounded font-bold">สงขลา</span>
                                            <span class="px-2 py-1 bg-indigo-100 text-indigo-700 text-xs rounded font-bold">สุราษฎร์ฯ</span>
                                        </div>
                                    </div>
                                    <!-- Card 3 -->
                                    <div class="bg-white p-6 rounded-xl shadow-sm border border-slate-100 relative overflow-hidden group">
                                        <div class="absolute right-0 top-0 p-4 opacity-10 group-hover:opacity-20 transition">
                                            <div class="text-6xl">🏥</div>
                                        </div>
                                        <div class="text-slate-500 text-xs font-semibold uppercase mb-2" data-key="stat_benefit">สิทธิเบิกคงเหลือ</div>
                                        <div class="text-3xl font-bold text-blue-600">100%</div>
                                        <div class="text-slate-400 text-xs mt-2">Annual Checkup</div>
                                    </div>
                                </div>

                                <!-- Chart Area in Demo -->
                                <div class="bg-white p-6 rounded-xl shadow-sm border border-slate-100 h-64 flex items-center justify-center text-slate-400 border-dashed border-2">
                                    <span data-key="chart_placeholder">[ พื้นที่แสดงกราฟสถิติการเข้างาน ]</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- SECTION 2: STRATEGIC SOLUTIONS (Why Me?) -->
    <section id="solutions" class="py-16 bg-slate-50 border-t border-slate-200">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div class="mb-12">
                <h2 class="text-3xl font-extrabold text-slate-900 mb-4">
                    โซลูชันเพื่อสวัสดิการพนักงาน <span class="text-blue-600">2S Metal</span>
                </h2>
                <p class="text-lg text-slate-600 max-w-3xl">
                    จากสวัสดิการที่ยอดเยี่ยมของบริษัท ผมมองเห็นโอกาสในการนำเทคโนโลยี Web Application 
                    มาช่วยจัดการข้อมูลให้เป็นอัตโนมัติ ลดภาระงานเอกสาร และเพิ่มความถูกต้องแม่นยำ
                </p>
            </div>

            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                <!-- Solution Card 1 -->
                <div class="glass-panel p-6 rounded-xl hover:shadow-lg transition-shadow duration-300 border-l-4 border-blue-500 group cursor-default">
                    <div class="flex items-center justify-between mb-4">
                        <h3 class="font-bold text-lg text-slate-800">👕 ชุดยูนิฟอร์ม</h3>
                        <span class="text-2xl group-hover:scale-110 transition-transform">👔</span>
                    </div>
                    <p class="text-sm text-slate-600 mb-4 h-12">การเบิกชุดพนักงานประจำปีและการจัดการสต็อก</p>
                    <div class="bg-blue-50 p-3 rounded-lg">
                        <div class="text-xs font-bold text-blue-700 mb-1 uppercase">My Solution:</div>
                        <p class="text-xs text-blue-900">
                            ระบบบันทึก Size พนักงานรายบุคคล + ตัดสต็อกอัตโนมัติแจ้งเตือนเมื่อของใกล้หมด (Low Stock Alert)
                        </p>
                    </div>
                </div>

                <!-- Solution Card 2 -->
                <div class="glass-panel p-6 rounded-xl hover:shadow-lg transition-shadow duration-300 border-l-4 border-green-500 group cursor-default">
                    <div class="flex items-center justify-between mb-4">
                        <h3 class="font-bold text-lg text-slate-800">🩺 ตรวจสุขภาพประจำปี</h3>
                        <span class="text-2xl group-hover:scale-110 transition-transform">🏥</span>
                    </div>
                    <p class="text-sm text-slate-600 mb-4 h-12">การนัดหมายและการดูผลตรวจย้อนหลัง</p>
                    <div class="bg-green-50 p-3 rounded-lg">
                        <div class="text-xs font-bold text-green-700 mb-1 uppercase">My Solution:</div>
                        <p class="text-xs text-green-900">
                            Web Portal จองคิวตรวจตามโควตา + หน้า Dashboard แสดงผลสุขภาพรายบุคคล (Confidential Data)
                        </p>
                    </div>
                </div>

                <!-- Solution Card 3 -->
                <div class="glass-panel p-6 rounded-xl hover:shadow-lg transition-shadow duration-300 border-l-4 border-pink-500 group cursor-default">
                    <div class="flex items-center justify-between mb-4">
                        <h3 class="font-bold text-lg text-slate-800">🎉 สวัสดิการพิเศษ</h3>
                        <span class="text-2xl group-hover:scale-110 transition-transform">🎁</span>
                    </div>
                    <p class="text-sm text-slate-600 mb-4 h-12">เงินรับขวัญบุตร, งานมงคลสมรส, เยี่ยมไข้</p>
                    <div class="bg-pink-50 p-3 rounded-lg">
                        <div class="text-xs font-bold text-pink-700 mb-1 uppercase">My Solution:</div>
                        <p class="text-xs text-pink-900">
                            ระบบ E-Form ยื่นคำร้องพร้อมแนบหลักฐานรูปถ่ายผ่านมือถือ อนุมัติไวผ่านระบบ Workflow
                        </p>
                    </div>
                </div>
            </div>

            <!-- Efficiency Chart -->
            <div class="mt-16 bg-white rounded-2xl shadow-sm p-8 border border-slate-100">
                <div class="grid grid-cols-1 lg:grid-cols-2 gap-8 items-center">
                    <div>
                        <h3 class="text-2xl font-bold text-slate-900 mb-4">ศักยภาพในการลดระยะเวลาทำงาน</h3>
                        <p class="text-slate-600 mb-6">
                            การเปลี่ยนจากระบบเอกสาร (Manual) มาเป็น Web Application ที่ผมพัฒนา สามารถลดระยะเวลาการทำงานของฝ่าย HR และพนักงานได้อย่างมีนัยสำคัญ
                        </p>
                        <ul class="space-y-3">
                            <li class="flex items-center text-sm text-slate-700">
                                <span class="h-2 w-2 bg-blue-500 rounded-full mr-3"></span>
                                การค้นหาข้อมูลพนักงาน: <strong>เร็วขึ้น 90%</strong>
                            </li>
                            <li class="flex items-center text-sm text-slate-700">
                                <span class="h-2 w-2 bg-blue-500 rounded-full mr-3"></span>
                                การสรุปรายงานประจำเดือน: <strong>Real-time (ทันที)</strong>
                            </li>
                            <li class="flex items-center text-sm text-slate-700">
                                <span class="h-2 w-2 bg-blue-500 rounded-full mr-3"></span>
                                การอนุมัติคำร้อง: <strong>ลดขั้นตอนการเดินเอกสาร</strong>
                            </li>
                        </ul>
                    </div>
                    <div class="chart-container">
                        <canvas id="efficiencyChart"></canvas>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- SECTION 3: CONTACT -->
    <section id="contact" class="py-12 bg-blue-900 text-white">
        <div class="max-w-4xl mx-auto px-4 text-center">
            <h2 class="text-2xl font-bold mb-6">พร้อมร่วมงานกับ 2S Metal ทันที</h2>
            <p class="text-blue-200 mb-8">
                ผมมั่นใจว่าทักษะ .NET / Blazor และความเข้าใจใน Business Logic จะเป็นกำลังสำคัญให้ทีม IT ของท่านได้
            </p>
            <div class="flex flex-col md:flex-row justify-center items-center gap-6">
                <div class="flex items-center space-x-3 bg-blue-800/50 px-6 py-3 rounded-full backdrop-blur-sm border border-blue-700">
                    <span>📞</span>
                    <span class="font-mono">096-8162902</span>
                </div>
                <div class="flex items-center space-x-3 bg-blue-800/50 px-6 py-3 rounded-full backdrop-blur-sm border border-blue-700">
                    <span>📧</span>
                    <span class="font-mono">panuwat.b.2026@email.com</span>
                </div>
            </div>
            <p class="mt-8 text-xs text-blue-400">
                Created by Panuwat Buapetch using Tailwind CSS & Chart.js for 2S Metal Application.
            </p>
        </div>
    </section>

    <!-- JAVASCRIPT LOGIC -->
    <script>
        // --- DATA STORE ---
        const translations = {
            th: {
                dashboard: "📊 ภาพรวม",
                profile: "👤 ข้อมูลส่วนตัว",
                leave: "📅 ยื่นวันลา",
                salary: "💰 จัดการเงินเดือน",
                users: "👥 จัดการพนักงาน",
                page_dashboard: "แดชบอร์ดภาพรวม",
                stat_employee: "พนักงานทั้งหมด",
                stat_location: "สาขา",
                stat_benefit: "สิทธิเบิกคงเหลือ",
                chart_placeholder: "[ พื้นที่แสดงกราฟสถิติการเข้างาน ]"
            },
            en: {
                dashboard: "📊 Dashboard",
                profile: "👤 Profile",
                leave: "📅 Leave Request",
                salary: "💰 Payroll Mgmt",
                users: "👥 User Mgmt",
                page_dashboard: "Overview Dashboard",
                stat_employee: "Total Employees",
                stat_location: "Branches",
                stat_benefit: "Remaining Benefits",
                chart_placeholder: "[ Attendance Statistics Area ]"
            }
        };

        // --- STATE MANAGEMENT ---
        let appState = {
            role: 'admin', // 'admin' or 'user'
            lang: 'th'     // 'th' or 'en'
        };

        // --- CORE FUNCTIONS ---

        function init() {
            // Check LocalStorage on load (Simulating Client-Side Persistence)
            const savedRole = localStorage.getItem('2s_demo_role');
            const savedLang = localStorage.getItem('2s_demo_lang');
            
            if(savedRole) appState.role = savedRole;
            if(savedLang) appState.lang = savedLang;

            updateUI();
            renderChart();
        }

        function setRole(role) {
            appState.role = role;
            localStorage.setItem('2s_demo_role', role);
            updateUI();
            
            // Visual feedback for route guard simulation
            if (role === 'user') {
                checkRouteGuard();
            } else {
                document.getElementById('unauthorized-overlay').classList.add('hidden');
            }
        }

        function setLang(lang) {
            appState.lang = lang;
            localStorage.setItem('2s_demo_lang', lang);
            updateUI();
        }

        function updateUI() {
            // Update Buttons Styling
            const btnAdmin = document.getElementById('btn-admin');
            const btnUser = document.getElementById('btn-user');
            const btnTh = document.getElementById('btn-th');
            const btnEn = document.getElementById('btn-en');

            // Reset Classes
            [btnAdmin, btnUser, btnTh, btnEn].forEach(btn => {
                btn.className = "flex-1 py-2 text-sm font-medium rounded-md text-slate-500 hover:text-slate-700 transition-all";
            });

            // Active Classes
            const activeClass = "flex-1 py-2 text-sm font-medium rounded-md shadow-sm bg-white text-blue-600 transition-all ring-1 ring-black/5";
            
            if(appState.role === 'admin') btnAdmin.className = activeClass;
            else btnUser.className = activeClass;

            if(appState.lang === 'th') btnTh.className = activeClass;
            else btnEn.className = activeClass;

            // Update Text Content (Localization)
            const t = translations[appState.lang];
            document.querySelectorAll('[data-key]').forEach(el => {
                const key = el.getAttribute('data-key');
                if(t[key]) el.textContent = t[key];
            });

            // Update Visibility based on Role
            const adminMenu = document.getElementById('admin-menu');
            const roleStatus = document.getElementById('role-status');
            
            if(appState.role === 'admin') {
                adminMenu.classList.remove('hidden');
                roleStatus.textContent = "Current Token: ADMIN_ACCESS_GRANTED";
                roleStatus.className = "mt-3 text-xs text-center font-mono text-green-600";
            } else {
                adminMenu.classList.add('hidden');
                roleStatus.textContent = "Current Token: USER_ACCESS_LIMITED";
                roleStatus.className = "mt-3 text-xs text-center font-mono text-amber-600";
            }

            // Update LocalStorage Display
            document.getElementById('storage-display').textContent = `lang: "${appState.lang}", role: "${appState.role}"`;
        }

        function checkRouteGuard() {
            // Simulate user clicking an admin link while in User mode
            // For demo purposes, we don't automatically trigger this, but let's assume
            // if they were on a restricted page, it would trigger.
        }

        // Simulating the Route Guard Event
        // Add click listeners to simulated nav items
        document.addEventListener('DOMContentLoaded', () => {
            init();

            const navItems = document.querySelectorAll('.nav-item');
            navItems.forEach(item => {
                item.addEventListener('click', (e) => {
                    const key = e.target.getAttribute('data-key');
                    
                    // Simulate routing
                    if (['salary', 'users'].includes(key)) {
                        // Restricted Routes
                        if (appState.role !== 'admin') {
                            // Trigger Guard
                            const overlay = document.getElementById('unauthorized-overlay');
                            overlay.classList.remove('hidden');
                        } else {
                            // Admin Access Allowed - Just visual update
                            alert("Admin Access Granted: Navigating to " + key);
                        }
                    } else {
                        // Public Routes
                        document.querySelectorAll('.nav-item').forEach(n => n.classList.remove('bg-blue-50', 'text-blue-700'));
                        e.target.classList.add('bg-blue-50', 'text-blue-700');
                    }
                });
            });
        });

        function resetToDashboard() {
            document.getElementById('unauthorized-overlay').classList.add('hidden');
        }

        // --- CHART VISUALIZATION ---
        function renderChart() {
            const ctx = document.getElementById('efficiencyChart').getContext('2d');
            
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ['ยื่นเอกสาร', 'สรุปเวลาเข้างาน', 'ตรวจสอบสิทธิ์', 'ค้นหาประวัติ'],
                    datasets: [
                        {
                            label: 'ระบบเดิม (นาที)',
                            data: [15, 120, 10, 20],
                            backgroundColor: '#e2e8f0', // Slate 200
                            borderRadius: 4,
                            barPercentage: 0.6
                        },
                        {
                            label: 'Modern HRMS (นาที)',
                            data: [2, 1, 1, 1], // Drastic reduction
                            backgroundColor: '#2563eb', // Blue 600
                            borderRadius: 4,
                            barPercentage: 0.6
                        }
                    ]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'bottom'
                        },
                        tooltip: {
                            callbacks: {
                                label: function(context) {
                                    return context.dataset.label + ': ' + context.raw + ' นาที';
                                }
                            }
                        }
                    },
                    scales: {
                        y: {
                            beginAtZero: true,
                            title: {
                                display: true,
                                text: 'ระยะเวลาดำเนินการ (นาที)'
                            }
                        }
                    }
                }
            });
        }
    </script>
</body>
</html>
