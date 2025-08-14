using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvisibleSoftware.DeviceGateway.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDevice1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MqttPayloadOrders_Devices_DeviceId",
                table: "MqttPayloadOrders");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeviceId",
                table: "MqttPayloadOrders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MqttPayloadOrders_Devices_DeviceId",
                table: "MqttPayloadOrders",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MqttPayloadOrders_Devices_DeviceId",
                table: "MqttPayloadOrders");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeviceId",
                table: "MqttPayloadOrders",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_MqttPayloadOrders_Devices_DeviceId",
                table: "MqttPayloadOrders",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");
        }
    }
}
