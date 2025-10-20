USE MASTER
GO

drop database QuanLyTMDT
go

CREATE DATABASE QuanLyTMDT
GO

USE QuanLyTMDT
GO


-- Faculty-style naming removed; using ecommerce schema
-- 1. Customers (and Users can be joined if needed)
CREATE TABLE [Customer] (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NULL,
    Password VARCHAR(100) NULL,
    Email NVARCHAR(200) NOT NULL,
    FullName NVARCHAR(200) NULL,
    Phone NVARCHAR(30) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- 2. Address
CREATE TABLE [Address] (
    AddressID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    Type NVARCHAR(20) NULL, -- 'Billing'/'Shipping'
    Street NVARCHAR(300) NULL,
    City NVARCHAR(100) NULL,
    State NVARCHAR(100) NULL,
    PostalCode NVARCHAR(20) NULL,
    Country NVARCHAR(100) NULL,
    IsDefault BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

-- 3. Category (self-referencing)
CREATE TABLE [Category] (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Slug NVARCHAR(200) NULL,
    ParentCategoryID INT NULL,
    SortOrder INT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (ParentCategoryID) REFERENCES Category(CategoryID)
);

-- 4. Product
CREATE TABLE [Product] (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    SKU NVARCHAR(100) NULL UNIQUE,
    Name NVARCHAR(300) NOT NULL,
    ShortDescription NVARCHAR(500) NULL,
    Description NVARCHAR(MAX) NULL,
    DefaultCategoryID INT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (DefaultCategoryID) REFERENCES Category(CategoryID)
);

-- 5. ProductVariant (optional, supports color/size)
CREATE TABLE [ProductVariant] (
    VariantID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NOT NULL,
    SKU NVARCHAR(100) NULL,
    Attributes NVARCHAR(MAX) NULL, -- JSON/text for attributes
    Price MONEY NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- 6. ProductImage
CREATE TABLE [ProductImage] (
    ImageID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NULL,
    VariantID INT NULL,
    Url NVARCHAR(1000) NOT NULL,
    AltText NVARCHAR(300) NULL,
    SortOrder INT NULL DEFAULT 0,
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
    FOREIGN KEY (VariantID) REFERENCES ProductVariant(VariantID)
);

-- 7. Inventory
CREATE TABLE [Inventory] (
    InventoryID INT IDENTITY(1,1) PRIMARY KEY,
    VariantID INT NOT NULL,
    QuantityAvailable INT NOT NULL DEFAULT 0,
    QuantityReserved INT NOT NULL DEFAULT 0,
    LastUpdated DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (VariantID) REFERENCES ProductVariant(VariantID)
);

-- 8. Cart + CartItem
CREATE TABLE [Cart] (
    CartID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

CREATE TABLE [CartItem] (
    CartItemID INT IDENTITY(1,1) PRIMARY KEY,
    CartID INT NOT NULL,
    VariantID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice MONEY NOT NULL, -- snapshot price at time added
    FOREIGN KEY (CartID) REFERENCES Cart(CartID),
    FOREIGN KEY (VariantID) REFERENCES ProductVariant(VariantID)
);

-- 9. Order + OrderItem
CREATE TABLE [OrderTbl] (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    Status NVARCHAR(50) NOT NULL, -- Pending, Paid, Shipped, Completed, Cancelled
    BillingAddressID INT NULL,
    ShippingAddressID INT NULL,
    SubTotal MONEY NOT NULL DEFAULT 0,
    ShippingFee MONEY NOT NULL DEFAULT 0,
    TaxAmount MONEY NOT NULL DEFAULT 0,
    DiscountAmount MONEY NOT NULL DEFAULT 0,
    TotalAmount MONEY NOT NULL DEFAULT 0,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (BillingAddressID) REFERENCES Address(AddressID),
    FOREIGN KEY (ShippingAddressID) REFERENCES Address(AddressID)
);

CREATE TABLE [OrderItem] (
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    VariantID INT NOT NULL,
    SKU NVARCHAR(100) NULL,
    ProductName NVARCHAR(300) NULL,
    Quantity INT NOT NULL,
    UnitPrice MONEY NOT NULL,
    TotalPrice MONEY NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES OrderTbl(OrderID),
    FOREIGN KEY (VariantID) REFERENCES ProductVariant(VariantID)
);

-- 10. Payment
CREATE TABLE [Payment] (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    PaymentMethod NVARCHAR(50) NULL,
    Amount MONEY NOT NULL,
    Status NVARCHAR(50) NULL, -- Pending, Completed, Failed, Refunded
    TransactionRef NVARCHAR(200) NULL,
    PaidAt DATETIME NULL,
    FOREIGN KEY (OrderID) REFERENCES OrderTbl(OrderID)
);

-- 11. Shipment
CREATE TABLE [Shipment] (
    ShipmentID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    Carrier NVARCHAR(100) NULL,
    TrackingNumber NVARCHAR(200) NULL,
    ShippedAt DATETIME NULL,
    DeliveredAt DATETIME NULL,
    Status NVARCHAR(50) NULL,
    FOREIGN KEY (OrderID) REFERENCES OrderTbl(OrderID)
);

-- 12. Coupon
CREATE TABLE [Coupon] (
    CouponID INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Type NVARCHAR(20) NOT NULL, -- percent/fixed
    Value MONEY NOT NULL,
    StartDate DATETIME NULL,
    EndDate DATETIME NULL,
    MinOrderAmount MONEY NULL,
    UsageLimit INT NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

-- 13. Review
CREATE TABLE [Review] (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NOT NULL,
    CustomerID INT NOT NULL,
    Rating INT NOT NULL CHECK (Rating BETWEEN 1 AND 5),
    Title NVARCHAR(200) NULL,
    Content NVARCHAR(MAX) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    IsApproved BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);



--Nhap du lieu
------------------------------------------------
-- 1. CUSTOMER (20 bản ghi)
------------------------------------------------
INSERT INTO Customer (UserName, Password, Email, FullName, Phone)
VALUES
(N'user1', '123', N'user1@mail.com', N'Nguyễn Văn A', N'0901111111'),
(N'user2', '1234', N'user2@mail.com', N'Trần Thị B', N'0902222222'),
(N'user3', '234', N'user3@mail.com', N'Lê Văn C', N'0903333333'),
(N'user4', '2345', N'user4@mail.com', N'Phạm Thị D', N'0904444444'),
(N'user5', '345', N'user5@mail.com', N'Hoàng Văn E', N'0905555555'),
(N'user6', '3456', N'user6@mail.com', N'Đỗ Thị F', N'0906666666'),
(N'user7', '321', N'user7@mail.com', N'Vũ Văn G', N'0907777777'),
(N'user8', '3212', N'user8@mail.com', N'Phan Thị H', N'0908888888'),
(N'user9', '231', N'user9@mail.com', N'Bùi Văn I', N'0909999999'),
(N'user10', '23121', N'user10@mail.com', N'Đặng Thị J', N'0910000000'),
(N'user11', '653', N'user11@mail.com', N'Lý Văn K', N'0911111111'),
(N'user12', '9867', N'user12@mail.com', N'Phạm Minh L', N'0912222222'),
(N'user13', '068', N'user13@mail.com', N'Trần Hữu M', N'0913333333'),
(N'user14', '1045', N'user14@mail.com', N'Lưu Thị N', N'0914444444'),
(N'user15', '961', N'user15@mail.com', N'Đỗ Văn O', N'0915555555'),
(N'user16', '009', N'user16@mail.com', N'Trương Thị P', N'0916666666'),
(N'user17', '008', N'user17@mail.com', N'Lê Văn Q', N'0917777777'),
(N'user18', '007', N'user18@mail.com', N'Nguyễn Thị R', N'0918888888'),
(N'user19', '006', N'user19@mail.com', N'Phan Văn S', N'0919999999'),
(N'user20', '005', N'user20@mail.com', N'Bùi Thị T', N'0920000000');
SELECT * FROM Customer

------------------------------------------------
-- 2. ADDRESS (mỗi khách có ít nhất 1 địa chỉ)
------------------------------------------------
INSERT INTO Address (CustomerID, Type, Street, City, State, PostalCode, Country, IsDefault)
VALUES
(1, N'Shipping', N'12 Nguyễn Huệ', N'Hồ Chí Minh', N'HCM', N'700000', N'Việt Nam', 1),
(1, N'Billing', N'123 Lê Lợi', N'Hồ Chí Minh', N'HCM', N'700001', N'Việt Nam', 0),
(2, N'Shipping', N'22 Trần Hưng Đạo', N'Hà Nội', N'Hà Nội', N'100000', N'Việt Nam', 1),
(3, N'Shipping', N'34 Nguyễn Trãi', N'Đà Nẵng', N'Đà Nẵng', N'500000', N'Việt Nam', 1),
(4, N'Shipping', N'56 Hai Bà Trưng', N'Hà Nội', N'Hà Nội', N'100001', N'Việt Nam', 1),
(5, N'Shipping', N'78 Pasteur', N'Hồ Chí Minh', N'HCM', N'700002', N'Việt Nam', 1),
(6, N'Shipping', N'90 Nguyễn Văn Linh', N'Đà Nẵng', N'Đà Nẵng', N'500001', N'Việt Nam', 1),
(7, N'Shipping', N'111 Lý Thường Kiệt', N'Hà Nội', N'Hà Nội', N'100002', N'Việt Nam', 1),
(8, N'Shipping', N'122 Bạch Đằng', N'Đà Nẵng', N'Đà Nẵng', N'500002', N'Việt Nam', 1),
(9, N'Shipping', N'133 Cách Mạng Tháng 8', N'HCM', N'HCM', N'700003', N'Việt Nam', 1),
(10, N'Shipping', N'144 Điện Biên Phủ', N'Hà Nội', N'Hà Nội', N'100003', N'Việt Nam', 1),
(11, N'Shipping', N'155 Nguyễn Thiện Thuật', N'HCM', N'HCM', N'700004', N'Việt Nam', 1),
(12, N'Shipping', N'166 Trần Phú', N'Huế', N'Thừa Thiên', N'530000', N'Việt Nam', 1),
(13, N'Shipping', N'177 Nguyễn Văn Cừ', N'Hà Nội', N'Hà Nội', N'100004', N'Việt Nam', 1),
(14, N'Shipping', N'188 Lê Duẩn', N'Đà Nẵng', N'Đà Nẵng', N'500003', N'Việt Nam', 1),
(15, N'Shipping', N'199 Nguyễn Chí Thanh', N'HCM', N'HCM', N'700005', N'Việt Nam', 1),
(16, N'Shipping', N'200 Lý Tự Trọng', N'HCM', N'HCM', N'700006', N'Việt Nam', 1),
(17, N'Shipping', N'210 Lê Thánh Tôn', N'HCM', N'HCM', N'700007', N'Việt Nam', 1),
(18, N'Shipping', N'220 Võ Văn Kiệt', N'HCM', N'HCM', N'700008', N'Việt Nam', 1),
(19, N'Shipping', N'230 Phan Chu Trinh', N'Hà Nội', N'Hà Nội', N'100005', N'Việt Nam', 1),
(20, N'Shipping', N'240 Hùng Vương', N'Huế', N'Thừa Thiên', N'530001', N'Việt Nam', 1);
SELECT * FROM Address

------------------------------------------------
-- 3. CATEGORY (20 danh mục, có cha – con)
------------------------------------------------
INSERT INTO Category (Name, Slug, ParentCategoryID)
VALUES
(N'Điện tử', N'dien-tu', NULL),
(N'Thời trang', N'thoi-trang', NULL),
(N'Mỹ phẩm', N'my-pham', NULL),
(N'Sách', N'sach', NULL),
(N'Đồ gia dụng', N'do-gia-dung', NULL),
(N'Điện thoại', N'dien-thoai', 1),
(N'Laptop', N'laptop', 1),
(N'TV', N'tv', 1),
(N'Tai nghe', N'tai-nghe', 1),
(N'Đồng hồ', N'dong-ho', 1),
(N'Áo nam', N'ao-nam', 2),
(N'Quần nam', N'quan-nam', 2),
(N'Áo nữ', N'ao-nu', 2),
(N'Váy nữ', N'vay-nu', 2),
(N'Kem dưỡng', N'kem-duong', 3),
(N'Son môi', N'son-moi', 3),
(N'Sữa rửa mặt', N'sua-rua-mat', 3),
(N'Truyện', N'truyen', 4),
(N'Sách học tập', N'sach-hoc-tap', 4),
(N'Nồi cơm điện', N'noi-com', 5);
SELECT * FROM Category


------------------------------------------------
-- 4. PRODUCT (20 sản phẩm)
------------------------------------------------
INSERT INTO Product (SKU, Name, ShortDescription, Description, DefaultCategoryID)
VALUES
(N'P001', N'iPhone 15 Pro Max', N'Flagship Apple', N'Điện thoại cao cấp mới nhất của Apple', 6),
(N'P002', N'Samsung Galaxy S24', N'Android mạnh mẽ', N'Phiên bản mới của Galaxy', 6),
(N'P003', N'Dell XPS 13', N'Laptop siêu mỏng', N'Máy tính dành cho doanh nhân', 7),
(N'P004', N'Asus ZenBook', N'Laptop nhỏ gọn', N'Laptop phù hợp công việc văn phòng', 7),
(N'P005', N'Sony Bravia 65"', N'TV 4K', N'Hình ảnh siêu nét', 8),
(N'P006', N'LG OLED C3', N'TV OLED cao cấp', N'Màu sắc trung thực', 8),
(N'P007', N'AirPods Pro 2', N'Tai nghe Apple', N'Chống ồn chủ động', 9),
(N'P008', N'Sony WH-1000XM5', N'Tai nghe chống ồn', N'Âm thanh cực đỉnh', 9),
(N'P009', N'Casio MTP-1374', N'Đồng hồ nam', N'Chống nước, dây kim loại', 10),
(N'P010', N'Seiko Presage', N'Đồng hồ cơ', N'Thiết kế sang trọng', 10),
(N'P011', N'Áo thun nam trắng', N'Áo cotton', N'Áo mềm mại, thoáng mát', 11),
(N'P012', N'Quần jeans nam', N'Chất bò co giãn', N'Phong cách trẻ trung', 12),
(N'P013', N'Áo sơ mi nữ', N'Áo công sở', N'Áo kiểu sang trọng', 13),
(N'P014', N'Váy công sở', N'Váy nữ thanh lịch', N'Phù hợp đi làm', 14),
(N'P015', N'Kem dưỡng da ban đêm', N'Kem dưỡng ẩm', N'Giúp da mềm mịn', 15),
(N'P016', N'Son môi đỏ Ruby', N'Son lì cao cấp', N'Màu đỏ quyến rũ', 16),
(N'P017', N'Sữa rửa mặt Senka', N'Sạch sâu', N'Làm sạch nhẹ nhàng cho da', 17),
(N'P018', N'Truyện Doraemon', N'Truyện tranh thiếu nhi', N'Hài hước, ý nghĩa', 18),
(N'P019', N'Sách lập trình C#', N'Sách học IT', N'Hướng dẫn chi tiết C# cơ bản', 19),
(N'P020', N'Nồi cơm điện Sharp', N'Nồi điện 1.8L', N'Nấu nhanh, tiết kiệm điện', 20);
SELECT * FROM Product

------------------------------------------------
-- 5. PRODUCT VARIANT (20 mẫu)
------------------------------------------------
INSERT INTO ProductVariant (ProductID, SKU, Attributes, Price)
VALUES
(1, N'V001', N'{"Color":"Blue","Storage":"256GB"}', 32990000),
(2, N'V002', N'{"Color":"Black","Storage":"128GB"}', 25990000),
(3, N'V003', N'{"RAM":"16GB","SSD":"512GB"}', 28990000),
(4, N'V004', N'{"RAM":"8GB","SSD":"256GB"}', 19990000),
(5, N'V005', N'{"Size":"65 inch"}', 25900000),
(6, N'V006', N'{"Size":"55 inch"}', 23900000),
(7, N'V007', N'{"Color":"White"}', 6490000),
(8, N'V008', N'{"Color":"Black"}', 7990000),
(9, N'V009', N'{"Color":"Silver"}', 3500000),
(10, N'V010', N'{"Color":"Gold"}', 7500000),
(11, N'V011', N'{"Size":"M"}', 250000),
(12, N'V012', N'{"Size":"L"}', 300000),
(13, N'V013', N'{"Size":"S"}', 270000),
(14, N'V014', N'{"Size":"M"}', 350000),
(15, N'V015', N'{"Volume":"50ml"}', 450000),
(16, N'V016', N'{"Shade":"Ruby Red"}', 350000),
(17, N'V017', N'{"Volume":"100ml"}', 180000),
(18, N'V018', N'{"Type":"Truyện tranh"}', 65000),
(19, N'V019', N'{"Edition":"2024"}', 120000),
(20, N'V020', N'{"Capacity":"1.8L"}', 890000);
SELECT * FROM ProductVariant


------------------------------------------------
-- 6. PRODUCT IMAGE (20 hình)
------------------------------------------------
INSERT INTO ProductImage (ProductID, VariantID, Url, AltText)
VALUES
(1,1,N'https://cdn2.cellphones.com.vn/insecure/rs:fill:0:0/q:90/plain/https://cellphones.com.vn/media/wysiwyg/Phone/Apple/iphone_15/dien-thoai-iphone-15-plus-8.jpg', N'iPhone 15'),
(2,2,N'https://cdn.tgdd.vn/Products/Images/42/342565/samsung-galaxy-s25-fe-navy-thumbai-600x600.jpg', N'Samsung Galaxy'),
(3,3,N'https://cdnv2.tgdd.vn/mwg-static/tgdd/Products/Images/44/333106/dell-xps-13-9340-ultra-7-hxrgt2-1-638708028116982814-750x500.jpg',N'Dell XPS 13'),
(4,4,N'https://nguyencongpc.vn/media/product/23109-up5401za-kn101w.png',N'Asus ZenBook'),
(5,5,N'https://logico.com.vn/images/products/2022/04/07/original/32w830k-3_1649317795.png',N'Sony Bravia'),
(6,6,N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTJF-X-pdf7HB5XyOAibMoI7CsS9soJlMFNQw&s',N'LG OLED'),
(7,7,N'https://cdn.tgdd.vn/Products/Images/54/315014/s16/tai-nghe-bluetooth-airpods-pro-2nd-gen-usb-c-charge-apple-thumb-12-1-650x650.png',N'AirPods'),
(8,8,N'https://tainghetot.com/wp-content/uploads/2022/06/1_WH-1000XM5_standard_black-Large-scaled.jpg',N'Sony WH1000'),
(9,9,N'https://www.casio.com/content/dam/casio/product-info/locales/vn/vi/timepiece/product/watch/G/GM/GM5/gm-5600rh-1/assets/GM-5600RH-1.png.transform/product-panel/image.png',N'Casio Watch'),
(10,10,N'https://image.donghohaitrieu.com/wp-content/uploads/2017/12/SRPB41J12.jpg',N'Seiko'),
(11,11,N'https://product.hstatic.net/1000312752/product/548e103d31d2952b748f18f04406a434_37d5015da5cd409790f296c97c279909_89c4cdbacbc647a9a61c1b22fb4fa7b4.png',N'Áo nam'),
(12,12,N'https://badass.vn/wp-content/uploads/2024/06/QJDTK502-2.jpg',N'Quần jeans'),
(13,13,N'https://milvus.com.vn/wp-content/uploads/2023/08/BRBS028WPINK-400x500.png',N'Áo nữ'),
(14,14,N'https://ecochic.vn/wp-content/uploads/2024/07/chan-vay-tag-sat-ecochic-xam.png',N'Váy'),
(15,15,N'https://product.hstatic.net/200000724049/product/bibala1__1__73e6031ea76146db8b66d557f254e978_master.png',N'Kem dưỡng'),
(16,16,N'https://product.hstatic.net/1000025647/product/_ximal_matte_683_cafe_mocha_c7dd302c6271426d964e42c102276117_1024x1024_1afaf33662e54db29f03c5e439d1344f.png',N'Son môi'),
(17,17,N'https://production-cdn.pharmacity.io/digital/1080x1080/plain/e-com/images/ecommerce/P22162_4.jpg',N'Sữa rửa mặt'),
(18,18,N'https://nhasachmienphi.com/images/thumbnail/nhasachmienphi-truyen-tranh-doremon.jpg',N'Truyện Doraemon'),
(19,19,N'https://images.nxbbachkhoa.vn/Picture/2024/5/8/image-20240508180323597.jpg',N'Sách C#'),
(20,20,N'https://satovietnhat.com.vn/Upload/avatar/sato-30s039.jpg',N'Nồi cơm điện');
SELECT * FROM ProductImage

------------------------------------------------
-- 7. INVENTORY (20 dòng)
------------------------------------------------
INSERT INTO Inventory (VariantID, QuantityAvailable, QuantityReserved)
SELECT VariantID, 50, 5 FROM ProductVariant;
SELECT * FROM Inventory

------------------------------------------------
-- 8. CART (20 giỏ hàng)
------------------------------------------------
INSERT INTO Cart (CustomerID)
SELECT CustomerID FROM Customer;
SELECT * FROM Cart

------------------------------------------------
-- 9. CART ITEM (20 sản phẩm trong giỏ)
------------------------------------------------
INSERT INTO CartItem (CartID, VariantID, Quantity, UnitPrice)
VALUES
(1,1,1,32990000),
(2,2,1,25990000),
(3,3,1,28990000),
(4,4,1,19990000),
(5,5,1,25900000),
(6,6,1,23900000),
(7,7,1,6490000),
(8,8,1,7990000),
(9,9,1,3500000),
(10,10,1,7500000),
(11,11,2,250000),
(12,12,1,300000),
(13,13,1,270000),
(14,14,1,350000),
(15,15,1,450000),
(16,16,1,350000),
(17,17,2,180000),
(18,18,1,65000),
(19,19,1,120000),
(20,20,1,890000);
SELECT * FROM CartItem

------------------------------------------------
-- 10. ORDER + ORDER ITEM (20 đơn hàng)
------------------------------------------------
INSERT INTO OrderTbl (CustomerID, Status, BillingAddressID, ShippingAddressID, SubTotal, ShippingFee, TaxAmount, DiscountAmount, TotalAmount)
SELECT CustomerID, N'Paid', CustomerID, CustomerID, 1000000, 30000, 0, 0, 1030000 FROM Customer;
SELECT * FROM OrderTbl

INSERT INTO OrderItem (OrderID, VariantID, SKU, ProductName, Quantity, UnitPrice, TotalPrice)
SELECT OrderID, VariantID, SKU, N'Sản phẩm mẫu', 1, Price, Price FROM ProductVariant JOIN OrderTbl ON OrderTbl.OrderID <= 20;
SELECT * FROM OrderItem

------------------------------------------------
-- 11. PAYMENT (20)
------------------------------------------------
INSERT INTO Payment (OrderID, PaymentMethod, Amount, Status, TransactionRef, PaidAt)
SELECT OrderID, N'Credit Card', TotalAmount, N'Completed', CONCAT('TX', OrderID), GETDATE()
FROM OrderTbl;
SELECT * FROM Payment

------------------------------------------------
-- 12. SHIPMENT (20)
------------------------------------------------
INSERT INTO Shipment (OrderID, Carrier, TrackingNumber, ShippedAt, Status)
SELECT OrderID, N'VNPost', CONCAT('TRK', OrderID, 'VN'), GETDATE(), N'Shipped'
FROM OrderTbl;
SELECT * FROM Shipment

------------------------------------------------
-- 13. COUPON (20)
------------------------------------------------
INSERT INTO Coupon (Code, Type, Value, StartDate, EndDate, MinOrderAmount, UsageLimit)
VALUES
(N'CP01','percent',10,GETDATE(),DATEADD(DAY,30,GETDATE()),500000,100),
(N'CP02','percent',15,GETDATE(),DATEADD(DAY,60,GETDATE()),1000000,100),
(N'CP03','fixed',50000,GETDATE(),DATEADD(DAY,90,GETDATE()),NULL,50),
(N'CP04','percent',5,GETDATE(),DATEADD(DAY,20,GETDATE()),NULL,500),
(N'CP05','percent',20,GETDATE(),DATEADD(DAY,30,GETDATE()),2000000,50),
(N'CP06','fixed',100000,GETDATE(),DATEADD(DAY,10,GETDATE()),500000,30),
(N'CP07','percent',25,GETDATE(),DATEADD(DAY,45,GETDATE()),3000000,20),
(N'CP08','percent',8,GETDATE(),DATEADD(DAY,50,GETDATE()),NULL,80),
(N'CP09','fixed',30000,GETDATE(),DATEADD(DAY,15,GETDATE()),NULL,300),
(N'CP10','percent',10,GETDATE(),DATEADD(DAY,40,GETDATE()),1000000,100),
(N'CP11','fixed',40000,GETDATE(),DATEADD(DAY,30,GETDATE()),NULL,90),
(N'CP12','percent',12,GETDATE(),DATEADD(DAY,25,GETDATE()),NULL,60),
(N'CP13','fixed',20000,GETDATE(),DATEADD(DAY,35,GETDATE()),NULL,80),
(N'CP14','percent',18,GETDATE(),DATEADD(DAY,90,GETDATE()),2000000,40),
(N'CP15','percent',30,GETDATE(),DATEADD(DAY,60,GETDATE()),3000000,25),
(N'CP16','fixed',10000,GETDATE(),DATEADD(DAY,20,GETDATE()),NULL,120),
(N'CP17','percent',22,GETDATE(),DATEADD(DAY,50,GETDATE()),2500000,60),
(N'CP18','fixed',60000,GETDATE(),DATEADD(DAY,40,GETDATE()),NULL,70),
(N'CP19','percent',5,GETDATE(),DATEADD(DAY,30,GETDATE()),NULL,500),
(N'CP20','percent',15,GETDATE(),DATEADD(DAY,20,GETDATE()),1000000,200);
SELECT * FROM Coupon

------------------------------------------------
-- 14. REVIEW (20)
------------------------------------------------
INSERT INTO Review (ProductID, CustomerID, Rating, Title, Content, IsApproved)
SELECT TOP 20 ProductID, CustomerID, (ABS(CHECKSUM(NEWID())) % 5) + 1,
N'Đánh giá sản phẩm',
N'Sản phẩm dùng tốt, hài lòng.', 1
FROM Product CROSS JOIN Customer;
SELECT * FROM Review
