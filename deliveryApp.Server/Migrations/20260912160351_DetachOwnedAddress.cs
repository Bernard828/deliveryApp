using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace deliveryApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class DetachOwnedAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Restaurants_AddressId",
                table: "Restaurants");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AddressId",
                table: "Restaurants",
                column: "AddressId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Restaurants_AddressId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AddressId",
                table: "Restaurants",
                column: "AddressId");
        }
    }
}
