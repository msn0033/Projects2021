using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_CheckHafza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HafzaName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BankAccNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HafzaDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_CheckHafza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_CheckHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChkNum = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TransID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_CheckHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_CheckLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckLocationEN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CheckLocationAR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_CheckLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_CheckStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckStatusEN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CheckStatusAR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_CheckStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Settings_Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Settings_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Settings_ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Settings_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Settings_UnitMeasure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitMeasureName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Settings_UnitMeasure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Expense_ExpenseType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseTypeName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Expense_ExpenseType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_GL_AccountChartCounter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountTypeAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountCategory = table.Column<int>(type: "int", nullable: false),
                    ParentAcNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    BalanceSheet = table.Column<bool>(type: "bit", nullable: false),
                    IncomeStatement = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_GL_AccountChartCounter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_GL_FinancialPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_GL_FinancialPeriod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_GL_Journal",
                columns: table => new
                {
                    JournalId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EntryDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TransDate = table.Column<DateTime>(type: "Date", nullable: false),
                    TransDes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransCount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemModules = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_GL_Journal", x => x.JournalId);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Settings_Branch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Settings_Branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Settings_Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CurrencyNameAr = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CurrencyAbbrev = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Settings_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Settings_VAT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VatRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Settings_VAT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HR_Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_GL_HistoricalBalance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialPeriodId = table.Column<int>(type: "int", nullable: false),
                    AccNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_GL_HistoricalBalance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_GL_HistoricalBalance_Finance_GL_FinancialPeriod_FinancialPeriodId",
                        column: x => x.FinancialPeriodId,
                        principalTable: "Finance_GL_FinancialPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_GL_AccountChart",
                columns: table => new
                {
                    AccNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AccountNameAr = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AccountNature = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartingBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsParent = table.Column<bool>(type: "bit", nullable: false),
                    ParentAcNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AccTypeId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_GL_AccountChart", x => x.AccNum);
                    table.ForeignKey(
                        name: "FK_Finance_GL_AccountChart_Finance_GL_AccountChartCounter_AccTypeId",
                        column: x => x.AccTypeId,
                        principalTable: "Finance_GL_AccountChartCounter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_GL_AccountChart_Finance_Settings_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Finance_Settings_Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_GL_AccountChart_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_GL_JournalDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    AccNum = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountLocal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Side = table.Column<int>(type: "int", nullable: false),
                    BalanceAfter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    UsedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_GL_JournalDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_GL_JournalDetails_Finance_GL_Journal_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Finance_GL_Journal",
                        principalColumn: "JournalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_GL_JournalDetails_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CRM_Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TaxationCard = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    IsClient = table.Column<bool>(type: "bit", nullable: false),
                    ClientAccNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClientBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsSupplier = table.Column<bool>(type: "bit", nullable: false),
                    SupplierAccNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SupplierBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRM_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CRM_Contacts_Finance_GL_AccountChart_ClientAccNum",
                        column: x => x.ClientAccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CRM_Contacts_Finance_GL_AccountChart_SupplierAccNum",
                        column: x => x.SupplierAccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BarCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    UnitMeasureId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WithSN = table.Column<bool>(type: "bit", nullable: false),
                    StoreSystemEnums = table.Column<int>(type: "int", nullable: false),
                    StoreAccNum = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    PurchaseAccNum = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    StoreLocation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Main_StoreItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_Finance_CurrentAsset_Inventory_Settings_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Finance_CurrentAsset_Inventory_Settings_Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_Finance_CurrentAsset_Inventory_Settings_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "Finance_CurrentAsset_Inventory_Settings_ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_Finance_CurrentAsset_Inventory_Settings_UnitMeasure_UnitMeasureId",
                        column: x => x.UnitMeasureId,
                        principalTable: "Finance_CurrentAsset_Inventory_Settings_UnitMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_Finance_GL_AccountChart_PurchaseAccNum",
                        column: x => x.PurchaseAccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_Finance_GL_AccountChart_StoreAccNum",
                        column: x => x.StoreAccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Expense_ExpenseItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccNum = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ExpenseTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Expense_ExpenseItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_Expense_ExpenseItem_Finance_Expense_ExpenseType_ExpenseTypeId",
                        column: x => x.ExpenseTypeId,
                        principalTable: "Finance_Expense_ExpenseType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Expense_ExpenseItem_Finance_GL_AccountChart_AccNum",
                        column: x => x.AccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CRM_ContactBalanceInCurrency",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    AccNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRM_ContactBalanceInCurrency", x => new { x.ContactId, x.CurrencyId, x.AccNum });
                    table.ForeignKey(
                        name: "FK_CRM_ContactBalanceInCurrency_CRM_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CRM_ContactBalanceInCurrency_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Checks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChkNum = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DueDate = table.Column<DateTime>(type: "Date", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    AmountLocal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountForgin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Paid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    OrginalBank = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BankAccNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CheckHafzaId = table.Column<int>(type: "int", nullable: true),
                    CheckLocationId = table.Column<int>(type: "int", nullable: false),
                    CheckStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Checks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Checks_CRM_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Checks_Finance_CurrentAsset_CheckHafza_CheckHafzaId",
                        column: x => x.CheckHafzaId,
                        principalTable: "Finance_CurrentAsset_CheckHafza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Checks_Finance_CurrentAsset_CheckLocation_CheckLocationId",
                        column: x => x.CheckLocationId,
                        principalTable: "Finance_CurrentAsset_CheckLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Checks_Finance_CurrentAsset_CheckStatus_CheckStatusId",
                        column: x => x.CheckStatusId,
                        principalTable: "Finance_CurrentAsset_CheckStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Checks_Finance_GL_AccountChart_BankAccNum",
                        column: x => x.BankAccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Checks_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentLiabilties_NP_NotesPayable",
                columns: table => new
                {
                    ChkNum = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    WritingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmountLocal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountForgin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    BankAccountNum = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    SupplierId = table.Column<int>(type: "int", maxLength: 15, nullable: false),
                    Paid = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CheckStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentLiabilties_NP_NotesPayable", x => x.ChkNum);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentLiabilties_NP_NotesPayable_CRM_Contacts_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentLiabilties_NP_NotesPayable_Finance_GL_AccountChart_BankAccountNum",
                        column: x => x.BankAccountNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentLiabilties_NP_NotesPayable_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Sales_Invoices",
                columns: table => new
                {
                    InvoiceNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Year = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    InvoiceNumYear = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InvoiceCount = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    IsVAT = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VATAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWithVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Sales_Invoices", x => x.InvoiceNum);
                    table.ForeignKey(
                        name: "FK_Finance_Sales_Invoices_CRM_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Sales_Invoices_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Supplier_Purchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "Date", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsVAT = table.Column<bool>(type: "bit", nullable: false),
                    VATAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmountWithVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsFullyPaid = table.Column<bool>(type: "bit", nullable: false),
                    Paid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Supplier_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_Supplier_Purchases_CRM_Contacts_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Supplier_Purchases_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Expense_ExpenseSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseItemId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentAccNum = table.Column<int>(type: "int", nullable: false),
                    MyProperty = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsVAT = table.Column<bool>(type: "bit", nullable: false),
                    AmountVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountWithVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReferanceAccNUm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    LocalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostCenterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Expense_ExpenseSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_Expense_ExpenseSummary_CRM_Contacts_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Expense_ExpenseSummary_Finance_Expense_ExpenseItem_ExpenseItemId",
                        column: x => x.ExpenseItemId,
                        principalTable: "Finance_Expense_ExpenseItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Expense_ExpenseSummary_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Expense_ExpenseSummary_HR_Department_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "HR_Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChkNum = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    TransId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StatusAfterAction = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory_Finance_CurrentLiabilties_NP_NotesPayable_ChkNum",
                        column: x => x.ChkNum,
                        principalTable: "Finance_CurrentLiabilties_NP_NotesPayable",
                        principalColumn: "ChkNum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Sales_ClientTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    InvoiceNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "Date", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    PaymentAccNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentMethodEnum = table.Column<int>(type: "int", nullable: false),
                    JournalId = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    BalanceAfter = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Sales_ClientTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_Sales_ClientTransaction_CRM_Contacts_ClientId",
                        column: x => x.ClientId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Sales_ClientTransaction_Finance_GL_AccountChart_PaymentAccNum",
                        column: x => x.PaymentAccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Sales_ClientTransaction_Finance_GL_Journal_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Finance_GL_Journal",
                        principalColumn: "JournalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Sales_ClientTransaction_Finance_Sales_Invoices_InvoiceNum",
                        column: x => x.InvoiceNum,
                        principalTable: "Finance_Sales_Invoices",
                        principalColumn: "InvoiceNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Sales_ClientTransaction_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreItemId = table.Column<int>(type: "int", nullable: false),
                    StoreTransTypeEnum = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseId = table.Column<int>(type: "int", nullable: true),
                    QtyBalanceAfter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Main_StoreTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreTransaction_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreTransaction_Finance_Sales_Invoices_InvoiceNum",
                        column: x => x.InvoiceNum,
                        principalTable: "Finance_Sales_Invoices",
                        principalColumn: "InvoiceNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreTransaction_Finance_Supplier_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Finance_Supplier_Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Supplier_SupplierTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    PurchaseId = table.Column<int>(type: "int", nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "Date", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    PaymentAccNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentMethodEnum = table.Column<int>(type: "int", nullable: false),
                    TransId = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    BalanceAfter = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Supplier_SupplierTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_Supplier_SupplierTransaction_CRM_Contacts_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Supplier_SupplierTransaction_Finance_GL_AccountChart_PaymentAccNum",
                        column: x => x.PaymentAccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Supplier_SupplierTransaction_Finance_GL_Journal_TransId",
                        column: x => x.TransId,
                        principalTable: "Finance_GL_Journal",
                        principalColumn: "JournalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Supplier_SupplierTransaction_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Supplier_SupplierTransaction_Finance_Supplier_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Finance_Supplier_Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreItemId = table.Column<int>(type: "int", nullable: false),
                    StoreTransactionId = table.Column<int>(type: "int", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails_Finance_CurrentAsset_Inventory_Main_StoreTransaction_StoreTransa~",
                        column: x => x.StoreTransactionId,
                        principalTable: "Finance_CurrentAsset_Inventory_Main_StoreTransaction",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreItemWithSN",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreItemId = table.Column<int>(type: "int", nullable: false),
                    StoreTransactionId = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Main_StoreItemWithSN", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItemWithSN_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItemWithSN_Finance_CurrentAsset_Inventory_Main_StoreTransaction_StoreTransactionId",
                        column: x => x.StoreTransactionId,
                        principalTable: "Finance_CurrentAsset_Inventory_Main_StoreTransaction",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Finance_CurrentAsset_CheckLocation",
                columns: new[] { "Id", "CheckLocationAR", "CheckLocationEN", "IsDefault" },
                values: new object[,]
                {
                    { 1, "الخزنة", "Safe", true },
                    { 2, "البنك", "Bank", false },
                    { 3, "محصل", "BankCollected", false },
                    { 4, "مع العميل", "Client", false }
                });

            migrationBuilder.InsertData(
                table: "Finance_CurrentAsset_CheckStatus",
                columns: new[] { "Id", "CheckStatusAR", "CheckStatusEN", "IsDefault" },
                values: new object[,]
                {
                    { 1, "تحت التحصيل", "Under Collection", true },
                    { 2, "محصل", "Collection", false },
                    { 3, "رجيع ", "Bounced", false }
                });

            migrationBuilder.InsertData(
                table: "Finance_Expense_ExpenseType",
                columns: new[] { "Id", "ExpenseTypeName" },
                values: new object[,]
                {
                    { 2, "مصروفات بيع/ تسويق" },
                    { 1, "مصروفات انتاج / تشغيل" },
                    { 4, "مصروفات تمويل" },
                    { 3, "مصروفات عمومية وادارية" }
                });

            migrationBuilder.InsertData(
                table: "Finance_GL_AccountChartCounter",
                columns: new[] { "Id", "AccountCategory", "AccountType", "AccountTypeAr", "BalanceSheet", "Count", "IncomeStatement", "ParentAcNum" },
                values: new object[,]
                {
                    { 14, 10, "NotePayable", "شيكات موردين", true, 1, false, "2210000000" },
                    { 23, 50, "OwnerWithdraw", "مسحوبات المالك", true, 0, false, "5120000000" },
                    { 22, 50, "Owners", "حقوق الملكية", true, 0, false, "5110000000" },
                    { 21, 80, "Purchases", "المشتريات", false, 0, true, "4112000000" },
                    { 20, 70, "Expense", "المصروفات", false, 0, true, "4111000000" },
                    { 19, 60, "Income", "الايرادات", false, 0, true, "3110000000" },
                    { 18, 40, "Advances Income", "ايردات مستحقة", true, 0, false, "2250000000" },
                    { 17, 40, "Accrued Expenses", "مصروفات مستحقة", true, 0, false, "2240000000" },
                    { 16, 40, "Creditors", "دائنون اخرون", true, 0, false, "2230000000" },
                    { 15, 40, "Taxes", "ضريبة", true, 0, false, "2220000000" },
                    { 13, 40, "Suppliers", "الموردين", true, 0, false, "2170000000" },
                    { 10, 20, "StaffAdvances", "السلف", true, 0, false, "1262000000" },
                    { 11, 20, "SupplierAdvances", "دفعات مقدمة للموردين", true, 0, false, "1263000000" },
                    { 1, 10, "Building", "مباني", true, 0, false, "1110000000" },
                    { 2, 10, "Machines And Equipments", "اجهزة ومعدات", true, 0, false, "1120000000" },
                    { 12, 20, "OtherCurrentAsset", "مدينون اخرون", true, 0, false, "1269000000" },
                    { 4, 20, "Safe", "خزنة", true, 1, false, "1210000000" },
                    { 5, 20, "Bank", "البنك", true, 0, false, "1220000000" },
                    { 3, 10, "Furniture", "اثاث", true, 0, false, "1130000000" },
                    { 7, 20, "Check", "شيكات", true, 3, false, "1240000000" },
                    { 8, 20, "Store", "المخزن", true, 0, false, "1250000000" },
                    { 9, 20, "Custody", "العهد", true, 0, false, "1261000000" },
                    { 6, 20, "Client", "عملاء", true, 0, false, "1230000000" }
                });

            migrationBuilder.InsertData(
                table: "Finance_GL_FinancialPeriod",
                columns: new[] { "Id", "IsActive", "YearName" },
                values: new object[] { 1, true, "2020-2021" });

            migrationBuilder.InsertData(
                table: "Finance_Settings_Branch",
                columns: new[] { "Id", "BranchName" },
                values: new object[] { 1, "Main" });

            migrationBuilder.InsertData(
                table: "Finance_Settings_Currency",
                columns: new[] { "Id", "CurrencyAbbrev", "CurrencyName", "CurrencyNameAr", "IsDefault", "Rate" },
                values: new object[,]
                {
                    { 2, "$", "American Dollar", "دولار امريكي", false, 3.75m },
                    { 1, "SAR", "Saudi Arabia Rial", "ريال سعودي", true, 1m }
                });

            migrationBuilder.InsertData(
                table: "Finance_Settings_VAT",
                columns: new[] { "Id", "VatRate" },
                values: new object[] { 1, 0.15m });

            migrationBuilder.InsertData(
                table: "HR_Department",
                columns: new[] { "Id", "DepartmentName" },
                values: new object[,]
                {
                    { 1, "HR" },
                    { 2, "IT" }
                });

            migrationBuilder.InsertData(
                table: "Finance_GL_AccountChart",
                columns: new[] { "AccNum", "AccTypeId", "AccountName", "AccountNameAr", "AccountNature", "Balance", "BranchId", "CurrencyId", "IsActive", "IsParent", "ParentAcNum", "StartingBalance" },
                values: new object[,]
                {
                    { "1110000000", 1, "Buildings", "مباني", 0, 0m, 1, 1, true, true, "", 0m },
                    { "4112000000", 21, "Purchases", "مشتريات", 0, 0m, 1, 1, true, true, "", 0m },
                    { "4111000000", 20, "Expenses", "مصروفات", 0, 0m, 1, 1, true, true, "", 0m },
                    { "3110000001", 19, " Income", "ايرادات", 1, 0m, 1, 1, true, false, "3110000000", 0m },
                    { "3110000000", 19, "Income", "ايرادات", 1, 0m, 1, 1, true, true, "", 0m },
                    { "2250000001", 18, "Advances Income", "ايرادات مقدمة", 1, 0m, 1, 1, true, false, "2250000000", 0m },
                    { "2250000000", 18, "Advances Income", "ايرادات مقدمة", 1, 0m, 1, 1, true, true, "", 0m },
                    { "2240000000", 17, "Accrued Expenses", "مصروفات مستحقة", 1, 0m, 1, 1, true, true, "", 0m },
                    { "2230000000", 16, "Creditors", "دائنون", 1, 0m, 1, 1, true, true, "", 0m },
                    { "2220000001", 15, "VAT-Sales", "ضريبة قيمة مضافة-مبيعات", 1, 0m, 1, 1, true, false, "2220000000", 0m },
                    { "2220000000", 15, "Taxes", "ضرائب", 1, 0m, 1, 1, true, true, "", 0m },
                    { "2210000001", 14, "NotePayable", "شيكات موردين", 1, 0m, 1, 1, true, false, "2210000000", 0m },
                    { "2210000000", 14, "NotePayable", "شيكات موردين", 1, 0m, 1, 1, true, true, "", 0m },
                    { "2170000000", 13, "Suppliers", "موردين", 1, 0m, 1, 1, true, true, "", 0m },
                    { "5110000000", 22, "Owners", "حقوق الملكية", 1, 0m, 1, 1, true, true, "", 0m },
                    { "1269000001", 12, "VAT-Purchase", "ضريبة مشتريات", 0, 0m, 1, 1, true, false, "1269000000", 0m },
                    { "1263000000", 11, "Suppliers Advances", "دفعات مقدمة للموردين", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1262000000", 10, "StaffAdvances", "سلف", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1261000000", 9, "Custody", "عهدة", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1250000000", 8, "Store", "مخزن", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1240000003", 7, "Bounced Checks ", "شيكات رجيع", 0, 0m, 1, 1, true, false, "1240000000", 0m },
                    { "1240000002", 7, "Checks In Bank", "شيكات في البنك", 0, 0m, 1, 1, true, false, "1240000000", 0m },
                    { "1240000001", 7, "Checks In Safe", "شيكات في الخزنة", 0, 0m, 1, 1, true, false, "1240000000", 0m },
                    { "1240000000", 7, "Checks", "شيكات", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1230000000", 6, "Clients", "عملاء", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1220000000", 5, "Banks", "بنوك", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1210000000", 4, "Safe", "الخزنة", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1130000000", 3, "Furnitiures", "اثاث", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1120000000", 2, "Machines And Equipments", "اجهزة ومعدات", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1269000000", 12, "OtherCrrentAsset", "أصول متداولة أخرى", 0, 0m, 1, 1, true, true, "", 0m },
                    { "5120000000", 23, "OwnerWithdraw", "مسحوبات المالك", 1, 0m, 1, 1, true, true, "", 0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CRM_ContactBalanceInCurrency_CurrencyId",
                table: "CRM_ContactBalanceInCurrency",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CRM_Contacts_ClientAccNum",
                table: "CRM_Contacts",
                column: "ClientAccNum");

            migrationBuilder.CreateIndex(
                name: "IX_CRM_Contacts_SupplierAccNum",
                table: "CRM_Contacts",
                column: "SupplierAccNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Checks_BankAccNum",
                table: "Finance_CurrentAsset_Checks",
                column: "BankAccNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Checks_CheckHafzaId",
                table: "Finance_CurrentAsset_Checks",
                column: "CheckHafzaId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Checks_CheckLocationId",
                table: "Finance_CurrentAsset_Checks",
                column: "CheckLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Checks_CheckStatusId",
                table: "Finance_CurrentAsset_Checks",
                column: "CheckStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Checks_ContactId",
                table: "Finance_CurrentAsset_Checks",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Checks_CurrencyId",
                table: "Finance_CurrentAsset_Checks",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_BrandId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_ProductTypeId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_PurchaseAccNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                column: "PurchaseAccNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreAccNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                column: "StoreAccNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_UnitMeasureId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                column: "UnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails_StoreItemId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails_StoreTransactionId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails",
                column: "StoreTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItemWithSN_StoreItemId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItemWithSN",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItemWithSN_StoreTransactionId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItemWithSN",
                column: "StoreTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreTransaction_InvoiceNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreTransaction",
                column: "InvoiceNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreTransaction_PurchaseId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreTransaction",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreTransaction_StoreItemId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreTransaction",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentLiabilties_NP_NotesPayable_BankAccountNum",
                table: "Finance_CurrentLiabilties_NP_NotesPayable",
                column: "BankAccountNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentLiabilties_NP_NotesPayable_CurrencyId",
                table: "Finance_CurrentLiabilties_NP_NotesPayable",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentLiabilties_NP_NotesPayable_SupplierId",
                table: "Finance_CurrentLiabilties_NP_NotesPayable",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory_ChkNum",
                table: "Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory",
                column: "ChkNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Expense_ExpenseItem_AccNum",
                table: "Finance_Expense_ExpenseItem",
                column: "AccNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Expense_ExpenseItem_ExpenseTypeId",
                table: "Finance_Expense_ExpenseItem",
                column: "ExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Expense_ExpenseSummary_CostCenterId",
                table: "Finance_Expense_ExpenseSummary",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Expense_ExpenseSummary_CurrencyId",
                table: "Finance_Expense_ExpenseSummary",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Expense_ExpenseSummary_ExpenseItemId",
                table: "Finance_Expense_ExpenseSummary",
                column: "ExpenseItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Expense_ExpenseSummary_SupplierId",
                table: "Finance_Expense_ExpenseSummary",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_GL_AccountChart_AccTypeId",
                table: "Finance_GL_AccountChart",
                column: "AccTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_GL_AccountChart_BranchId",
                table: "Finance_GL_AccountChart",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_GL_AccountChart_CurrencyId",
                table: "Finance_GL_AccountChart",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_GL_HistoricalBalance_FinancialPeriodId",
                table: "Finance_GL_HistoricalBalance",
                column: "FinancialPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_GL_JournalDetails_CurrencyId",
                table: "Finance_GL_JournalDetails",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_GL_JournalDetails_JournalId",
                table: "Finance_GL_JournalDetails",
                column: "JournalId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Sales_ClientTransaction_ClientId",
                table: "Finance_Sales_ClientTransaction",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Sales_ClientTransaction_CurrencyId",
                table: "Finance_Sales_ClientTransaction",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Sales_ClientTransaction_InvoiceNum",
                table: "Finance_Sales_ClientTransaction",
                column: "InvoiceNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Sales_ClientTransaction_JournalId",
                table: "Finance_Sales_ClientTransaction",
                column: "JournalId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Sales_ClientTransaction_PaymentAccNum",
                table: "Finance_Sales_ClientTransaction",
                column: "PaymentAccNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Sales_Invoices_ContactId",
                table: "Finance_Sales_Invoices",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Sales_Invoices_CurrencyId",
                table: "Finance_Sales_Invoices",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_Purchases_CurrencyId",
                table: "Finance_Supplier_Purchases",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_Purchases_SupplierId",
                table: "Finance_Supplier_Purchases",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_SupplierTransaction_CurrencyId",
                table: "Finance_Supplier_SupplierTransaction",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_SupplierTransaction_PaymentAccNum",
                table: "Finance_Supplier_SupplierTransaction",
                column: "PaymentAccNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_SupplierTransaction_PurchaseId",
                table: "Finance_Supplier_SupplierTransaction",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_SupplierTransaction_SupplierId",
                table: "Finance_Supplier_SupplierTransaction",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_SupplierTransaction_TransId",
                table: "Finance_Supplier_SupplierTransaction",
                column: "TransId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CRM_ContactBalanceInCurrency");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_CheckHistory");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Checks");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreItemWithSN");

            migrationBuilder.DropTable(
                name: "Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory");

            migrationBuilder.DropTable(
                name: "Finance_Expense_ExpenseSummary");

            migrationBuilder.DropTable(
                name: "Finance_GL_HistoricalBalance");

            migrationBuilder.DropTable(
                name: "Finance_GL_JournalDetails");

            migrationBuilder.DropTable(
                name: "Finance_Sales_ClientTransaction");

            migrationBuilder.DropTable(
                name: "Finance_Settings_VAT");

            migrationBuilder.DropTable(
                name: "Finance_Supplier_SupplierTransaction");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_CheckHafza");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_CheckLocation");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_CheckStatus");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreTransaction");

            migrationBuilder.DropTable(
                name: "Finance_CurrentLiabilties_NP_NotesPayable");

            migrationBuilder.DropTable(
                name: "Finance_Expense_ExpenseItem");

            migrationBuilder.DropTable(
                name: "HR_Department");

            migrationBuilder.DropTable(
                name: "Finance_GL_FinancialPeriod");

            migrationBuilder.DropTable(
                name: "Finance_GL_Journal");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreItem");

            migrationBuilder.DropTable(
                name: "Finance_Sales_Invoices");

            migrationBuilder.DropTable(
                name: "Finance_Supplier_Purchases");

            migrationBuilder.DropTable(
                name: "Finance_Expense_ExpenseType");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Settings_Brand");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Settings_ProductType");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Settings_UnitMeasure");

            migrationBuilder.DropTable(
                name: "CRM_Contacts");

            migrationBuilder.DropTable(
                name: "Finance_GL_AccountChart");

            migrationBuilder.DropTable(
                name: "Finance_GL_AccountChartCounter");

            migrationBuilder.DropTable(
                name: "Finance_Settings_Branch");

            migrationBuilder.DropTable(
                name: "Finance_Settings_Currency");
        }
    }
}
