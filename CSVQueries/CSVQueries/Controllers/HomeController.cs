using Microsoft.AspNetCore.Mvc;
using CSVQueries.Models;
using CSVQueries.Services;

namespace CSVQueries.Controllers
{
    [ApiController]
    [Route("api/v1/csv_query")]
    public class HomeController : ControllerBase
    {
        private readonly ICSVQueryService _service;
        private string _query;

        public HomeController(ICSVQueryService service)
        {
            _service = service;
        }

        [HttpPost("csv_file")]
        //post: api/v1/csv_query/file
        public ActionResult UploadFile(IFormFile file)
        {
            _service.Upload(file);
            return NoContent();
        }

       [HttpGet("/SearchByKeyword/{columnName},{word}")]
        public ActionResult<List<string>> SearchByKeyword(string columnName, string word)
        {
            _query = $"SELECT * FROM public.\"Employees\" WHERE Lower(\"{columnName}\") LIKE Lower('%{word}%');";
            return _service.QueryDB(_query);
        }

        [HttpGet("/SearchExact/{columnName},{word}")]
        public ActionResult<List<string>> SearchExact(string columnName, string word)
        {
            _query = $"SELECT * FROM public.\"Employees\" WHERE Lower(\"{columnName}\")=Lower('{word}');";
            return _service.QueryDB(_query);
        }

        [HttpGet("/RegexSearch/{columnName},{regex}")]
        public ActionResult<List<string>> RegexSearch(string columnName, string regex)
        {
            //regex = ^\d{3}[^\d]
            //string regex2 = "^[^\d]{1,2}\d{1}(\y|[^\d])";

            _query = $"SELECT * FROM public.\"Employees\" WHERE \"{columnName}\" ~ '{regex}';";
            return _service.QueryDB(_query);
        }

        [HttpGet("/LimitByCharacterLength/{columnName},{minValue}")]
        public ActionResult<List<string>> LimitByCharacterLength(string columnName, int minValue)
        {
            _query = $"SELECT * FROM public.\"Employees\" WHERE length(\"{columnName}\")>{minValue};";
            return _service.QueryDB(_query);
        }

        [HttpGet("/GreaterColumn/{columnA},{columnB}")]
        public ActionResult<List<string>> CompareColumns(string columnA, string columnB)
        {
            _query = $"SELECT * FROM public.\"Employees\" WHERE \"{columnA}\">\"{columnB}\";";
            return _service.QueryDB(_query);
        }
    }
}
