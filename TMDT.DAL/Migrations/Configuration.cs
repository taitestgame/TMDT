namespace TMDT.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<TMDT.DAL.Model1>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TMDT.DAL.Model1 context)
        {
            // Admin account
            if (!context.Customers.Any(c => c.UserName == "admin"))
            {
                context.Customers.Add(new Customer
                {
                    UserName = "admin",
                    Password = "admin123",
                    Email = "admin@shop.com",
                    FullName = "Quản trị hệ thống",
                    Phone = "0900000000",
                    CreatedAt = DateTime.Now, 
                    IsAdmin = true
                });
                context.SaveChanges();
            }

            // Customers (20)
            var customers = new List<Customer>
            {
                new Customer{ UserName = "user1", Password = "123", Email = "user1@mail.com", FullName = "Nguyễn Văn A", Phone = "0901111111", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user2", Password = "1234", Email = "user2@mail.com", FullName = "Trần Thị B", Phone = "0902222222", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user3", Password = "234", Email = "user3@mail.com", FullName = "Lê Văn C", Phone = "0903333333", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user4", Password = "2345", Email = "user4@mail.com", FullName = "Phạm Thị D", Phone = "0904444444", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user5", Password = "345", Email = "user5@mail.com", FullName = "Hoàng Văn E", Phone = "0905555555", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user6", Password = "3456", Email = "user6@mail.com", FullName = "Đỗ Thị F", Phone = "0906666666", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user7", Password = "321", Email = "user7@mail.com", FullName = "Vũ Văn G", Phone = "0907777777", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user8", Password = "3212", Email = "user8@mail.com", FullName = "Phan Thị H", Phone = "0908888888", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user9", Password = "231", Email = "user9@mail.com", FullName = "Bùi Văn I", Phone = "0909999999", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user10", Password = "23121", Email = "user10@mail.com", FullName = "Đặng Thị J", Phone = "0910000000", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user11", Password = "653", Email = "user11@mail.com", FullName = "Lý Văn K", Phone = "0911111111", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user12", Password = "9867", Email = "user12@mail.com", FullName = "Phạm Minh L", Phone = "0912222222", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user13", Password = "068", Email = "user13@mail.com", FullName = "Trần Hữu M", Phone = "0913333333", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user14", Password = "1045", Email = "user14@mail.com", FullName = "Lưu Thị N", Phone = "0914444444", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user15", Password = "961", Email = "user15@mail.com", FullName = "Đỗ Văn O", Phone = "0915555555", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user16", Password = "009", Email = "user16@mail.com", FullName = "Trương Thị P", Phone = "0916666666", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user17", Password = "008", Email = "user17@mail.com", FullName = "Lê Văn Q", Phone = "0917777777", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user18", Password = "007", Email = "user18@mail.com", FullName = "Nguyễn Thị R", Phone = "0918888888", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user19", Password = "006", Email = "user19@mail.com", FullName = "Phan Văn S", Phone = "0919999999", CreatedAt = DateTime.Now, IsAdmin = false },
                new Customer{ UserName = "user20", Password = "005", Email = "user20@mail.com", FullName = "Bùi Thị T", Phone = "0920000000", CreatedAt = DateTime.Now, IsAdmin = false }
            };
            foreach (var c in customers)
            {
                var exists = context.Customers.Any(x => x.UserName == c.UserName || x.Email == c.Email);
                if (!exists) context.Customers.Add(c);
            }
            context.SaveChanges();

            // Categories
            var categoryNames = new[] { "Điện tử", "Thời trang", "Mỹ phẩm", "Sách", "Đồ gia dụng", "Điện thoại", "Laptop", "TV", "Tai nghe", "Đồng hồ", "Áo nam", "Quần nam", "Áo nữ", "Váy nữ", "Kem dưỡng", "Son môi", "Sữa rửa mặt", "Truyện", "Sách học tập", "Nồi cơm điện" };
            foreach (var name in categoryNames)
            {
                if (!context.Categories.Any(c => c.Name == name))
                {
                    context.Categories.Add(new Category { Name = name, Slug = ToSlug(name), SortOrder = 0, IsActive = true });
                }
            }
            context.SaveChanges();

            // Set parent relationships
            SetParent(context, "Điện thoại", "Điện tử");
            SetParent(context, "Laptop", "Điện tử");
            SetParent(context, "TV", "Điện tử");
            SetParent(context, "Tai nghe", "Điện tử");
            SetParent(context, "Đồng hồ", "Điện tử");
            SetParent(context, "Áo nam", "Thời trang");
            SetParent(context, "Quần nam", "Thời trang");
            SetParent(context, "Áo nữ", "Thời trang");
            SetParent(context, "Váy nữ", "Thời trang");
            context.SaveChanges();

            // Products (subset identical to SQL)
            AddProduct(context, "P001", "iPhone 15 Pro Max",  "Điện thoại cao cấp mới nhất của Apple", "Điện thoại");
            AddProduct(context, "P002", "Samsung Galaxy S24",  "Phiên bản mới của Galaxy", "Điện thoại");
            AddProduct(context, "P003", "Dell XPS 13",         "Máy tính dành cho doanh nhân", "Laptop");
            AddProduct(context, "P004", "Asus ZenBook",        "Laptop phù hợp công việc văn phòng", "Laptop");
            AddProduct(context, "P005", "Sony Bravia 65\"",   "Hình ảnh siêu nét", "TV");
            AddProduct(context, "P006", "LG OLED C3",          "Màu sắc trung thực", "TV");
            AddProduct(context, "P007", "AirPods Pro 2",       "Chống ồn chủ động", "Tai nghe");
            AddProduct(context, "P008", "Sony WH-1000XM5",     "Âm thanh cực đỉnh", "Tai nghe");
            AddProduct(context, "P009", "Casio MTP-1374",      "Chống nước, dây kim loại", "Đồng hồ");
            AddProduct(context, "P010", "Seiko Presage",       "Thiết kế sang trọng", "Đồng hồ");
            AddProduct(context, "P011", "Áo thun nam trắng",   "Áo cotton", "Áo nam");
            AddProduct(context, "P012", "Quần jeans nam",      "Phong cách trẻ trung", "Quần nam");
            AddProduct(context, "P013", "Áo sơ mi nữ",         "Áo kiểu sang trọng", "Áo nữ");
            AddProduct(context, "P014", "Váy công sở",         "Phù hợp đi làm", "Váy nữ");
            AddProduct(context, "P015", "Kem dưỡng da ban đêm", "Giúp da mềm mịn", "Kem dưỡng");
            AddProduct(context, "P016", "Son môi đỏ Ruby",     "Màu đỏ quyến rũ", "Son môi");
            AddProduct(context, "P017", "Sữa rửa mặt Senka",   "Làm sạch nhẹ nhàng cho da", "Sữa rửa mặt");
            AddProduct(context, "P018", "Truyện Doraemon",     "Hài hước, ý nghĩa", "Truyện");
            AddProduct(context, "P019", "Sách lập trình C#",   "Hướng dẫn chi tiết C# cơ bản", "Sách học tập");
            AddProduct(context, "P020", "Nồi cơm điện Sharp",  "Nấu nhanh, tiết kiệm điện", "Nồi cơm điện");
            context.SaveChanges();

            // Variants (subset with prices)
            AddVariant(context, "P001", "V001", "{\"Color\":\"Blue\",\"Storage\":\"256GB\"}", 32990000m);
            AddVariant(context, "P002", "V002", "{\"Color\":\"Black\",\"Storage\":\"128GB\"}", 25990000m);
            AddVariant(context, "P003", "V003", "{\"RAM\":\"16GB\",\"SSD\":\"512GB\"}", 28990000m);
            AddVariant(context, "P004", "V004", "{\"RAM\":\"8GB\",\"SSD\":\"256GB\"}", 19990000m);
            AddVariant(context, "P005", "V005", "{\"Size\":\"65 inch\"}", 25900000m);
            AddVariant(context, "P006", "V006", "{\"Size\":\"55 inch\"}", 23900000m);
            AddVariant(context, "P007", "V007", "{\"Color\":\"White\"}", 6490000m);
            AddVariant(context, "P008", "V008", "{\"Color\":\"Black\"}", 7990000m);
            AddVariant(context, "P009", "V009", "{\"Color\":\"Silver\"}", 3500000m);
            AddVariant(context, "P010", "V010", "{\"Color\":\"Gold\"}", 7500000m);
            AddVariant(context, "P011", "V011", "{\"Size\":\"M\"}", 250000m);
            AddVariant(context, "P012", "V012", "{\"Size\":\"L\"}", 300000m);
            AddVariant(context, "P013", "V013", "{\"Size\":\"S\"}", 270000m);
            AddVariant(context, "P014", "V014", "{\"Size\":\"M\"}", 350000m);
            AddVariant(context, "P015", "V015", "{\"Volume\":\"50ml\"}", 450000m);
            AddVariant(context, "P016", "V016", "{\"Shade\":\"Ruby Red\"}", 350000m);
            AddVariant(context, "P017", "V017", "{\"Volume\":\"100ml\"}", 180000m);
            AddVariant(context, "P018", "V018", "{\"Type\":\"Truyện tranh\"}", 65000m);
            AddVariant(context, "P019", "V019", "{\"Edition\":\"2024\"}", 120000m);
            AddVariant(context, "P020", "V020", "{\"Capacity\":\"1.8L\"}", 890000m);
            context.SaveChanges();

            // Simple inventory for each variant if missing
            var variants = context.ProductVariants.ToList();
            foreach (var v in variants)
            {
                if (!context.Inventories.Any(i => i.VariantID == v.VariantID || i.VariantID == v.VariantID))
                {
                    context.Inventories.Add(new Inventory { VariantID = v.VariantID, QuantityAvailable = 50, QuantityReserved = 5, LastUpdated = DateTime.Now });
                }
            }
            context.SaveChanges();

            // Addresses (one shipping per first 20 customers)
            var allCustomers = context.Customers.Where(c => c.UserName.StartsWith("user")).OrderBy(c => c.CustomerID).ToList();
            var addressSeeds = new[]
            {
                new { Street = "12 Nguyễn Huệ", City = "Hồ Chí Minh", State = "HCM", Postal = "700000" },
                new { Street = "22 Trần Hưng Đạo", City = "Hà Nội", State = "Hà Nội", Postal = "100000" },
                new { Street = "34 Nguyễn Trãi", City = "Đà Nẵng", State = "Đà Nẵng", Postal = "500000" },
                new { Street = "56 Hai Bà Trưng", City = "Hà Nội", State = "Hà Nội", Postal = "100001" },
                new { Street = "78 Pasteur", City = "Hồ Chí Minh", State = "HCM", Postal = "700002" },
                new { Street = "90 Nguyễn Văn Linh", City = "Đà Nẵng", State = "Đà Nẵng", Postal = "500001" },
                new { Street = "111 Lý Thường Kiệt", City = "Hà Nội", State = "Hà Nội", Postal = "100002" },
                new { Street = "122 Bạch Đằng", City = "Đà Nẵng", State = "Đà Nẵng", Postal = "500002" },
                new { Street = "133 Cách Mạng Tháng 8", City = "HCM", State = "HCM", Postal = "700003" },
                new { Street = "144 Điện Biên Phủ", City = "Hà Nội", State = "Hà Nội", Postal = "100003" },
                new { Street = "155 Nguyễn Thiện Thuật", City = "HCM", State = "HCM", Postal = "700004" },
                new { Street = "166 Trần Phú", City = "Huế", State = "Thừa Thiên", Postal = "530000" },
                new { Street = "177 Nguyễn Văn Cừ", City = "Hà Nội", State = "Hà Nội", Postal = "100004" },
                new { Street = "188 Lê Duẩn", City = "Đà Nẵng", State = "Đà Nẵng", Postal = "500003" },
                new { Street = "199 Nguyễn Chí Thanh", City = "HCM", State = "HCM", Postal = "700005" },
                new { Street = "200 Lý Tự Trọng", City = "HCM", State = "HCM", Postal = "700006" },
                new { Street = "210 Lê Thánh Tôn", City = "HCM", State = "HCM", Postal = "700007" },
                new { Street = "220 Võ Văn Kiệt", City = "HCM", State = "HCM", Postal = "700008" },
                new { Street = "230 Phan Chu Trinh", City = "Hà Nội", State = "Hà Nội", Postal = "100005" },
                new { Street = "240 Hùng Vương", City = "Huế", State = "Thừa Thiên", Postal = "530001" },
            };
            for (int i = 0; i < Math.Min(allCustomers.Count, addressSeeds.Length); i++)
            {
                var cust = allCustomers[i];
                var a = addressSeeds[i];
                bool hasAddress = context.Addresses.Any(x => x.CustomerID == cust.CustomerID && x.Street == a.Street);
                if (!hasAddress)
                {
                    context.Addresses.Add(new Address
                    {
                        CustomerID = cust.CustomerID,
                        Type = "Shipping",
                        Street = a.Street,
                        City = a.City,
                        State = a.State,
                        PostalCode = a.Postal,
                        Country = "Việt Nam",
                        IsDefault = true
                    });
                }
            }
            context.SaveChanges();

            // Carts for each customer
            foreach (var c in context.Customers.ToList())
            {
                if (!context.Carts.Any(k => k.CustomerID == c.CustomerID))
                {
                    context.Carts.Add(new Cart { CustomerID = c.CustomerID, CreatedAt = DateTime.Now });
                }
            }
                context.SaveChanges();
            }

        private static string ToSlug(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;
            var s = input.Trim().ToLowerInvariant();
            s = s.Replace(" ", "-").Replace("\"", "");
            return s;
        }

        private static void SetParent(Model1 context, string childName, string parentName)
        {
            var child = context.Categories.FirstOrDefault(c => c.Name == childName);
            var parent = context.Categories.FirstOrDefault(c => c.Name == parentName);
            if (child != null && parent != null && child.ParentCategoryID != parent.CategoryID)
            {
                child.ParentCategoryID = parent.CategoryID;
            }
        }

        private static void AddProduct(Model1 context, string sku, string name, string shortDesc, string categoryName)
        {
            var category = context.Categories.FirstOrDefault(c => c.Name == categoryName);
            if (category == null) return;
            var existing = context.Products.FirstOrDefault(p => p.SKU == sku);
            if (existing == null)
            {
                context.Products.Add(new Product
                {
                    SKU = sku,
                    Name = name,
                    ShortDescription = shortDesc,
                    Description = shortDesc,
                    DefaultCategoryID = category.CategoryID,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                });
            }
        }

        private static void AddVariant(Model1 context, string productSku, string variantSku, string attributes, decimal price)
        {
            var product = context.Products.FirstOrDefault(p => p.SKU == productSku);
            if (product == null) return;
            var existing = context.ProductVariants.FirstOrDefault(v => v.SKU == variantSku);
            if (existing == null)
            {
                context.ProductVariants.Add(new ProductVariant
                {
                    ProductID = product.ProductID,
                    SKU = variantSku,
                    Attributes = attributes,
                    Price = price,
                    IsActive = true
                });
            }
        }
    }
}
