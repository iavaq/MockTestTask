using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CSVQueries.Models
{
    public class Employee
    {

        //[Key]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        //[Key]
        //public int Id { get; set;}
        
        [Name("first_name")]
        public string FirstName { get; set; }

        [Name("last_name")]
        public string LastName { get; set; }

        [Name("company_name")]
        public string CompanyName { get; set; }

        [Name("address")]
        public string Address { get; set; }

        [Name("city")]
        public string City { get; set; }

        [Name("county")]
        public string County { get; set; }

        [Name("postal")]
        public string Postal { get; set; }

        [Name("phone1")]
        public string Phone1 { get; set; }

        [Name("phone2")]
        public string Phone2 { get; set; }

        [Name("email")]
        public string Email { get; set; }

        [Name("web")]
        public string Web { get; set; }

        [Key]
        public int? Id { get; set; }
    }
}
