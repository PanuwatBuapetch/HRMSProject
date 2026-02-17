--
-- PostgreSQL database dump
--

-- Dumped from database version 17.4
-- Dumped by pg_dump version 17.4

-- Started on 2026-02-17 11:31:56

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 7 (class 2615 OID 27726)
-- Name: person; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA person;


ALTER SCHEMA person OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 232 (class 1259 OID 27900)
-- Name: AuditLog; Type: TABLE; Schema: person; Owner: postgres
--

CREATE TABLE person."AuditLog" (
    logid integer NOT NULL,
    studentid integer NOT NULL,
    action character varying(50) NOT NULL,
    actionby character varying(50) NOT NULL,
    actionat timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE person."AuditLog" OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 27740)
-- Name: department; Type: TABLE; Schema: person; Owner: postgres
--

CREATE TABLE person.department (
    dept_id character varying(20) NOT NULL,
    location_id character varying(20),
    division_id character varying(20),
    dept_desc character varying(200),
    dept_name_eng character varying(200),
    dept_name_thai character varying(200),
    isactive character varying(10),
    mission_id character varying(10)
);


ALTER TABLE person.department OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 27735)
-- Name: division; Type: TABLE; Schema: person; Owner: postgres
--

CREATE TABLE person.division (
    division_id character varying(20) NOT NULL,
    location_id character varying(20),
    central_id character varying(10),
    division_desc character varying(60),
    division_name_eng character varying(200),
    division_name_thai character varying(200),
    isactive character varying(10),
    mission_id character varying(10)
);


ALTER TABLE person.division OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 27784)
-- Name: employee; Type: TABLE; Schema: person; Owner: postgres
--

CREATE TABLE person.employee (
    employee_id character varying(200) NOT NULL,
    first_name_thai character varying(200),
    first_name_eng character varying(200),
    last_name_thai character varying(200),
    last_name_eng character varying(200),
    full_name_thai character varying(401),
    full_name_eng character varying(401),
    title_id character varying(50),
    position_id character varying(20),
    location_id character varying(20),
    division_id character varying(20),
    dept_id character varying(20),
    team_id character varying(50),
    unit_id character varying(20),
    picture_url text,
    employment_status character varying(50),
    email character varying(100),
    end_date character varying(20),
    citizen_id character varying(13),
    termination_date character varying(20),
    start_date character varying(20),
    username character varying(100),
    password character varying(400),
    pincode character varying(400),
    secret_code numeric(6,0),
    birth_date date,
    gender character varying(10),
    nationality character varying(50),
    religion character varying(50),
    military_status character varying(20),
    emergency_contact_name character varying(200),
    emergency_contact_phone character varying(50),
    emergency_contact_relation character varying(50),
    current_address text,
    permanent_address text,
    bank_name character varying(100),
    bank_account_no character varying(50),
    social_security_no character varying(20),
    tax_id character varying(20),
    manager_id character varying(200)
);


ALTER TABLE person.employee OWNER TO postgres;

--
-- TOC entry 234 (class 1259 OID 28238)
-- Name: employee_document; Type: TABLE; Schema: person; Owner: postgres
--

CREATE TABLE person.employee_document (
    document_id uuid DEFAULT gen_random_uuid() NOT NULL,
    employee_id character varying(200) NOT NULL,
    document_type character varying(50),
    file_name character varying(255),
    file_url text,
    uploaded_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE person.employee_document OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 27796)
-- Name: employee_session; Type: TABLE; Schema: person; Owner: postgres
--

CREATE TABLE person.employee_session (
    id uuid DEFAULT gen_random_uuid() NOT NULL,
    date_created timestamp without time zone,
    date_expired timestamp without time zone,
    ip text,
    employee_id character varying(200)
);


ALTER TABLE person.employee_session OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 27757)
-- Name: employee_title; Type: TABLE; Schema: person; Owner: postgres
--

CREATE TABLE person.employee_title (
    title_id character varying(2) NOT NULL,
    title_name_eng character varying(200),
    title_name_thai character varying(200),
    title_short_eng character varying(200),
    title_short_thai character varying(200)
);


ALTER TABLE person.employee_title OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 27764)
-- Name: job_position; Type: TABLE; Schema: person; Owner: postgres
--

CREATE TABLE person.job_position (
    position_id character varying(20) NOT NULL,
    position_name_eng character varying(400),
    position_name_thai character varying(400),
    position_type character varying(2)
);


