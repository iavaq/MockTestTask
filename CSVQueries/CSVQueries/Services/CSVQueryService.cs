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
        public List<string> Result = new();

        public CSVQueryService(ModelsContext context)
        {
            _context = context;
        }

        private static void DropTable()
        {
            NpgsqlConnection conn = new(Environment.GetEnvironmentVariable("CSVQueryAPI"));
            conn.Open();
            string sql = "TRUNCATE \"Employees\" RESTART IDENTITY;";
            NpgsqlCommand command = new(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }


        public void Upload(IFormFile file)
        {
            DropTable();

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


        public List<string> KeywordQuery(string column, string word)
        {
            NpgsqlConnection con = new(Environment.GetEnvironmentVariable("CSVQueryAPI"));
            con.Open();
            string sql = $"SELECT * FROM public.\"Employees\" WHERE Lower(\"{column}\") LIKE Lower('%{word}%');";
            
            using (NpgsqlCommand command = new(sql, con))
            {

                using NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string e = reader.GetValue(0).ToString() + " - " + reader.GetString(1) + " " + reader.GetString(2) + " - " + reader.GetString(3);
                    Result.Add(e);

                }
            }

            con.Close();
            return Result;
        }

        public List<string> ExactQuery(string column, string word)
        {
            NpgsqlConnection con = new(Environment.GetEnvironmentVariable("CSVQueryAPI"));
            con.Open();
            string sql = $"SELECT * FROM public.\"Employees\" WHERE Lower(\"{column}\")=Lower('{word}');";

            using (NpgsqlCommand command = new(sql, con))
            {

                using NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string e = reader.GetValue(0).ToString() + " - " + reader.GetString(1) + " " + reader.GetString(2) + " - " + reader.GetString(3);
                    Result.Add(e);

                }
            }

            con.Close();
            return Result;
        }

        public List<string> RegexQuery(string column, string regex)
        {
            //regex
            NpgsqlConnection con = new(Environment.GetEnvironmentVariable("CSVQueryAPI"));
            con.Open();
            string sql = $"SELECT * FROM public.\"Employees\" WHERE \"{column}\" ~ '{regex}';";

            using (NpgsqlCommand command = new(sql, con))
            {
                using NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string e = reader.GetValue(0).ToString() + " - " + reader.GetString(1) + " " + reader.GetString(2) + " - " + reader.GetString(3);
                    Result.Add(e);
                }
            }

            con.Close();
            return Result;
        }

        public List<string> CharLength(string column, int num)
        {
         
            NpgsqlConnection con = new(Environment.GetEnvironmentVariable("CSVQueryAPI"));
            con.Open();
            string sql = $"SELECT * FROM public.\"Employees\" WHERE length(\"{column}\")>{num};";

            using (NpgsqlCommand command = new(sql, con))
            {

                using NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string e = reader.GetValue(0).ToString() + " - " + reader.GetString(1) + " " + reader.GetString(2) + " - " + reader.GetString(3);
                    Result.Add(e);

                }
            }

            con.Close();
            return Result;
        }

        public List<string> CompareColumns(string colA, string colB)
        {

            NpgsqlConnection con = new(Environment.GetEnvironmentVariable("CSVQueryAPI"));
            con.Open();
            string sql = $"SELECT * FROM public.\"Employees\" WHERE \"{colA}\")>\"{colB}\";";

            using (NpgsqlCommand command = new(sql, con))
            {

                using NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string e = reader.GetValue(0).ToString() + " - " + reader.GetString(1) + " " + reader.GetString(2) + " - " + reader.GetString(3);
                    Result.Add(e);

                }
            }

            con.Close();
            return Result;
        }


    }
}
