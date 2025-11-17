var builder = WebApplication.CreateBuilder(args);

/*
 * 1. ADICIONE O SERVIÇO BÁSICO DE HEALTH CHECK
 * Isso apenas verifica se a API está "viva",
 * não vai checar o banco de dados.
 */
builder.Services.AddHealthChecks();

// Adicione outros serviços se precisar (como Controllers)
builder.Services.AddControllers();

/*
 * 2. NÃO REGISTRE O DBCONTEXT
 * Vamos pular o `builder.Services.AddDbContext<...>()`
 * de propósito, para simular a falha que você quer evitar.
 */


// ----------
var app = builder.Build();
// ----------


/*
 * 3. MAPEIE O ENDPOINT DE HEALTH CHECK
 * Isso cria o endpoint /health
 */
app.MapHealthChecks("/health");

app.MapControllers();

/*
 * 4. NÃO CHAME A MIGRAÇÃO DO BANCO!
 * No seu log, o erro vinha de uma chamada aqui (perto da linha 117).
 * A linha parecida com 'MigrateDatabase(app.Services)' deve ser
 * COMENTADA ou REMOVIDA para este teste.
 *
 * // DataBaseMigration.MigrateDatabase(app.Services); // <-- LINHA DESATIVADA
 */

app.Run();
