namespace CSVQueries.Services
{
    public interface ICSVQueryService
    {
        //void DropTable();
        void Upload(IFormFile file);
        List<string> KeywordQuery(string columnName, string keyword);
        List<string> ExactQuery(string columnName, string keyword);
        List<string> RegexQuery(string columnName, string regex);
        List<string> CharLength(string columnName, int minLength);
        List<string> CompareColumns(string columnA, string columnB);

    }

}

