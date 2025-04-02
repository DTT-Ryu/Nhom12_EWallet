create database EWallet

USE EWallet;

-- Bảng quyền
CREATE TABLE tblRoles (
    iRoleID_PK TINYINT PRIMARY KEY,       -- Mã quyền (PK)
    sRoleName NVARCHAR(50) UNIQUE NOT NULL -- Tên quyền (Admin, User, ...)
);

-- Bảng người dùng (tách quyền ra bảng riêng và thêm cột cập nhật)
CREATE TABLE tblUsers (
    iUserID_PK INT IDENTITY(1,1) PRIMARY KEY,  -- Mã người dùng (PK)
    sPhoneNumber VARCHAR(10) UNIQUE NOT NULL,  -- Số điện thoại
    sFullName NVARCHAR(100) NOT NULL,          -- Họ tên
    sCCCD VARCHAR(12) UNIQUE NOT NULL,         -- Căn cước công dân
    dBirthDate DATE NOT NULL,                  -- Ngày sinh
    sEmail VARCHAR(100) UNIQUE NOT NULL,       -- Email
    fBalance DECIMAL(15,2) DEFAULT 0,          -- Số dư ví
    sPasswordHash VARCHAR(255) NOT NULL,       -- Mật khẩu đã mã hóa
    sPinCode VARCHAR(255) NOT NULL,              -- Mã PIN
    iRoleID_FK TINYINT NOT NULL,               -- Mã quyền (FK)
    dCreatedAt DATETIME DEFAULT GETDATE(),     -- Ngày tạo tài khoản
    dUpdatedAt DATETIME DEFAULT GETDATE(),     -- Thời gian cập nhật thông tin
    sStatus NVARCHAR(10) CHECK (sStatus IN ('active', 'blocked')) DEFAULT 'active',  -- Trạng thái tài khoản

    CONSTRAINT FK_tblUsers_Role FOREIGN KEY (iRoleID_FK) REFERENCES tblRoles(iRoleID_PK)
);

-- Bảng ngân hàng
CREATE TABLE tblBanks (
    sBankID_PK VARCHAR(10) PRIMARY KEY NOT NULL,  -- Mã ngân hàng (PK)
    sBankName NVARCHAR(100) UNIQUE NOT NULL,      -- Tên ngân hàng
    sImage VARCHAR(255) NULL                      -- Ảnh logo ngân hàng (có thể NULL)
);

-- Bảng tài khoản ngân hàng
CREATE TABLE tblBankAccounts (
    iAccountID_PK INT IDENTITY(1,1) PRIMARY KEY,  -- Mã tài khoản ngân hàng (PK)
    iUserID_FK INT NOT NULL,                      -- Mã người dùng (FK)
    sBankID_FK VARCHAR(10) NOT NULL,              -- Mã ngân hàng (FK)
    sAccountNumber VARCHAR(20) UNIQUE NOT NULL,   -- Số tài khoản ngân hàng
    sStatus NVARCHAR(10) CHECK (sStatus IN ('active', 'blocked')) DEFAULT 'active',

    CONSTRAINT FK_tblBankAccounts_User FOREIGN KEY (iUserID_FK) REFERENCES tblUsers(iUserID_PK),
    CONSTRAINT FK_tblBankAccounts_Bank FOREIGN KEY (sBankID_FK) REFERENCES tblBanks(sBankID_PK)
);

