using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class UserInfo1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_user_info",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_account = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    user_password = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    password_salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    user_name = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    user_phone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    user_email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    refresh_token = table.Column<string>(type: "text", nullable: true),
                    refresh_token_expiry_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_user_info", x => x.user_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_user_info");
        }
    }
}
