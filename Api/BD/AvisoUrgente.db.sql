BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Processos" (
	"NumeroProcesso"	TEXT UNIQUE,
	"Grau"	INTEGER NOT NULL,
	PRIMARY KEY("NumeroProcesso")
);
CREATE TABLE IF NOT EXISTS "ProcessoMovimentacoes" (
	"ProcessoMovimentacao"	INTEGER,
	"NumeroProcesso"	TEXT NOT NULL,
	"Data"	TEXT NOT NULL,
	"Descricao"	TEXT NOT NULL,
	PRIMARY KEY("ProcessoMovimentacao" AUTOINCREMENT),
	FOREIGN KEY("NumeroProcesso") REFERENCES "Processos"("NumeroProcesso")
);
CREATE INDEX IF NOT EXISTS "IX_Processo" ON "Processos" (
	"NumeroProcesso"
);
CREATE INDEX IF NOT EXISTS "IX_ProcessoMovimentacao" ON "ProcessoMovimentacoes" (
	"NumeroProcesso"
);
COMMIT;
