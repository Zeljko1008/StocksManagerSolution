namespace RepositoryContracts
{
    /// <summary>
    /// Represents data access methods for FinnHub entity
    /// </summary>
    public interface IFinnHubRepository
    {
        /// <summary>
        /// Gets all stock symbols
        /// </summary>
        /// <returns>returns a list of object StockSymbols</returns>
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
        /// <summary>
        /// Gets all stock candles
        /// </summary>
        /// <returns>returns a list of object StockCandles</returns>
        Task<Dictionary<string , object>?> GetStockPriceQuote(string stockSymbol);

        /// <summary>
        /// Gets all stocks supported by exchange
        /// </summary>
        /// <returns>List of stocks</returns>

        Task<List<Dictionary<string , string>>?> GetStocks();

        /// <summary>
        /// Searches for stocks based on the stock symbol
        /// </summary>
        /// <param name="stockSymbolToSearch">Symbol to search</param>
        /// <returns>List of matching stocks after search</returns>

        Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);

    }
}
