-- Tạo cơ sở dữ liệu
CREATE DATABASE StudentHealthManagement;

USE StudentHealthManagement;

-- ==============================
-- 1. Bảng Parent (Phụ huynh)
-- ==============================
CREATE TABLE Parent (
    ParentId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(15),
    Email NVARCHAR(100)
);

-- ======================================
-- 2. Bảng UserAccount (Tài khoản người dùng)
-- Có liên kết đến bảng Parent (nếu là phụ huynh)
-- ======================================
CREATE TABLE UserAccount (
    AccountId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Admin', 'Nurse', 'Manager', 'Parent')),
    ParentId INT NULL,
    FOREIGN KEY (ParentId) REFERENCES Parent(ParentId)
);

-- ==============================
-- 3. Bảng Student (Học sinh)
-- ==============================
CREATE TABLE Student (
    StudentId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    BirthDate DATE,
    Gender NVARCHAR(10),
    Class NVARCHAR(20),
    ParentId INT,
    FOREIGN KEY (ParentId) REFERENCES Parent(ParentId)
);

-- ==============================
-- 4. Hồ sơ sức khỏe
-- ==============================
CREATE TABLE HealthRecord (
    RecordId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    Allergy NVARCHAR(500),
    ChronicDisease NVARCHAR(500),
    MedicalHistory NVARCHAR(1000),
    Vision NVARCHAR(100),
    Hearing NVARCHAR(100),
    FOREIGN KEY (StudentId) REFERENCES Student(StudentId)
);

-- ==============================
-- 5. Sự kiện y tế
-- ==============================
CREATE TABLE MedicalEvent (
    EventId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    NurseId INT,
    Date DATETIME NOT NULL,
    Description NVARCHAR(1000),
    TreatmentGiven NVARCHAR(1000),
    FOREIGN KEY (StudentId) REFERENCES Student(StudentId),
    FOREIGN KEY (NurseId) REFERENCES UserAccount(AccountId)
);

-- ==============================
-- 6. Vật tư y tế
-- ==============================
CREATE TABLE MedicalSupply (
    SupplyId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Quantity INT NOT NULL,
    ExpirationDate DATE
);

-- ==============================
-- 7. Thuốc gửi từ phụ huynh
-- ==============================
CREATE TABLE MedicineSent (
    SendId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    ParentId INT NOT NULL,
    MedicineName NVARCHAR(200) NOT NULL,
    Dosage NVARCHAR(200),
    Instruction NVARCHAR(1000),
    FOREIGN KEY (StudentId) REFERENCES Student(StudentId),
    FOREIGN KEY (ParentId) REFERENCES Parent(ParentId)
);

-- ==============================
-- 8. Tiêm chủng
-- ==============================
CREATE TABLE Vaccination (
    VaccinationId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    VaccineName NVARCHAR(100) NOT NULL,
    Date DATE NOT NULL,
    NurseId INT,
    Result NVARCHAR(500),
    FOREIGN KEY (StudentId) REFERENCES Student(StudentId),
    FOREIGN KEY (NurseId) REFERENCES UserAccount(AccountId)
);

-- =======================================
-- 9. Phiếu đồng ý tiêm chủng từ phụ huynh
-- =======================================
CREATE TABLE VaccinationConsentForm (
    FormId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    ParentId INT NOT NULL,
    SentDate DATETIME NOT NULL,
    Confirmed BIT DEFAULT 0,
    FOREIGN KEY (StudentId) REFERENCES Student(StudentId),
    FOREIGN KEY (ParentId) REFERENCES Parent(ParentId)
);

-- ==============================
-- 10. Kiểm tra sức khỏe định kỳ
-- ==============================
CREATE TABLE HealthCheck (
    CheckId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    Date DATE NOT NULL,
    Result NVARCHAR(1000),
    DoctorNotes NVARCHAR(1000),
    FOREIGN KEY (StudentId) REFERENCES Student(StudentId)
);

-- =======================================
-- 11. Phiếu kiểm tra sức khỏe gửi phụ huynh
-- =======================================
CREATE TABLE HealthCheckForm (
    FormId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    ParentId INT NOT NULL,
    SentDate DATETIME NOT NULL,
    Confirmed BIT DEFAULT 0,
    FOREIGN KEY (StudentId) REFERENCES Student(StudentId),
    FOREIGN KEY (ParentId) REFERENCES Parent(ParentId)
);

-- ==============================
-- 12. Blog chia sẻ kiến thức y tế
-- ==============================
CREATE TABLE Blog (
    BlogId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Content NVARCHAR(MAX),
    DatePosted DATETIME NOT NULL DEFAULT GETDATE(),
    AuthorId INT,
    Type INT NOT NULL DEFAULT 1, -- 1 = Medical, 2 = Other
    FOREIGN KEY (AuthorId) REFERENCES UserAccount(AccountId)
);

-- ==============================
-- 13. Chỉ mục để tối ưu truy vấn
-- ==============================
CREATE INDEX IX_Student_ParentId ON Student(ParentId);
CREATE INDEX IX_HealthRecord_StudentId ON HealthRecord(StudentId);
CREATE INDEX IX_MedicalEvent_StudentId ON MedicalEvent(StudentId);
CREATE INDEX IX_MedicalEvent_Date ON MedicalEvent(Date);
CREATE INDEX IX_Vaccination_StudentId ON Vaccination(StudentId);
CREATE INDEX IX_Vaccination_Date ON Vaccination(Date);
CREATE INDEX IX_HealthCheck_StudentId ON HealthCheck(StudentId);
CREATE INDEX IX_HealthCheck_Date ON HealthCheck(Date);
CREATE INDEX IX_MedicineSent_StudentId ON MedicineSent(StudentId);
CREATE INDEX IX_VaccinationConsentForm_StudentId ON VaccinationConsentForm(StudentId);
CREATE INDEX IX_HealthCheckForm_StudentId ON HealthCheckForm(StudentId);
CREATE INDEX IX_Blog_DatePosted ON Blog(DatePosted);
