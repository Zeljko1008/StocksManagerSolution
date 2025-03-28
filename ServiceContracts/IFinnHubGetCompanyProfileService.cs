namespace ServiceContracts
{
    public interface IFinnHubGetCompanyProfileService
    {

        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);


    }
}
