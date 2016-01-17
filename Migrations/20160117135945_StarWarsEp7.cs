using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace StarWars.Migrations
{
    public partial class StarWarsEp7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Director",
                columns: table => new
                {
                    DirectorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Born = table.Column<DateTime>(nullable: false),
                    Episode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Director", x => x.DirectorId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Director");
        }
    }
}
