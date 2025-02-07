using Microsoft.AspNetCore.Mvc;

namespace StatusCode_Toggler.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class Status_TogglerController : Controller
    {
        private static readonly Dictionary<string, int> _statusCode = new Dictionary<string, int>
        {
            ["current"] = 200 // Default status code is 200 OK
        };

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new { status = _statusCode["current"] });
        }
        [HttpPost("set-status/{code:int}")]
        public IActionResult SetStatus(int code)
        {
            _statusCode["current"] = code;
            return Ok(new { message = $"Status code is set to {code}" });
        }
        [HttpGet("check-status")]
        public IActionResult CheckStatus()
        {
            int status_Code = _statusCode["current"];

            //HTTP's Status code = status code of the App
            Response.StatusCode = status_Code;
            //Return the Error messages using Dictionary which primary to check the status
            var statusMessages = new Dictionary<int, string>
            {
                { 200, "OK" },
                { 201, "Created" },
                { 400, "Error - Bad Request" },
                { 401, "Error - Unauthorized" },
                { 403, "Error - Forbidden" },
                { 404, "Error - Not Found" },
                { 500, "Internal Server Error" },
                { 503, "Service Unavailable" }
            };

            //If any code inputed other than these codes then returns "Unknown Status Code"
            string message = statusMessages.TryGetValue(status_Code, out var msg) ? msg : "Unknown Status Code";
            return new JsonResult(new { status = status_Code, message});
        }
    }
}