-- Bảng giao dịch
CREATE TABLE tblTransactions (
    iTransactionID_PK INT IDENTITY(1,1) PRIMARY KEY,  -- Mã giao dịch (PK)
    iSenderUserID_FK INT NOT NULL,                    -- Người gửi (FK)
    sTransactionType NVARCHAR(10) CHECK (sTransactionType IN ('deposit', 'withdraw', 'transfer')) NOT NULL,  -- Loại giao dịch
    fAmount DECIMAL(15,2) NOT NULL,                   -- Số tiền giao dịch
    dCreatedAt DATETIME DEFAULT GETDATE(),            -- Thời gian thực hiện
    sDescription NVARCHAR(255) NULL,                  -- Nội dung giao dịch
    iRecipientUserID_FK INT NULL,                     -- Người nhận (FK - có thể NULL)
    iBankAccountID_FK INT NULL,                       -- Tài khoản ngân hàng (FK - có thể NULL)
    sStatus NVARCHAR(10) CHECK (sStatus IN ('pending', 'completed', 'failed')) DEFAULT 'pending',

    CONSTRAINT FK_tblTransactions_Sender FOREIGN KEY (iSenderUserID_FK) REFERENCES tblUsers(iUserID_PK),
    CONSTRAINT FK_tblTransactions_Recipient FOREIGN KEY (iRecipientUserID_FK) REFERENCES tblUsers(iUserID_PK),
    CONSTRAINT FK_tblTransactions_Bank FOREIGN KEY (iBankAccountID_FK) REFERENCES tblBankAccounts(iAccountID_PK)
);





USE EWallet;

-- Dữ liệu cho bảng Quyền
INSERT INTO tblRoles (iRoleID_PK, sRoleName)
VALUES 
    (1, 'Admin'),
    (2, 'User');

-- Dữ liệu cho bảng người dùng
INSERT INTO tblUsers (sPhoneNumber, sFullName, sCCCD, dBirthDate, sEmail, fBalance, sPasswordHash, sPinCode, iRoleID_FK)
VALUES 
    ('0987654321', N'Nguyễn Văn A', '001200123456', '1990-01-15', 'admin1@example.com', 5000000, 'hashed_password_1', '123456', 1),
    ('0976543210', N'Trần Thị B', '001200654321', '1992-05-20', 'admin2@example.com', 4000000, 'hashed_password_2', '654321', 1),

    ('0912345678', N'Lê Hoàng C', '001201234567', '1995-03-10', 'user1@example.com', 1000000, 'hashed_password_3', '111111', 2),
    ('0923456789', N'Hoàng Anh D', '001202345678', '1998-07-25', 'user2@example.com', 2000000, 'hashed_password_4', '222222', 2),
    ('0934567890', N'Phạm Minh E', '001203456789', '1996-11-30', 'user3@example.com', 1500000, 'hashed_password_5', '333333', 2),
    ('0945678901', N'Đỗ Quang F', '001204567890', '1993-06-18', 'user4@example.com', 800000, 'hashed_password_6', '444444', 2),
    ('0956789012', N'Vũ Thị G', '001205678901', '1997-12-05', 'user5@example.com', 2500000, 'hashed_password_7', '555555', 2),
    ('0967890123', N'Bùi Hữu H', '001206789012', '2000-04-14', 'user6@example.com', 3000000, 'hashed_password_8', '666666', 2),
    ('0978901234', N'Nguyễn Thị I', '001207890123', '1999-09-22', 'user7@example.com', 1200000, 'hashed_password_9', '777777', 2),
    ('0989012345', N'Phan Văn K', '001208901234', '1994-02-28', 'user8@example.com', 1800000, 'hashed_password_10', '888888', 2);


-- Dữ liệu cho bảng ngân hàng
INSERT INTO tblBanks (sBankID_PK, sBankName)
VALUES 
    ('BIDV', N'Ngân hàng Đầu tư và Phát triển Việt Nam'),
    ('VCB', N'Vietcombank (Ngân hàng Ngoại thương Việt Nam)'),
    ('VPB', N'VPBank (Ngân hàng Việt Nam Thịnh Vượng)'),
    ('TCB', N'Techcombank (Ngân hàng Kỹ Thương Việt Nam)'),
    ('TPB', N'TPBank (Ngân hàng Tiên Phong)');

