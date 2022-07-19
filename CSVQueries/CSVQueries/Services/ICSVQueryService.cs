namespace CSVQueries.Services
{
    public interface ICSVQueryService
    {

        void Upload(IFormFile file);
        List<string> QueryDB (string query);
    }

}

