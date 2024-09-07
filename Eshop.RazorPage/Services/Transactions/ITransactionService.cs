using Eshop.RazorPage.Models;

namespace Eshop.RazorPage.Services.Transactions;

public interface ITransactionService
{
    Task<ApiResult<string>?> CreateTransaction(CreateTransactionCommand command);

}


public class TransactionService(HttpClient client):ITransactionService
{
    public const string ModuloName = " Transaction";

    public async Task<ApiResult<string>?> CreateTransaction(CreateTransactionCommand command)
    {
        var httpResponseMessage = await client.PostAsJsonAsync(ModuloName, command);
        return await httpResponseMessage.Content.ReadFromJsonAsync<ApiResult<string>>();
    }
}

public class CreateTransactionCommand
{
    public long OrderId { get; set; }

    public string SuccessCallBackUrl { get; set; }

    public string ErrorCallBackUrl { get; set; }
}