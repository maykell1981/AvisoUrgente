using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Processos",
                columns: table => new
                {
                    NumeroProcesso = table.Column<string>(maxLength: 20, nullable: false),
                    Grau = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processos", x => x.NumeroProcesso);
                });

            migrationBuilder.CreateTable(
                name: "ProcessoMovimentacoes",
                columns: table => new
                {
                    ProcessoMovimentacaoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroProcesso = table.Column<string>(maxLength: 20, nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessoMovimentacoes", x => x.ProcessoMovimentacaoId);
                    table.ForeignKey(
                        name: "FK_ProcessoMovimentacoes_Processos",
                        column: x => x.NumeroProcesso,
                        principalTable: "Processos",
                        principalColumn: "NumeroProcesso",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoMovimentacoes_NumeroProcesso",
                table: "ProcessoMovimentacoes",
                column: "NumeroProcesso");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessoMovimentacoes");

            migrationBuilder.DropTable(
                name: "Processos");
        }
    }
}
