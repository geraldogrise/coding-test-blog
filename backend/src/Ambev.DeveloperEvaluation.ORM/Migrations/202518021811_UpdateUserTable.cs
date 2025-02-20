using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    public partial class UpdateUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_Firstname",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name_Lastname",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Users",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Address_Number",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Zipcode",
                table: "Users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Geolocation_Lat",
                table: "Users",
                type: "character varying(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Geolocation_Long",
                table: "Users",
                type: "character varying(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "CreatedAt", table: "Users");
            migrationBuilder.DropColumn(name: "UpdatedAt", table: "Users");
            migrationBuilder.DropColumn(name: "Name_Firstname", table: "Users");
            migrationBuilder.DropColumn(name: "Name_Lastname", table: "Users");
            migrationBuilder.DropColumn(name: "Address_City", table: "Users");
            migrationBuilder.DropColumn(name: "Address_Street", table: "Users");
            migrationBuilder.DropColumn(name: "Address_Number", table: "Users");
            migrationBuilder.DropColumn(name: "Address_Zipcode", table: "Users");
            migrationBuilder.DropColumn(name: "Address_Geolocation_Lat", table: "Users");
            migrationBuilder.DropColumn(name: "Address_Geolocation_Long", table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "character varying(20)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Users",
                type: "character varying(20)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
