using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

public class ExpenseRepository : IExpensesReadOnlyRepository, IExpensesUpdateOnlyRepository, IExpensesWriteOnlyRepository
{
    private CashFlowDbContext _dbContext;

    public ExpenseRepository(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task Delete(long id)
    {
        // Busca uma entidade no banco de dados utilizando sua chave primária!
        var result = await _dbContext.Expenses.FindAsync(id);
        // 'async' Não bloqueia a thread principal de execução enquanto espera a resopsta do banco de dados.
        // Melhora a performance e escalabilidade da aplicação.

        _dbContext.Expenses.Remove(result!); // '!' "null-forgiving operator"
        // Informa ao compilador que você tem certeza de que result não é null naquele ponto!
    }

    public async Task<List<Expense>> GetAll(User user)
    {
        // 'AsNoTracking()' diz ao EF para não rastrear as entidades retornadas. Melhora a performance, pois, só vai ler os dados.
        // Where() filtra as despesas para retornas apenas aquelas cujo UserId é igual ao Id.
        return await _dbContext.Expenses.AsNoTracking().Where(expense => expense.UserId == user.Id).ToListAsync();
    }

    // Implementação explícita da interface. Por isso a ausência do 'public'
    async Task<Expense?> IExpensesReadOnlyRepository.GetById(User user, long id)
    {
        return await GetFullExpense()
            .AsNoTracking()
            // Busca de forma assíncrona a primeira despesa que tem o Id igual ao informado e o UserId igual ao do usuário informado.
            .FirstOrDefaultAsync(expense => expense.Id == id && expense.UserId == user.Id);
    }

    async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(User user, long id)
    {
        // Para atualizar uma entidade o EF precisa rastrear (track) essa entidade para saber quais alterações aplicar no banco de dados.
        return await GetFullExpense()
            .FirstOrDefaultAsync(expense => expense.Id == id && expense.UserId == user.Id);
    }

    public void Update(Expense expense)
    {
        // Marca a entidade como modificada no contexto, sem acessar o banco de dados.
        // Por isso não é assíncrona.
        _dbContext.Expenses.Update(expense);
    }

    // Retorna uma lista de despesas que pertencem a um usuário em um determinado mês
    public async Task<List<Expense>> FilterByMonth(User user, DateOnly date)
    {
        // primeiro dia do mês
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        // último dia do mês às 23:59:59
        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContext
            .Expenses
            .AsNoTracking()
            // Filtra as despesas pelo Id do user e cuja data está entre início e fim do mês.
            .Where(expenses => expenses.UserId == user.Id && expenses.Date >= startDate && expenses.Date <= endDate)
            // Ordena os resultados por data e em seguida pelo título da despesa.
            .OrderBy(expenses => expenses.Date)
            .ThenBy(expenses => expenses.Title)
            .ToListAsync();
    }

    private IIncludableQueryable<Expense, ICollection<Tag>> GetFullExpense()
    {
        return _dbContext.Expenses
            .Include(expense => expense.Tags);
    }
}