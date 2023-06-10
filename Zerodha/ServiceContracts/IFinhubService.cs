namespace Zerodha.ServiceContracts
{
    public interface IFinhubService
    {
        Task<Dictionary<string, object>> GetStockDetails(string stockSymbol);
    }
}
