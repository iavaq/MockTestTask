using Npgsql;
using Microsoft.EntityFrameworkCore;
namespace CSVQueries.Models
{
    public class DBManager
    {
        public string ConnectionString {get; set; }
        public NpgsqlConnection Connection { get; set; }
      
        public DBManager()
        {
            ConnectionString = GetConnectionString();
            Connection = new NpgsqlConnection(ConnectionString);
        }

        public NpgsqlCommand SQLCommand(string cmd)
        {
            return new NpgsqlCommand(cmd,Connection);
        }

        private  string GetConnectionString()
        {
            return Environment.GetEnvironmentVariable("CSVQueryAPI")!;
        }
    }
}
