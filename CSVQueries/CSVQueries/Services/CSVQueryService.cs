using CsvHelper;
using CSVQueries.Models;
using Npgsql;
using System.Globalization;
using CsvHelper.Configuration;

namespace CSVQueries.Services
{
    public class CSVQueryService : ICSVQueryService
    {
        private readonly ModelsContext _context;
       
        private DBManager postgres = new();

        public List<string> Result = new();

        public CSVQueryService(ModelsContext context)
        {
            _context = context;
        }

        private void TruncTable()
        {
            postgres.Connection.Open();
            postgres.SQLCommand("TRUNCATE \"Employees\" RESTART IDENTITY;").ExecuteNonQuery();
            postgres.Connection.Close();
        }

        public void Upload(IFormFile file)
        {
            
            TruncTable();
          
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null

            };

            using (var streamReader = new StreamReader(file.OpenReadStream()))
            using (var csvReader = new CsvReader(streamReader, config))
            {
                var records = csvReader.GetRecords<Employee>();
                foreach (var row in records)
                {
                    _context.Add(row);
                }
            }
           _context.SaveChanges();
        }


        public List<string> QueryDB(string query)
        {
            postgres.Connection.Open();
            using (NpgsqlCommand cmd = postgres.SQLCommand(query))
            {
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //is there a way to load all matches as batch rather than row by row?
                    string e = reader.GetValue(0).ToString() + " - " + reader.GetString(1) + " " + reader.GetString(2) + " - " + reader.GetString(3);
                    Result.Add(e);
                }
            }
            postgres.Connection.Close();
            return Result;
        }

    }
}