ALTER TABLE person.job_position OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 27728)
-- Name: location; Type: TABLE; Schema: person; Owner: postgres
--

CREATE TABLE person.location (
    location_id character varying(20) NOT NULL,
    location_address character varying(2000),
    location_desc character varying(2000),
    location_name_eng character varying(200),
    location_name_thai character varying(200)
);


ALTER TABLE person.location OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 27791)
-- Name: management; Type: TABLE; Schema: person; Owner: postgres
--

CREATE TABLE person.management (
    management_id character varying(400) NOT NULL,
    employee_id character varying(200),
    management_position_id character varying(50),
    temp_admin_code character varying(20),
    isactive character varying(1),
    location_id character varying(50),
    division_id character varying(50),
    dept_id character varying(50),
    team_id character varying(50),
    unit_id character varying(50)
);


ALTER TABLE person.management OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 27771)
-- Name: management_position; Type: TABLE; Schema: person; Owner: postgres
--

CREATE TABLE person.management_position (
    management_position_id character varying(2) NOT NULL,
    position_level integer DEFAULT 1,
    position_name_eng character varying(400),
    position_name_thai character varying(400)
);


ALTER TABLE person.management_position OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 27779)
-- Name: mission; Type: TABLE; Schema: person; Owner: postgres
--

CREATE TABLE person.mission (
    mission_id character varying(10) NOT NULL,
    mission_name_eng character varying(200),
    mission_name_thai character varying(200)
);


ALTER TABLE person.mission OWNER TO postgres;

--
-- TOC entry 231 (class 1259 OID 27899)
-- Name: studentlog_logid_seq; Type: SEQUENCE; Schema: person; Owner: postgres
--

CREATE SEQUENCE person.studentlog_logid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE person.studentlog_logid_seq OWNER TO postgres;

--
-- TOC entry 5015 (class 0 OID 0)
-- Dependencies: 231
-- Name: studentlog_logid_seq; Type: SEQUENCE OWNED BY; Schema: person; Owner: postgres
--

ALTER SEQUENCE person.studentlog_logid_seq OWNED BY person."AuditLog".logid;


--
-- TOC entry 222 (class 1259 OID 27747)
-- Name: team; Type: TABLE; Schema: person; Owner: postgres
--

CREATE TABLE person.team (
    team_id character varying(3) NOT NULL,
    location_id character varying(2),
    division_id character varying(2),
    dept_id character varying(3),
    team_name_eng character varying(200),
    team_name_thai character varying(200),
    isactive character varying(1),
    mission_id character varying(10)
);


ALTER TABLE person.team OWNER TO postgres;

--
-- TOC entry 233 (class 1259 OID 28233)
-- Name: v_employee_details; Type: VIEW; Schema: person; Owner: postgres
--

CREATE VIEW person.v_employee_details AS
 SELECT e.employee_id AS "EmployeeId",
    e.first_name_thai AS "FirstNameThai",
    e.last_name_thai AS "LastNameThai",
    e.first_name_eng AS "FirstNameEng",
    e.last_name_eng AS "LastNameEng",
    e.full_name_thai AS "FullNameThai",
    e.full_name_eng AS "FullNameEng",
    e.title_id AS "TitleId",
    e.position_id AS "PositionId",
    e.location_id AS "LocationId",
    e.division_id AS "DivisionId",
    e.dept_id AS "DeptId",
    e.team_id AS "TeamId",
    e.unit_id AS "UnitId",
    e.picture_url AS "PictureUrl",
    e.employment_status AS "EmploymentStatus",
    e.email AS "Email",
    e.citizen_id AS "CitizenId",
    e.start_date AS "StartDate",
    e.end_date AS "EndDate",
    e.termination_date AS "TerminationDate",
    d.dept_name_thai AS "DeptNameThai",
    d.dept_name_eng AS "DeptNameEng"
   FROM (person.employee e
     LEFT JOIN person.department d ON (((e.dept_id)::text = (d.dept_id)::text)));


ALTER VIEW person.v_employee_details OWNER TO postgres;

--
-- TOC entry 235 (class 1259 OID 28247)
-- Name: v_management_details; Type: VIEW; Schema: person; Owner: postgres
--

