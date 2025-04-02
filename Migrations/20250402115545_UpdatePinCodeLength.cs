using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nhom12_EWallet.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePinCodeLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "sPinCode",
                table: "tblUsers",
                type: "varchar(255)", // Chỉnh lại độ dài là 255
                unicode: false,
                maxLength: 255, // Đặt lại maxLength thành 255
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldMaxLength: 6);
        }
    }
}
