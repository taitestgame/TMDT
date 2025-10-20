
# 🛍️ E-Commerce WinForms (TMDT)

Ứng dụng bán hàng cơ bản phát triển bằng **WinForms (.NET Framework 4.7.2)**, tuân theo **mô hình 3 lớp**:  
**GUI (WinForms)** – **BUS (Business Logic)** – **DAL (Entity Framework 6)**.

---

## ⚙️ Kiến trúc & Công nghệ

| Thành phần | Mô tả |
|-------------|-------|
| **GUI** | Giao diện WinForms (`TMDT`) |
| **BUS** | Xử lý nghiệp vụ (`TMDT.BUS`) |
| **DAL** | Truy cập dữ liệu với **Entity Framework 6** (`TMDT.DAL`) |
| **DbContext** | `Model1` |
| **Database** | SQL Server |
| **Framework** | .NET Framework 4.7.2 |

---

## 📁 Cấu trúc Solution

```

TMDT.sln
│
├── TMDT/                # Giao diện WinForms (Login, Register, User/*, Admin/*)
├── TMDT.BUS/            # Lớp nghiệp vụ (CustomerBUS, ProductBUS, CartBUS, …)
├── TMDT.DAL/            # Entity classes, DbContext, EF Migrations
└── Database/
└── tmdtdatabase.sql   # Script tạo cơ sở dữ liệu và dữ liệu mẫu

````

---

## 🧩 Thiết lập Môi Trường

### Yêu cầu
- SQL Server (Express hoặc Developer)
- Visual Studio 2019 / 2022  
- .NET Framework 4.7.2 Developer Pack  

### Khôi phục Cơ sở Dữ liệu

**Cách 1 – Sử dụng script SQL**
```sql
-- Mở Database/tmdtdatabase.sql trong SSMS và Execute toàn bộ
ALTER TABLE Customer ADD IsAdmin BIT NOT NULL DEFAULT(0);

-- Thêm tài khoản admin
INSERT INTO Customer (UserName, Password, Email, FullName, Phone, IsAdmin)
VALUES (N'admin', 'admin123', N'admin@shop.com', N'Quản trị hệ thống', N'0900000000', 1);
````

**Cách 2 – Entity Framework Migrations**

```powershell
# Trong Package Manager Console
# Chọn "TMDT.DAL" làm Default Project
Update-Database
```

**Chạy ứng dụng**

* Mở `TMDT.sln`
* Build Solution → Start (chọn project `TMDT` làm startup)

---

## 👤 Tài Khoản Mẫu

| Loại      | Tên đăng nhập      | Mật khẩu        |
| --------- | ------------------ | --------------- |
| **Admin** | `admin`            | `admin123`      |
| **User**  | `user1` … `user20` | Theo script SQL |

> Lưu ý: Tài khoản admin cần có `IsAdmin = 1` trong bảng `Customer`.

---

## 💡 Chức Năng Chính

### Người Dùng (User)

* **Đăng ký / Đăng nhập**

  * Nhập thông tin: tên đăng nhập, mật khẩu, email, họ tên, điện thoại, địa chỉ
  * Tự động tạo địa chỉ mặc định sau đăng ký
  * Phân quyền đăng nhập:

    * **Admin** → `AdminMain`
    * **User** → `UserMain`

* **Trang chủ:** hiển thị danh sách sản phẩm (ảnh từ URL)

* **Chi tiết sản phẩm:** xem thông tin, chọn số lượng, thêm vào giỏ hàng

* **Giỏ hàng:** xem danh sách sản phẩm, áp dụng mã giảm giá (demo), thanh toán

* **Thanh toán:** xác nhận thông tin, tạo đơn hàng, hiển thị trong `OrderHistoryForm`

* **Lịch sử đơn hàng:** xem chi tiết và trạng thái đơn

* **Tài khoản:** xem và chỉnh sửa họ tên, số điện thoại

### Quản Trị Viên (Admin)

* **Giao diện chính:** `AdminMainForm` (menu trái, các tab dữ liệu)
* **Quản lý sản phẩm:** thêm, sửa, xóa, chọn danh mục
* **Quản lý khách hàng:** cập nhật nhanh thông tin, xóa hoặc làm mới danh sách
* **Quản lý đơn hàng:** xem chi tiết, cập nhật trạng thái, xóa đơn
* **Thống kê:** hiển thị tổng quan số lượng và doanh thu (dạng đơn giản)

---

## 🖼️ Hình Ảnh Sản Phẩm

* Mặc định tải từ URL trong `ProductImage.Url`
* Nếu muốn chạy offline:

  1. Tạo thư mục `TMDT/Images/`
  2. Thêm ảnh vào thư mục
  3. Đặt thuộc tính `Copy to Output Directory = Copy if newer`
  4. Thay code tải ảnh:

     ```csharp
     Image.FromFile(Path.Combine(Application.StartupPath, "Images", "xxx.jpg"));
     ```

---


## 🧠 Ghi Chú Phát Triển

* Sử dụng **Entity Framework 6** theo convention
* `Customer.IsAdmin` dùng để phân quyền
* Các lớp trong BUS được public để tầng GUI có thể sử dụng (`ProductBUS`, `CustomerBUS`, …)
* Lưu thông tin người dùng đăng nhập tại `Session.CurrentCustomer`
* Ứng dụng bật **TLS 1.2** trong `Program.Main` để tránh lỗi tải ảnh HTTPS

---