CREATE VIEW person.v_management_details AS
 SELECT m.management_id AS "Key",
    e.employee_id AS "StaffId",
    (((COALESCE(e.first_name_thai, ''::character varying))::text || ' '::text) || (COALESCE(e.last_name_thai, ''::character varying))::text) AS "StaffNameThai",
    mp.position_name_thai AS "AdminNameThai",
    d.division_name_thai AS "DivisionFull",
    m.isactive AS "Isactive"
   FROM (((person.management m
     LEFT JOIN person.employee e ON (((m.employee_id)::text = (e.employee_id)::text)))
     LEFT JOIN person.management_position mp ON (((m.management_position_id)::text = (mp.management_position_id)::text)))
     LEFT JOIN person.division d ON (((m.division_id)::text = (d.division_id)::text)));


ALTER VIEW person.v_management_details OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 27752)
-- Name: work_unit; Type: TABLE; Schema: person; Owner: postgres
--

CREATE TABLE person.work_unit (
    unit_id character varying(20) NOT NULL,
    location_id character varying(2),
    division_id character varying(20),
    dept_id character varying(20),
    team_id character varying(3),
    unit_name_eng character varying(70),
    unit_name_thai character varying(70),
    isactive character varying(10),
    mission_id character varying(10)
);


ALTER TABLE person.work_unit OWNER TO postgres;

--
-- TOC entry 4816 (class 2604 OID 27903)
-- Name: AuditLog logid; Type: DEFAULT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person."AuditLog" ALTER COLUMN logid SET DEFAULT nextval('person.studentlog_logid_seq'::regclass);


--
-- TOC entry 5008 (class 0 OID 27900)
-- Dependencies: 232
-- Data for Name: AuditLog; Type: TABLE DATA; Schema: person; Owner: postgres
--



--
-- TOC entry 4997 (class 0 OID 27740)
-- Dependencies: 221
-- Data for Name: department; Type: TABLE DATA; Schema: person; Owner: postgres
--

INSERT INTO person.department VALUES ('HRM', NULL, 'AD', NULL, NULL, 'กองการเจ้าหน้าที่', NULL, NULL);
INSERT INTO person.department VALUES ('CSD', NULL, 'SC', NULL, NULL, 'ภาควิชาวิทยาการคอมฯ', NULL, NULL);
INSERT INTO person.department VALUES ('FIN', NULL, 'AD', NULL, NULL, 'กองคลังในมหาลัย', NULL, NULL);
INSERT INTO person.department VALUES ('IT001', NULL, 'D003', NULL, 'IT', 'สารสนเทศ', '0', NULL);
INSERT INTO person.department VALUES ('DEPT-HR-01', 'LOC-01', 'DIV-ADMIN', 'ดูแลการสรรหาและพัฒนาบุคลากร', 'Human Resource Management', 'ฝ่ายบริหารทรัพยากรบุคคล', '0', 'MIS-2026');
INSERT INTO person.department VALUES ('IT002', NULL, 'D003', NULL, NULL, 'พื้นฐาน', '0', NULL);
INSERT INTO person.department VALUES ('DIIS001', NULL, 'D003', NULL, NULL, 'บริหารจัดการ', '1', NULL);
INSERT INTO person.department VALUES ('DIIS002', NULL, 'D003', NULL, NULL, 'โครงสร้างพื้นฐาน', '1', NULL);
INSERT INTO person.department VALUES ('TEST', NULL, 'D003', NULL, NULL, 'AAA', '1', NULL);
INSERT INTO person.department VALUES ('TEST2', NULL, 'D003', NULL, NULL, 'BBB', '1', NULL);
INSERT INTO person.department VALUES ('CCC', NULL, 'D003', NULL, NULL, 'CCCC', '1', NULL);
INSERT INTO person.department VALUES ('DDD', NULL, 'D003', NULL, NULL, 'DDD', '1', NULL);
INSERT INTO person.department VALUES ('DDGG', NULL, 'D003', NULL, NULL, 'DGDG', '1', NULL);


--
-- TOC entry 4996 (class 0 OID 27735)
-- Dependencies: 220
-- Data for Name: division; Type: TABLE DATA; Schema: person; Owner: postgres
--

