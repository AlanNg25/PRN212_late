-- 1. Phụ huynh
INSERT INTO Parent (FullName, Phone, Email)
VALUES 
(N'Nguyễn Văn A', '0909123456', 'a.parent@example.com'),
(N'Lê Thị B', '0912233445', 'b.parent@example.com');

-- 2. Tài khoản
INSERT INTO UserAccount (Username, PasswordHash, Role, ParentId)
VALUES 
('admin01', 'hashed_adminpass', 'Admin', NULL),
('nurse01', 'hashed_nursepass', 'Nurse', NULL),
('manager01', 'hashed_managerpass', 'Manager', NULL),
('parentA', 'hashed_passA', 'Parent', 1),
('parentB', 'hashed_passB', 'Parent', 2);

-- 3. Học sinh
INSERT INTO Student (FullName, BirthDate, Gender, Class, ParentId)
VALUES 
(N'Nguyễn Minh Khoa', '2012-06-12', N'Nam', '6A1', 1),
(N'Lê Mai Linh', '2011-09-20', N'Nữ', '6A2', 2);

-- 4. Hồ sơ sức khỏe
INSERT INTO HealthRecord (StudentId, Allergy, ChronicDisease, MedicalHistory, Vision, Hearing)
VALUES 
(1, N'Không', N'Hen suyễn nhẹ', N'Tiêm phòng đầy đủ', '10/10', 'Bình thường'),
(2, N'Tôm cua', N'Không', N'Cảm cúm thường xuyên', '9/10', 'Bình thường');

-- 5. Sự kiện y tế
INSERT INTO MedicalEvent (StudentId, NurseId, Date, Description, TreatmentGiven)
VALUES 
(1, 2, '2024-10-01 09:00', N'Đau bụng sau giờ ăn', N'Nghỉ ngơi + Uống nước ấm'),
(2, 2, '2024-11-15 14:00', N'Té ngã nhẹ trong giờ ra chơi', N'Sát trùng vết thương, dán băng cá nhân');

-- 6. Vật tư y tế
INSERT INTO MedicalSupply (Name, Quantity, ExpirationDate)
VALUES 
(N'Băng cá nhân', 200, '2026-01-01'),
(N'Cồn y tế 70%', 50, '2025-12-31');

-- 7. Thuốc gửi theo học sinh
INSERT INTO MedicineSent (StudentId, ParentId, MedicineName, Dosage, Instruction)
VALUES 
(1, 1, N'Paracetamol 250mg', N'1 viên sau ăn sáng', N'Uống nếu sốt > 38 độ'),
(2, 2, N'Siro ho Prospan', N'5ml sáng + tối', N'Uống sau ăn, lắc đều trước khi dùng');

-- 8. Lịch sử tiêm chủng
INSERT INTO Vaccination (StudentId, VaccineName, Date, NurseId, Result)
VALUES 
(1, N'Viêm gan B', '2023-05-20', 2, N'Không phản ứng bất thường'),
(2, N'Uốn ván', '2023-07-10', 2, N'Sốt nhẹ sau tiêm');

-- 9. Phiếu xin xác nhận tiêm
INSERT INTO VaccinationConsentForm (StudentId, ParentId, SentDate, Confirmed)
VALUES 
(1, 1, '2023-05-10 08:00', 1),
(2, 2, '2023-07-01 10:30', 0);

-- 10. Khám sức khỏe định kỳ
INSERT INTO HealthCheck (StudentId, Date, Result, DoctorNotes)
VALUES 
(1, '2024-04-10', N'Khỏe mạnh bình thường', N'Cần chú ý giữ vệ sinh răng miệng'),
(2, '2024-04-10', N'Nhẹ cân so với tuổi', N'Khuyến khích ăn nhiều rau và uống sữa');

-- 11. Phiếu khám sức khỏe
INSERT INTO HealthCheckForm (StudentId, ParentId, SentDate, Confirmed)
VALUES 
(1, 1, '2024-03-30 15:00', 1),
(2, 2, '2024-03-30 15:30', 0);

-- 12. Bài viết blog
INSERT INTO Blog (Title, Content, AuthorId, Type)
VALUES 
-- Type = 1 (Chia sẻ kiến thức y tế)
(N'Làm sao để trẻ không sợ tiêm?', N'Nội dung chia sẻ mẹo nhỏ để giúp trẻ vượt qua nỗi sợ tiêm phòng...', 2, 1),
(N'Những bệnh học đường thường gặp', N'Bài viết tổng hợp các bệnh lý thường gặp ở học sinh tiểu học và THCS.', 2, 1),
(N'Triệu chứng thiếu vitamin ở trẻ em', N'Nhận biết các dấu hiệu thiếu vitamin như mệt mỏi, da khô, dễ ốm và cách bổ sung.', 1, 1),
(N'Phòng tránh cúm học đường', N'Hướng dẫn cha mẹ và học sinh cách phòng tránh cúm, nhất là trong mùa đông và đầu năm học mới.', 2, 1),
(N'Tầm quan trọng của kiểm tra sức khỏe định kỳ', N'Giải thích vì sao khám sức khỏe định kỳ lại cần thiết cho học sinh các cấp.', 1, 1),

-- Type = 2 (Hướng dẫn sinh hoạt, mẹo vặt, chăm sóc)
(N'Cách chăm sóc sức khỏe mùa thi', N'Bài viết chia sẻ các mẹo để giữ gìn sức khỏe trong thời gian ôn thi căng thẳng như ngủ đủ giấc, ăn uống đầy đủ dinh dưỡng và giữ tinh thần thoải mái.', 1, 2),
(N'Tập thể dục tại trường: Hướng dẫn cơ bản', N'Những bài tập đơn giản mà học sinh có thể thực hiện trong giờ ra chơi để tăng cường thể chất như xoay khớp, bật nhảy nhẹ và hít thở sâu.', 2, 2),
(N'Chế độ dinh dưỡng hợp lý cho học sinh', N'Giới thiệu thực đơn mẫu giúp học sinh đủ năng lượng và phát triển tốt trong độ tuổi học đường.', 1, 2),
(N'Mẹo tránh mỏi mắt khi học online', N'Hướng dẫn điều chỉnh ánh sáng, tư thế và thời gian nghỉ để bảo vệ mắt khi học trực tuyến.', 2, 2),
(N'Bí quyết giữ vệ sinh cá nhân tại trường', N'Cách rửa tay đúng cách, sử dụng khẩu trang, giữ khoảng cách để ngừa lây nhiễm bệnh.', 2, 2);
