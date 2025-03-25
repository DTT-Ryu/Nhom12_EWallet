# Ewallet (Ví điện tử) - ASP.NET Core MVC

## Giới thiệu
Ewallet là một hệ thống ví điện tử được xây dựng bằng **ASP.NET Core MVC**. Dự án hỗ trợ người dùng thực hiện các giao dịch tài chính như nạp tiền, rút tiền vào tài khoản ngân hàng (giả lập), chuyển tiền, nhận tiền từ tài khoản ví cùng hệ thống và xem lịch sử giao dịch. Ngoài ra, Admin có quyền quản lý tất cả dữ liệu trong hệ thống.

## Yêu cầu hệ thống
- **.NET 8.0 hoặc mới hơn**
- **SQL Server**
- **Visual Studio 2022**

## Cài đặt và chạy dự án
### 1. Clone repository
```sh
git clone https://github.com/Nhom12_Ewallet/Nhom12_Ewallet.git
cd Nhom12_Ewallet
```
### 2. Cấu Hình Cơ Sở Dữ Liệu
Tạo Database trên SQL Server
Mở SQL Server Management Studio (SSMS) và đăng nhập vào SQL Server (nếu có).

Mở file script EWallet-script.sql hoặc chạy các lệnh CREATE TABLE theo mô hình dữ liệu của bạn.

### 3. Cấu hình `appsettings.json`
Trong thư mục **Nhom12_Ewallet**, tạo tệp `appsettings.json` và thêm nội dung sau:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=Ewallet;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
Thay thế `YOUR_SERVER`, `YOUR_USERNAME`, và `YOUR_PASSWORD` bằng thông tin SQL Server của bạn, nếu không đăng nhập thì xóa `User Id=YOUR_USERNAME;Password=YOUR_PASSWORD`.

### 4. Chạy ứng dụng
Nhấn `Ctrl + F5` để chạy dự án hoặc sử dụng lệnh:
```sh
dotnet run
```

## Tài khoản mẫu
- **User**: `updating`
- **Admin**: updating

## Chức năng chính
- **User**: Đăng ký, đăng nhập, xem lịch sử giao dịch, nạp/rút/chuyển tiền, chỉnh sửa thông tin cá nhân.
- **Admin**: Quản lý người dùng, ngân hàng, tài khoản ngân hàng, giao dịch + Đăng ký, đăng nhập, xem lịch sử giao dịch, nạp/rút/chuyển tiền, chỉnh sửa thông tin cá nhân.

---
🚀 Chúc bạn thành công với dự án Ewallet!

