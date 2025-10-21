namespace CashFlow.Exception.ExceptionsBase;

public abstract class CashFlowException : SystemException
{
    // 'protected' impede criação direta e permite que somente classes derivadas a inicializem
    // 'base(message)' chama o construtor da classe base 'SystemException' inicializando a propriedade message herdada
    protected CashFlowException(string message) : base(message) { }   

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
