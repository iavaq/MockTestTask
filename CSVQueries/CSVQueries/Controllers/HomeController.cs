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
            return _service.KeywordQuery(columnName, word);
        }

        [HttpGet("/SearchExact/{columnName},{word}")]
        public ActionResult<List<string>> SearchExact(string columnName, string word)
        {
            return _service.ExactQuery(columnName, word);
        }

        [HttpGet("/RegexSearch/{columnName},{regex}")]
        public ActionResult<List<string>> RegexSearch(string columnName, string regex)
        {
            //regex = ^\d{3}[^\d]
            //string regex2 = "^[^\d]{1,2}\d{1}(\y|[^\d])";
            return _service.RegexQuery(columnName, regex);
        }

        [HttpGet("/LimitByCharacterLength/{columnName},{minValue}")]
        public ActionResult<List<string>> LimitByCharacterLength(string columnName, int minValue)
        {
            return _service.CharLength(columnName, minValue);
        }

        [HttpGet("/CompareColumns/{columnA},{columnB}")]
        public ActionResult<List<string>> CompareColumns(string columnA, string columnB)
        {

            return _service.CompareColumns(columnA, columnB);
        }
    }
}
