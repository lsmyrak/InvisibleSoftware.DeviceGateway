using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvisibleSoftware.DeviceGateway.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateByFunction",
                table: "Rooms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreateByFunction",
                table: "Roles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreateByFunction",
                table: "Places",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreateByFunction",
                table: "MqttPayloads",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreateByFunction",
                table: "MqttPayloadOrders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreateByFunction",
                table: "MqttConfigs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreateByFunction",
                table: "DeviceTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreateByFunction",
                table: "Devices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreateByFunction",
                table: "DeviceGroups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreateByFunction",
                table: "CommandHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoomId",
                table: "AspNetUsers",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rooms_RoomId",
                table: "AspNetUsers",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rooms_RoomId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoomId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreateByFunction",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CreateByFunction",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreateByFunction",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "CreateByFunction",
                table: "MqttPayloads");

            migrationBuilder.DropColumn(
                name: "CreateByFunction",
                table: "MqttPayloadOrders");

            migrationBuilder.DropColumn(
                name: "CreateByFunction",
                table: "MqttConfigs");

            migrationBuilder.DropColumn(
                name: "CreateByFunction",
                table: "DeviceTypes");

            migrationBuilder.DropColumn(
                name: "CreateByFunction",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CreateByFunction",
                table: "DeviceGroups");

            migrationBuilder.DropColumn(
                name: "CreateByFunction",
                table: "CommandHistories");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "AspNetUsers");
        }
    }
}