INSERT INTO person.division VALUES ('AD', NULL, NULL, NULL, NULL, 'สำนักงานอธิการบดี', NULL, NULL);
INSERT INTO person.division VALUES ('SC', NULL, NULL, NULL, NULL, 'คณะวิทยาศาสตร์', NULL, NULL);
INSERT INTO person.division VALUES ('DIV-ADMIN', 'LOC-01', 'CEN-MAIN', 'หน่วยงานดูแลภาพรวมการบริหารและทรัพยากรบุคคล', 'Central Administration Bureau', 'สำนักบริหารงานกลาง', '1', 'MIS-2026');
INSERT INTO person.division VALUES ('D003', NULL, NULL, NULL, 'PSU DIIS', 'สำนักนวัตกรรม', '1', NULL);
INSERT INTO person.division VALUES ('A003', NULL, NULL, NULL, 'NAAA', 'หน่วยงานหลัก', '1', NULL);
INSERT INTO person.division VALUES ('SKL01', 'L1', 'C001', 'หน่วยงานกำหนดทิศทางและเป้าหมายองค์กร', 'Strategy and Planning Division', 'สำนักยุทธศาสตร์และแผนงาน', '1', 'M-001');


--
-- TOC entry 5004 (class 0 OID 27784)
-- Dependencies: 228
-- Data for Name: employee; Type: TABLE DATA; Schema: person; Owner: postgres
--



--
-- TOC entry 5009 (class 0 OID 28238)
-- Dependencies: 234
-- Data for Name: employee_document; Type: TABLE DATA; Schema: person; Owner: postgres
--



--
-- TOC entry 5006 (class 0 OID 27796)
-- Dependencies: 230
-- Data for Name: employee_session; Type: TABLE DATA; Schema: person; Owner: postgres
--



--
-- TOC entry 5000 (class 0 OID 27757)
-- Dependencies: 224
-- Data for Name: employee_title; Type: TABLE DATA; Schema: person; Owner: postgres
--

INSERT INTO person.employee_title VALUES ('01', 'Mr.', 'นาย', NULL, NULL);
INSERT INTO person.employee_title VALUES ('02', 'Mrs.', 'นาง', NULL, NULL);
INSERT INTO person.employee_title VALUES ('03', 'Ms.', 'นางสาว', NULL, NULL);
INSERT INTO person.employee_title VALUES ('04', 'Dr.', 'ดร.', NULL, NULL);


--
-- TOC entry 5001 (class 0 OID 27764)
-- Dependencies: 225
-- Data for Name: job_position; Type: TABLE DATA; Schema: person; Owner: postgres
--

INSERT INTO person.job_position VALUES ('J001', NULL, 'อาจารย์', 'AC');
INSERT INTO person.job_position VALUES ('A001', NULL, 'เจ้าหน้าที่บริหาร', 'AD');
INSERT INTO person.job_position VALUES ('T001', NULL, 'พนักงานธุรการ', 'OF');


--
-- TOC entry 4995 (class 0 OID 27728)
-- Dependencies: 219
-- Data for Name: location; Type: TABLE DATA; Schema: person; Owner: postgres
--

INSERT INTO person.location VALUES ('H1', NULL, NULL, NULL, 'หาดใหญ่');
INSERT INTO person.location VALUES ('P1', NULL, NULL, NULL, 'ปัตตานี');
INSERT INTO person.location VALUES ('B1', NULL, NULL, NULL, 'ภูเก็ต');
INSERT INTO person.location VALUES ('S1', NULL, NULL, NULL, 'สุราษฎร์ธานี');


--
-- TOC entry 5005 (class 0 OID 27791)
-- Dependencies: 229
-- Data for Name: management; Type: TABLE DATA; Schema: person; Owner: postgres
--



--
-- TOC entry 5002 (class 0 OID 27771)
-- Dependencies: 226
-- Data for Name: management_position; Type: TABLE DATA; Schema: person; Owner: postgres
--



--
-- TOC entry 5003 (class 0 OID 27779)
-- Dependencies: 227
-- Data for Name: mission; Type: TABLE DATA; Schema: person; Owner: postgres
--



--
-- TOC entry 4998 (class 0 OID 27747)
-- Dependencies: 222
-- Data for Name: team; Type: TABLE DATA; Schema: person; Owner: postgres
--

INSERT INTO person.team VALUES ('REC', NULL, NULL, 'HRM', NULL, 'ทีมสรรหา', NULL, NULL);
INSERT INTO person.team VALUES ('ACC', NULL, NULL, 'FIN', NULL, 'ทีมบัญชี', NULL, NULL);


--
-- TOC entry 4999 (class 0 OID 27752)
-- Dependencies: 223
-- Data for Name: work_unit; Type: TABLE DATA; Schema: person; Owner: postgres
--

