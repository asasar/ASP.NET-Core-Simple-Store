using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SimpleStore.Migrations
{
    [ExcludeFromCodeCoverageAttribute]
    public partial class addInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusInformation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Group = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true),
                    StatusOrderId = table.Column<int>(nullable: true),
                    TotalPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderInvoices_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderInvoices_StatusInformation_StatusOrderId",
                        column: x => x.StatusOrderId,
                        principalTable: "StatusInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    OrderInvoiceId = table.Column<int>(nullable: true),
                    StatusOrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryStatus_OrderInvoices_OrderInvoiceId",
                        column: x => x.OrderInvoiceId,
                        principalTable: "OrderInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryStatus_StatusInformation_StatusOrderId",
                        column: x => x.StatusOrderId,
                        principalTable: "StatusInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderInvoiceId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    Qty = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInvoice_OrderInvoices_OrderInvoiceId",
                        column: x => x.OrderInvoiceId,
                        principalTable: "OrderInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductInvoice_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryStatus_OrderInvoiceId",
                table: "HistoryStatus",
                column: "OrderInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryStatus_StatusOrderId",
                table: "HistoryStatus",
                column: "StatusOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInvoices_CustomerId",
                table: "OrderInvoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInvoices_StatusOrderId",
                table: "OrderInvoices",
                column: "StatusOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoice_OrderInvoiceId",
                table: "ProductInvoice",
                column: "OrderInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoice_ProductId",
                table: "ProductInvoice",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryStatus");

            migrationBuilder.DropTable(
                name: "ProductInvoice");

            migrationBuilder.DropTable(
                name: "OrderInvoices");

            migrationBuilder.DropTable(
                name: "StatusInformation");
        }
    }
}
