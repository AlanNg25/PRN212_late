-- Tạo cơ sở dữ liệu
CREATE DATABASE StudentHealthManagement;
GO

USE StudentHealthManagement;
GO

-- Tạo bảng Parent (Phụ huynh)
CREATE TABLE Parent (
    ParentId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(15),
    Email NVARCHAR(100)
);

-- Tạo bảng Student (Học sinh)
CREATE TABLE Student (
    StudentId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    BirthDate DATE,
    Gender NVARCHAR(10),
    Class NVARCHAR(20),
    ParentId INT,
    FOREIGN KEY (ParentId) REFERENCES Parent(ParentId)
);

-- Tạo bảng UserAccount (Tài khoản người dùng)
CREATE TABLE UserAccount (
    AccountId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Admin', 'Nurse', 'Manager', 'Parent'))
);

-- Tạo bảng HealthRecord (Hồ sơ sức khỏe)
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

-- Tạo bảng MedicalEvent (Sự kiện y tế)
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

-- Tạo bảng MedicalSupply (Vật tư y tế)
CREATE TABLE MedicalSupply (
    SupplyId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Quantity INT NOT NULL,
    ExpirationDate DATE
);

-- Tạo bảng MedicineSent (Thuốc gửi)
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

-- Tạo bảng Vaccination (Tiêm chủng)
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

-- Tạo bảng VaccinationConsentForm (Phiếu đồng ý tiêm chủng)
CREATE TABLE VaccinationConsentForm (
    FormId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    ParentId INT NOT NULL,
    SentDate DATETIME NOT NULL,
    Confirmed BIT DEFAULT 0,
    FOREIGN KEY (StudentId) REFERENCES Student(StudentId),
    FOREIGN KEY (ParentId) REFERENCES Parent(ParentId)
);

-- Tạo bảng HealthCheck (Kiểm tra sức khỏe)
CREATE TABLE HealthCheck (
    CheckId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    Date DATE NOT NULL,
    Result NVARCHAR(1000),
    DoctorNotes NVARCHAR(1000),
    FOREIGN KEY (StudentId) REFERENCES Student(StudentId)
);

-- Tạo bảng HealthCheckForm (Phiếu kiểm tra sức khỏe)
CREATE TABLE HealthCheckForm (
    FormId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    ParentId INT NOT NULL,
    SentDate DATETIME NOT NULL,
    Confirmed BIT DEFAULT 0,
    FOREIGN KEY (StudentId) REFERENCES Student(StudentId),
    FOREIGN KEY (ParentId) REFERENCES Parent(ParentId)
);

-- Tạo bảng Blog (Blog)
CREATE TABLE Blog (
    BlogId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Content NVARCHAR(MAX),
    DatePosted DATETIME NOT NULL DEFAULT GETDATE(),
    AuthorId INT,
    FOREIGN KEY (AuthorId) REFERENCES UserAccount(AccountId)
);

-- Tạo các chỉ mục để tối ưu hiệu suất
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

-- Thêm một số dữ liệu mẫu
INSERT INTO Parent (FullName, Phone, Email) VALUES 
(N'Nguyễn Văn A', '0123456789', 'nguyenvana@email.com'),
(N'Trần Thị B', '0987654321', 'tranthib@email.com'),
(N'Lê Văn C', '0123987654', 'levanc@email.com');

INSERT INTO Student (FullName, BirthDate, Gender, Class, ParentId) VALUES 
(N'Nguyễn Văn D', '2010-05-15', N'Nam', N'5A', 1),
(N'Trần Thị E', '2011-08-22', N'Nữ', N'4B', 2),
(N'Lê Văn F', '2009-12-10', N'Nam', N'6C', 3);

INSERT INTO UserAccount (Username, PasswordHash, Role) VALUES 
('admin', 'admin_hash', 'Admin'),
('nurse1', 'nurse1_hash', 'Nurse'),
('manager1', 'manager1_hash', 'Manager');

INSERT INTO HealthRecord (StudentId, Allergy, ChronicDisease, MedicalHistory, Vision, Hearing) VALUES 
(1, N'Không có', N'Không có', N'Bình thường', N'Tốt', N'Tốt'),
(2, N'Dị ứng bụi', N'Không có', N'Từng bị viêm họng', N'Cận thị nhẹ', N'Tốt'),
(3, N'Không có', N'Hen suyễn', N'Hen suyễn từ nhỏ', N'Tốt', N'Tốt');

PRINT 'Cơ sở dữ liệu đã được tạo thành công!';
PRINT 'Các bảng đã được tạo:';
PRINT '- Parent (Phụ huynh)';
PRINT '- Student (Học sinh)';
PRINT '- UserAccount (Tài khoản người dùng)';
PRINT '- HealthRecord (Hồ sơ sức khỏe)';
PRINT '- MedicalEvent (Sự kiện y tế)';
PRINT '- MedicalSupply (Vật tư y tế)';
PRINT '- MedicineSent (Thuốc gửi)';
PRINT '- Vaccination (Tiêm chủng)';
PRINT '- VaccinationConsentForm (Phiếu đồng ý tiêm chủng)';
PRINT '- HealthCheck (Kiểm tra sức khỏe)';
PRINT '- HealthCheckForm (Phiếu kiểm tra sức khỏe)';
PRINT '- Blog (Blog)';
PRINT 'Đã thêm dữ liệu mẫu và tạo các chỉ mục tối ưu hiệu suất!';