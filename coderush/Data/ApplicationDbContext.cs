using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HedgeLinks.Models;

namespace HedgeLinks.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<HedgeLinks.Models.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<HedgeLinks.Models.Bill> Bill { get; set; }

        public DbSet<HedgeLinks.Models.BillType> BillType { get; set; }

        public DbSet<HedgeLinks.Models.Branch> Branch { get; set; }

        public DbSet<HedgeLinks.Models.CashBank> CashBank { get; set; }

        public DbSet<HedgeLinks.Models.Currency> Currency { get; set; }

        public DbSet<HedgeLinks.Models.Customer> Customer { get; set; }

        public DbSet<HedgeLinks.Models.CustomerType> CustomerType { get; set; }

        public DbSet<HedgeLinks.Models.GoodsReceivedNote> GoodsReceivedNote { get; set; }

        public DbSet<HedgeLinks.Models.Invoice> Invoice { get; set; }

        public DbSet<HedgeLinks.Models.InvoiceType> InvoiceType { get; set; }

        public DbSet<HedgeLinks.Models.NumberSequence> NumberSequence { get; set; }

        public DbSet<HedgeLinks.Models.PaymentReceive> PaymentReceive { get; set; }

        public DbSet<HedgeLinks.Models.PaymentType> PaymentType { get; set; }

        public DbSet<HedgeLinks.Models.PaymentVoucher> PaymentVoucher { get; set; }

        public DbSet<HedgeLinks.Models.Product> Product { get; set; }

        public DbSet<ProductType> ProductType { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }

        public DbSet<PurchaseOrderLine> PurchaseOrderLine { get; set; }

        public DbSet<PurchaseType> PurchaseType { get; set; }

        public DbSet<SalesOrder> SalesOrder { get; set; }

        public DbSet<SalesOrderLine> SalesOrderLine { get; set; }

        public DbSet<SalesType> SalesType { get; set; }

        public DbSet<Shipment> Shipment { get; set; }

        public DbSet<ShipmentType> ShipmentType { get; set; }

        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }

        public DbSet<Vendor> Vendor { get; set; }

        public DbSet<VendorType> VendorType { get; set; }

        public DbSet<Warehouse> Warehouse { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Menubar> Menubar{ get; set; }
        public DbSet<MenuPath> MenuPath{ get; set; }
        public DbSet<SubMenu> Submenu{ get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<ArticleTopic> ArticleTopic { get; set; }
        public DbSet<ComercialTips> ComercialTips { get; set; }
        public DbSet<ThirdSection> ThirdSection { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<JobIndustry> JobIndustries { get; set; }
        public DbSet<JobType> JobType { get; set; }
        public DbSet<Country> Country{ get; set; }
        public DbSet<State> State{ get; set; }
        public DbSet<City> City{ get; set; }
        public DbSet<JobSeekers> JobSeeker { get; set; }
        public DbSet<JobSeekerDetail> JobSeekerDetail{ get; set; }
        public DbSet<HedgeLinks.Models.TopImage> TopImage { get; set; }
    }
}
