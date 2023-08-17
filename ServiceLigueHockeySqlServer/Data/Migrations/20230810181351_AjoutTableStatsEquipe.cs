using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceLigueHockeySqlServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjoutTableStatsEquipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatsEquipe",
                columns: table => new
                {
                    AnneeStats = table.Column<short>(type: "smallint", nullable: false),
                    EquipeId = table.Column<int>(type: "int", nullable: false),
                    NbPartiesJouees = table.Column<short>(type: "smallint", nullable: false),
                    NbVictoires = table.Column<short>(type: "smallint", nullable: false),
                    NbDefaites = table.Column<short>(type: "smallint", nullable: false),
                    NbDefProlo = table.Column<short>(type: "smallint", nullable: false),
                    NbButsPour = table.Column<short>(type: "smallint", nullable: false),
                    NbButsContre = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsEquipe", x => new { x.EquipeId, x.AnneeStats });
                    table.ForeignKey(
                        name: "FK_StatsEquipe_Equipe_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StatsEquipe",
                columns: new[] { "AnneeStats", "EquipeId", "NbButsContre", "NbButsPour", "NbDefProlo", "NbDefaites", "NbPartiesJouees", "NbVictoires" },
                values: new object[] { (short)2023, 1, (short)199, (short)499, (short)12, (short)10, (short)82, (short)60 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatsEquipe");
        }
    }
}