-- Dữ liệu cho bảng tài khoản ngân hàng
INSERT INTO tblBankAccounts (iUserID_FK, sBankID_FK, sAccountNumber, sStatus)
VALUES 
	(1, 'BIDV', '012345678901', 'active'),
	(2, 'VCB', '123456789012', 'active'),
    (3, 'BIDV', '101234567890', 'active'),
    (4, 'VCB', '102345678901', 'active'),
    (5, 'VPB', '103456789012', 'active'),
    (6, 'TCB', '104567890123', 'active'),
    (7, 'TPB', '105678901234', 'active'),
    (8, 'BIDV', '106789012345', 'active'),
    (9, 'VCB', '107890123456', 'active'),
    (10, 'VPB', '108901234567', 'active'),
    (3, 'TCB', '109012345678', 'active'),
    (4, 'TPB', '110123456789', 'active');

-- Dữ liệu cho bảng giao dịch
INSERT INTO tblTransactions (iSenderUserID_FK, sTransactionType, fAmount, iRecipientUserID_FK, iBankAccountID_FK, sDescription, sStatus)
VALUES 

	(1, 'deposit', 5000000, NULL, 1, 'Nạp tiền từ BIDV', 'completed'),
    (1, 'transfer', 2000000, 3, NULL, 'Chuyển tiền cho user 3', 'completed'),
    (1, 'withdraw', 1000000, NULL, 1, 'Rút tiền về tài khoản BIDV', 'completed'),
	(2, 'deposit', 7000000, NULL, 2, 'Nạp tiền từ Vietcombank', 'completed'),
    (2, 'transfer', 1500000, 4, NULL, 'Chuyển tiền cho user 4', 'completed'),
    (2, 'withdraw', 2000000, NULL, 2, 'Rút tiền về tài khoản Vietcombank', 'completed'),

    (3, 'transfer', 500000, 4, NULL, N'Chuyển tiền cho bạn', 'completed'),
    (4, 'deposit', 1000000, NULL, 1, N'Nạp tiền vào tài khoản', 'completed'),
    (5, 'withdraw', 300000, NULL, 3, N'Rút tiền ATM', 'completed'),
    (6, 'transfer', 750000, 7, NULL, N'Chuyển tiền thanh toán', 'completed'),
    (7, 'deposit', 2000000, NULL, 2, N'Nạp tiền từ lương', 'completed'),
    (8, 'withdraw', 500000, NULL, 4, N'Rút tiền mua sắm', 'completed'),
    (9, 'transfer', 1200000, 10, NULL, N'Chuyển khoản mua hàng', 'completed'),
    (10, 'deposit', 500000, NULL, 6, N'Nạp tiền qua ngân hàng', 'completed'),
    (3, 'withdraw', 450000, NULL, 7, N'Rút tiền tiêu vặt', 'completed'),
    (4, 'transfer', 900000, 5, NULL, N'Chuyển tiền hỗ trợ', 'completed'),
    (5, 'deposit', 1500000, NULL, 8, N'Nạp tiền qua ngân hàng', 'completed'),
    (6, 'withdraw', 350000, NULL, 9, N'Rút tiền mua đồ', 'completed'),
    (7, 'transfer', 250000, 8, NULL, N'Chuyển khoản nhỏ', 'completed'),
    (8, 'deposit', 700000, NULL, 10, N'Nạp tiền tiêu dùng', 'completed'),
    (9, 'withdraw', 500000, NULL, 1, N'Rút tiền mua vé máy bay', 'completed'),
    (10, 'transfer', 1000000, 3, NULL, N'Thanh toán hóa đơn', 'completed'),
    (3, 'deposit', 1200000, NULL, 2, N'Nạp tiền từ tài khoản ngân hàng', 'completed'),
    (4, 'withdraw', 800000, NULL, 3, N'Rút tiền mua điện thoại', 'completed'),
    (5, 'transfer', 600000, 6, NULL, N'Chuyển tiền liên ngân hàng', 'completed'),
    (6, 'deposit', 900000, NULL, 5, N'Nạp tiền ví điện tử', 'completed');