INSERT INTO person.work_unit VALUES ('P01', 'H1', 'AD', 'HRM', 'REC', NULL, 'หน่วยงานวิจัย 1', 'Y', NULL);
INSERT INTO person.work_unit VALUES ('P02', 'H1', 'AD', 'FNC', 'ACC', NULL, 'หน่วยงานธุรการ', 'Y', NULL);
INSERT INTO person.work_unit VALUES ('DSS-001', NULL, NULL, 'DIIS001', NULL, NULL, 'ระบบจัดการข้อมูล', '1', NULL);
INSERT INTO person.work_unit VALUES ('SC-001', NULL, NULL, 'DIIS002', NULL, NULL, 'ระบบความปลอดภัยของไมโครชอฟต์', '1', NULL);


--
-- TOC entry 5016 (class 0 OID 0)
-- Dependencies: 231
-- Name: studentlog_logid_seq; Type: SEQUENCE SET; Schema: person; Owner: postgres
--

SELECT pg_catalog.setval('person.studentlog_logid_seq', 1, false);


--
-- TOC entry 4825 (class 2606 OID 28204)
-- Name: department department_pkey; Type: CONSTRAINT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person.department
    ADD CONSTRAINT department_pkey PRIMARY KEY (dept_id);


--
-- TOC entry 4823 (class 2606 OID 28208)
-- Name: division division_pkey; Type: CONSTRAINT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person.division
    ADD CONSTRAINT division_pkey PRIMARY KEY (division_id);


--
-- TOC entry 4847 (class 2606 OID 28246)
-- Name: employee_document employee_document_pkey; Type: CONSTRAINT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person.employee_document
    ADD CONSTRAINT employee_document_pkey PRIMARY KEY (document_id);


--
-- TOC entry 4839 (class 2606 OID 27846)
-- Name: employee employee_pkey; Type: CONSTRAINT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person.employee
    ADD CONSTRAINT employee_pkey PRIMARY KEY (employee_id);


--
-- TOC entry 4843 (class 2606 OID 27803)
-- Name: employee_session employee_session_pkey; Type: CONSTRAINT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person.employee_session
    ADD CONSTRAINT employee_session_pkey PRIMARY KEY (id);


--
-- TOC entry 4831 (class 2606 OID 27763)
-- Name: employee_title employee_title_pkey; Type: CONSTRAINT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person.employee_title
    ADD CONSTRAINT employee_title_pkey PRIMARY KEY (title_id);


--
-- TOC entry 4833 (class 2606 OID 28206)
-- Name: job_position job_position_pkey; Type: CONSTRAINT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person.job_position
    ADD CONSTRAINT job_position_pkey PRIMARY KEY (position_id);


--
-- TOC entry 4821 (class 2606 OID 28178)
-- Name: location location_pkey; Type: CONSTRAINT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person.location
    ADD CONSTRAINT location_pkey PRIMARY KEY (location_id);


--
-- TOC entry 4841 (class 2606 OID 27795)
-- Name: management management_pkey; Type: CONSTRAINT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person.management
    ADD CONSTRAINT management_pkey PRIMARY KEY (management_id);


--
-- TOC entry 4835 (class 2606 OID 27778)
-- Name: management_position management_position_pkey; Type: CONSTRAINT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person.management_position
    ADD CONSTRAINT management_position_pkey PRIMARY KEY (management_position_id);


--
-- TOC entry 4837 (class 2606 OID 27783)
-- Name: mission mission_pkey; Type: CONSTRAINT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person.mission
    ADD CONSTRAINT mission_pkey PRIMARY KEY (mission_id);


--
-- TOC entry 4845 (class 2606 OID 27906)
-- Name: AuditLog studentlog_pkey; Type: CONSTRAINT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person."AuditLog"
    ADD CONSTRAINT studentlog_pkey PRIMARY KEY (logid);


--
-- TOC entry 4827 (class 2606 OID 27751)
-- Name: team team_pkey; Type: CONSTRAINT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person.team
    ADD CONSTRAINT team_pkey PRIMARY KEY (team_id);


--
-- TOC entry 4829 (class 2606 OID 28210)
-- Name: work_unit work_unit_pkey; Type: CONSTRAINT; Schema: person; Owner: postgres
--

ALTER TABLE ONLY person.work_unit
    ADD CONSTRAINT work_unit_pkey PRIMARY KEY (unit_id);


-- Completed on 2026-02-17 11:31:57

--
-- PostgreSQL database dump complete
--

