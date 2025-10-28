
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

## 🧠 Cách hoạt động theo 3 lớp (GUI – BUS – DAL)

### 1) DAL (Data Access Layer) – Truy cập dữ liệu
- Sử dụng Entity Framework 6 Code-First với `DbContext` là `Model1`.
- Mỗi bảng là một entity: `Product`, `ProductImage`, `Category`, `Customer`, `OrderTbl`, `OrderItem`, `Payment`, `Review`, `Shipment`, `Coupon`, `Inventory`, `Address`, `ProductVariant`, v.v.
- Migrations: quản lý thay đổi schema. Ví dụ: `AddIsAdminToCustomer` thêm cột `IsAdmin` cho `Customer`, `SeedAdmin` tạo tài khoản admin mẫu.
- Kết nối CSDL cấu hình trong `App.config` của từng project (`TMDT`, `TMDT.BUS`, `TMDT.DAL`).

Vai trò: DAL chỉ chịu trách nhiệm ánh xạ CSDL ↔ đối tượng C#, không chứa logic nghiệp vụ UI.

### 2) BUS (Business Logic Layer) – Nghiệp vụ
- Đóng gói logic sử dụng DAL để phục vụ GUI.
- Các lớp tiêu biểu:
  - `ProductBUS`: đọc danh sách sản phẩm, lọc theo danh mục, lấy ảnh từ `ProductImages`.
  - `CartBUS`: thêm/xóa/cập nhật số lượng sản phẩm trong giỏ; tính tổng tiền (có thể áp dụng coupon demo).
  - `CustomerBUS`: đăng ký/đăng nhập, cập nhật thông tin người dùng.
  - `OrderBUS`: tạo `OrderTbl` và `OrderItem` khi checkout, cập nhật trạng thái.
  - `PaymentBUS`: sinh dữ liệu QR, tạo bản ghi thanh toán, xác nhận thanh toán.

Vai trò: BUS là nơi kiểm soát quy tắc nghiệp vụ, xác thực dữ liệu đầu vào cơ bản, điều phối gọi DAL. GUI chỉ gọi BUS, không truy cập DB trực tiếp.

### 3) GUI (Presentation Layer) – Giao diện
- WinForms ở project `TMDT`, gồm các form theo bối cảnh: `login/*`, `Use/*` (người dùng), `Admin/*` (quản trị).
- Một số form chính:
  - `login/Login`: xác thực, đặt `Session.CurrentCustomer`.
  - `Use/UserMain`: hiển thị danh mục/sản phẩm bằng grid card; gọi `ProductBUS` để lấy dữ liệu.
  - `Use/CartForm`, `Use/CheckoutForm`: thao tác giỏ hàng, tạo đơn (gọi `OrderBUS`).
  - `Use/QRCodePaymentForm`: hiển thị QR để thanh toán (gọi `PaymentBUS`).
  - `Use/OrderHistoryForm`: liệt kê đơn hàng của người dùng.
  - `Admin/*`: quản lý sản phẩm, khách hàng, đơn hàng, thống kê.

Vai trò: GUI tập trung render giao diện, bắt sự kiện click, nhập liệu. Toàn bộ dữ liệu lấy/ghi thông qua các lớp BUS.

### Luồng tương tác mẫu
1) Đăng nhập: `Login` → xác thực qua `CustomerBUS` → lưu `Session.CurrentCustomer` → điều hướng Admin/User.
2) Xem sản phẩm: `UserMain.LoadProductsGrid()` → gọi `ProductBUS.GetAllProducts()` hoặc `GetProductsByCategory()` → render card (ảnh từ `ProductImages`).
3) Giỏ hàng: `CartForm` gọi `CartBUS` để thêm/xóa/cập nhật, tính tổng.
4) Thanh toán: `CheckoutForm` gọi `OrderBUS` tạo `OrderTbl`/`OrderItem` → mở `QRCodePaymentForm` nếu chọn QR.

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
* **Thanh toán QR:** tạo mã QR cho đơn vừa tạo, xác nhận qua form QR

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

## 💳 Thanh toán bằng QR 

### Mục tiêu
Cung cấp phương thức thanh toán bằng cách hiển thị mã QR để người dùng quét qua ứng dụng ngân hàng/ ví điện tử.

### Các thành phần
- GUI: `Use/QRCodePaymentForm`
- BUS: `PaymentBUS` với các hàm chính:
  - `GenerateQRData(orderId, totalAmount)`: sinh chuỗi dữ liệu làm nội dung QR (ví dụ: chứa thông tin đơn, số tiền, định dạng tuân theo chuẩn nội bộ).
  - `CreateQRPayment(orderId, totalAmount)`: tạo bản ghi/phiên thanh toán QR trong DB để theo dõi.
  - `ConfirmPayment(orderId)`: xác nhận thanh toán thành công (cập nhật trạng thái đơn/payments).
- Thư viện: `QRCoder` để render ảnh QR từ chuỗi dữ liệu.

### Luồng hoạt động
1) Tại `CheckoutForm`, sau khi tạo đơn (`OrderBUS`), nếu chọn phương thức QR → mở `new QRCodePaymentForm(orderId, totalAmount)`.
2) `QRCodePaymentForm`:
   - Gọi `PaymentBUS.GenerateQRData()` để lấy chuỗi QR.
   - Dùng `QRCoder` tạo bitmap và hiển thị trên `pictureBoxQR`.
   - Gọi `PaymentBUS.CreateQRPayment()` ghi nhận phiên thanh toán trong DB.
3) Người dùng quét QR, thực hiện thanh toán trong app ngân hàng.
4) Bấm nút “Đã thanh toán” trên form:
   - Gọi `PaymentBUS.ConfirmPayment(orderId)`.
   - Nếu thành công → thông báo và đóng form (đơn ở trạng thái đã thanh toán).
5) Nút “Hủy”: đóng form, không xác nhận thanh toán (đơn giữ trạng thái trước đó).



