namespace TMDT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsAdminToCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        Type = c.String(maxLength: 20),
                        Street = c.String(maxLength: 300),
                        City = c.String(maxLength: 100),
                        State = c.String(maxLength: 100),
                        PostalCode = c.String(maxLength: 20),
                        Country = c.String(maxLength: 100),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AddressID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 100),
                        Password = c.String(maxLength: 100, unicode: false),
                        Email = c.String(nullable: false, maxLength: 200),
                        FullName = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 30),
                        CreatedAt = c.DateTime(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        CartID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CartID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.CartItem",
                c => new
                    {
                        CartItemID = c.Int(nullable: false, identity: true),
                        CartID = c.Int(nullable: false),
                        VariantID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.CartItemID)
                .ForeignKey("dbo.ProductVariant", t => t.VariantID)
                .ForeignKey("dbo.Cart", t => t.CartID)
                .Index(t => t.CartID)
                .Index(t => t.VariantID);
            
            CreateTable(
                "dbo.ProductVariant",
                c => new
                    {
                        VariantID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        SKU = c.String(maxLength: 100),
                        Attributes = c.String(),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VariantID)
                .ForeignKey("dbo.Product", t => t.ProductID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Inventory",
                c => new
                    {
                        InventoryID = c.Int(nullable: false, identity: true),
                        VariantID = c.Int(nullable: false),
                        QuantityAvailable = c.Int(nullable: false),
                        QuantityReserved = c.Int(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryID)
                .ForeignKey("dbo.ProductVariant", t => t.VariantID)
                .Index(t => t.VariantID);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        OrderItemID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        VariantID = c.Int(nullable: false),
                        SKU = c.String(maxLength: 100),
                        ProductName = c.String(maxLength: 300),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, storeType: "money"),
                        TotalPrice = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.OrderItemID)
                .ForeignKey("dbo.OrderTbl", t => t.OrderID)
                .ForeignKey("dbo.ProductVariant", t => t.VariantID)
                .Index(t => t.OrderID)
                .Index(t => t.VariantID);
            
            CreateTable(
                "dbo.OrderTbl",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 50),
                        BillingAddressID = c.Int(),
                        ShippingAddressID = c.Int(),
                        SubTotal = c.Decimal(nullable: false, storeType: "money"),
                        ShippingFee = c.Decimal(nullable: false, storeType: "money"),
                        TaxAmount = c.Decimal(nullable: false, storeType: "money"),
                        DiscountAmount = c.Decimal(nullable: false, storeType: "money"),
                        TotalAmount = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .ForeignKey("dbo.Address", t => t.BillingAddressID)
                .ForeignKey("dbo.Address", t => t.ShippingAddressID)
                .Index(t => t.CustomerID)
                .Index(t => t.BillingAddressID)
                .Index(t => t.ShippingAddressID);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        PaymentMethod = c.String(maxLength: 50),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        Status = c.String(maxLength: 50),
                        TransactionRef = c.String(maxLength: 200),
                        PaidAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.OrderTbl", t => t.OrderID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.Shipment",
                c => new
                    {
                        ShipmentID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        Carrier = c.String(maxLength: 100),
                        TrackingNumber = c.String(maxLength: 200),
                        ShippedAt = c.DateTime(),
                        DeliveredAt = c.DateTime(),
                        Status = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ShipmentID)
                .ForeignKey("dbo.OrderTbl", t => t.OrderID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        SKU = c.String(maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 300),
                        ShortDescription = c.String(maxLength: 500),
                        Description = c.String(),
                        DefaultCategoryID = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Category", t => t.DefaultCategoryID)
                .Index(t => t.DefaultCategoryID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Slug = c.String(maxLength: 200),
                        ParentCategoryID = c.Int(),
                        SortOrder = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Category", t => t.ParentCategoryID)
                .Index(t => t.ParentCategoryID);
            
            CreateTable(
                "dbo.ProductImage",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(),
                        VariantID = c.Int(),
                        Url = c.String(nullable: false, maxLength: 1000),
                        AltText = c.String(maxLength: 300),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.Product", t => t.ProductID)
                .ForeignKey("dbo.ProductVariant", t => t.VariantID)
                .Index(t => t.ProductID)
                .Index(t => t.VariantID);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Title = c.String(maxLength: 200),
                        Content = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Product", t => t.ProductID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .Index(t => t.ProductID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Coupon",
                c => new
                    {
                        CouponID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 20),
                        Value = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        MinOrderAmount = c.Decimal(storeType: "money"),
                        UsageLimit = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CouponID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderTbl", "ShippingAddressID", "dbo.Address");
            DropForeignKey("dbo.OrderTbl", "BillingAddressID", "dbo.Address");
            DropForeignKey("dbo.Review", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.OrderTbl", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Cart", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.CartItem", "CartID", "dbo.Cart");
            DropForeignKey("dbo.Review", "ProductID", "dbo.Product");
            DropForeignKey("dbo.ProductVariant", "ProductID", "dbo.Product");
            DropForeignKey("dbo.ProductImage", "VariantID", "dbo.ProductVariant");
            DropForeignKey("dbo.ProductImage", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Product", "DefaultCategoryID", "dbo.Category");
            DropForeignKey("dbo.Category", "ParentCategoryID", "dbo.Category");
            DropForeignKey("dbo.OrderItem", "VariantID", "dbo.ProductVariant");
            DropForeignKey("dbo.Shipment", "OrderID", "dbo.OrderTbl");
            DropForeignKey("dbo.Payment", "OrderID", "dbo.OrderTbl");
            DropForeignKey("dbo.OrderItem", "OrderID", "dbo.OrderTbl");
            DropForeignKey("dbo.Inventory", "VariantID", "dbo.ProductVariant");
            DropForeignKey("dbo.CartItem", "VariantID", "dbo.ProductVariant");
            DropForeignKey("dbo.Address", "CustomerID", "dbo.Customer");
            DropIndex("dbo.Review", new[] { "CustomerID" });
            DropIndex("dbo.Review", new[] { "ProductID" });
            DropIndex("dbo.ProductImage", new[] { "VariantID" });
            DropIndex("dbo.ProductImage", new[] { "ProductID" });
            DropIndex("dbo.Category", new[] { "ParentCategoryID" });
            DropIndex("dbo.Product", new[] { "DefaultCategoryID" });
            DropIndex("dbo.Shipment", new[] { "OrderID" });
            DropIndex("dbo.Payment", new[] { "OrderID" });
            DropIndex("dbo.OrderTbl", new[] { "ShippingAddressID" });
            DropIndex("dbo.OrderTbl", new[] { "BillingAddressID" });
            DropIndex("dbo.OrderTbl", new[] { "CustomerID" });
            DropIndex("dbo.OrderItem", new[] { "VariantID" });
            DropIndex("dbo.OrderItem", new[] { "OrderID" });
            DropIndex("dbo.Inventory", new[] { "VariantID" });
            DropIndex("dbo.ProductVariant", new[] { "ProductID" });
            DropIndex("dbo.CartItem", new[] { "VariantID" });
            DropIndex("dbo.CartItem", new[] { "CartID" });
            DropIndex("dbo.Cart", new[] { "CustomerID" });
            DropIndex("dbo.Address", new[] { "CustomerID" });
            DropTable("dbo.Coupon");
            DropTable("dbo.Review");
            DropTable("dbo.ProductImage");
            DropTable("dbo.Category");
            DropTable("dbo.Product");
            DropTable("dbo.Shipment");
            DropTable("dbo.Payment");
            DropTable("dbo.OrderTbl");
            DropTable("dbo.OrderItem");
            DropTable("dbo.Inventory");
            DropTable("dbo.ProductVariant");
            DropTable("dbo.CartItem");
            DropTable("dbo.Cart");
            DropTable("dbo.Customer");
            DropTable("dbo.Address");
        }
    }
}
